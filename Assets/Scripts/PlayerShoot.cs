using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GunData gunData;

    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }
        if(Input.GetKeyDown(reloadKey) || (gunData.currentAmmo == 0 && Input.GetMouseButtonDown(0)))
        {
            reloadInput?.Invoke();
        }
    }
}