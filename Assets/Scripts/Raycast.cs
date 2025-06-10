using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    float length = 10f;

    public bool locked = false; //Check if camera is locked 
    
    public void OnHit(){
        if (Input.GetKey("e")){
            Debug.Log("E PRESSED!");
            locked = true;
        }
    }

    void Update()
    {
    
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        Vector3 forward = transform.TransformDirection(Vector3.forward)*length;
        if(Physics.Raycast(ray, out hit, length)){
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward)*hit.distance,Color.yellow);       
             
             OnHit();
        }
        else{
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward)*hit.distance,Color.red);
            Debug.Log("not hitting!");
        }

    Debug.DrawRay(transform.position,forward,Color.green,10f);


    }
}
