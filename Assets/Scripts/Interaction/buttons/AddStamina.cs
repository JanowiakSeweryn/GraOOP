using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStamina : MonoBehaviour
{

    public PlayerAtributes atributes;
    public void add_Stamina(){
        atributes.max_stamina += 10;
        atributes.Stamina = atributes.max_stamina;
    }
}
