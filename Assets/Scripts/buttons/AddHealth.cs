using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{

    public PlayerAtributes atributes;
    public float health_regen = 10f;
    
    public void IncreaseHealth(){
    if(atributes.Health + health_regen >= atributes.max_health) atributes.Health = atributes.max_health;
    if(atributes.Health < atributes.max_health) atributes.Health += health_regen;
    }

    public void IncreaseMaxHealth(){
        atributes.max_health += health_regen;
    }
}
