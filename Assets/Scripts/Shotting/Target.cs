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
            if (TryGetComponent<HerdAnimalAI>(out HerdAnimalAI animal))
            {
                animal.colony.RemoveFromColony(this.gameObject); 
            }
            Destroy(gameObject);
        }
    }
}