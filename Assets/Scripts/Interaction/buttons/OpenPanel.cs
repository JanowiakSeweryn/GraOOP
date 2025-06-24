using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel: MonoBehaviour
{
   
    public GameObject Panel_menu;
    public Raycast ray_cast;
    public GameObject logo;

    public void Open_Panel(){
        if(Panel_menu != null) {
            Panel_menu.SetActive(true); 
            logo.SetActive(true);
        }
    }

}
