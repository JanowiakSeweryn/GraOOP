using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour,  IDamagable

{

    private float health = 100f;
    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health < 0 )
        {
            Destroy(gameObject);
        }
    }
}