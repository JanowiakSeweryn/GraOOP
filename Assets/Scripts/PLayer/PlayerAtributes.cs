using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtributes : MonoBehaviour
{
    public float Health;
    public float Stamina; //
    public float max_stamina;
    public float max_health;

    void Start(){
        Health = 100f;
        Stamina = 50f;
        max_stamina = Stamina;
        max_health = Health;
    }
}
