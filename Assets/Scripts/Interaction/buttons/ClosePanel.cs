using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
   
    public GameObject Panel_menu;
    public GameObject cube;
    public Raycast ray_cast;

    public void Close_Panel(){
        Panel_menu.SetActive(false); 
        ray_cast.locked = false;
        cube.SetActive(true); 
        
    }
}

