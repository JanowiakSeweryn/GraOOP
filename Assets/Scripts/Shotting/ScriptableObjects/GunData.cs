using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun",menuName ="Weapon/Gun")]

public class GunData: ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDisatance;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRateRPM;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;
}
