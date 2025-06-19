using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;

    float detectionRadius = 30f;
    Vector3 gunPos;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        Debug.Log("reloading");
        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;

    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRateRPM / 60f);

    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if(CanShoot())
            {
                if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDisatance))
                {
                    IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();
                    damagable?.Damage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();

                
                gunPos = transform.position;

                HerdAnimalAI[] allAnimals = FindObjectsOfType<HerdAnimalAI>();

                foreach (var animal in allAnimals)
                {
                    if (Vector3.Distance(animal.transform.position, gunPos) <= detectionRadius)
                    {
                        animal.SeeDanger();
                    }
                }
            }
        }
    }
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward * 100f, Color.red);
    }
    private void OnGunShot()
    {
        
    }
}
