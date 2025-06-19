<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    public Raycast ShowUI; //cube that represent button (to get the locked var)
    public GameObject cube; //same cube, but as GameObject (to hide vis)
    public Transform Camera; //Main camera 
    public Transform menu; //canvas as menu
    public GameObject canvas; // camwav ass Gameojcet (to hide vis)

    // Update is called once per frame
    void Update()
    {
        if(ShowUI.locked){
            Debug.Log("CAMERA IS LOCKED!");
   
            Camera.LookAt(menu);
            
            cube.SetActive(false); //cube is
            canvas.SetActive(true);
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    public Raycast ShowUI; //cube that represent button (to get the locked var)
    public GameObject cube; //same cube, but as GameObject (to hide vis)
    public Transform Camera; //Main camera 
    public Transform menu; //canvas as menu
    public GameObject canvas; // camwav ass Gameojcet (to hide vis)

    // Update is called once per frame
    void Update()
    {
        if(ShowUI.locked){
            Debug.Log("CAMERA IS LOCKED!");
   
            Camera.LookAt(menu);
            
            cube.SetActive(false); //cube is
            canvas.SetActive(true);
        }
    }
}
>>>>>>> Stashed changes
