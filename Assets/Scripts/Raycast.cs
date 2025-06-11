using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    float length = 10f;

    public bool locked = false; //Check if camera is locked 
    
    public void RayCasting(){
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        
        Vector3 forward = transform.TransformDirection(Vector3.forward)*length;

        if (Physics.Raycast(ray, out hit, length))
        {
            GameObject hit_obj = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit_obj.CompareTag("UI_point1")) locked = true;

            Debug.Log("hiting with: " + hit_obj.name);

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("not hitting!");

        }

        Debug.DrawRay(transform.position,forward,Color.green,10f);

    }

    void Update()
    {
        if (Input.GetKey("e")) RayCasting();
    }
}
