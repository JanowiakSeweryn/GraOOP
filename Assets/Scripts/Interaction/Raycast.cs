

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    float length = 10f;

    public bool locked = false; //Check if camera is locked 
    public GameObject hit_obj;
    List<string> InteractingTags = new List<string>();

    public void RayCasting(){
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        
        Vector3 forward = transform.TransformDirection(Vector3.forward)*length;

        if (Physics.Raycast(ray, out hit, length))
        {
            hit_obj = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            for(int i=0;i<InteractingTags.Count;i++){
                if (hit_obj.CompareTag(InteractingTags[i]))
                {
                    locked = true;
                    break;
                }
            }
        }

        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }

        Debug.DrawRay(transform.position,forward,Color.green,10f);

    }

    void Update()
    {
        if (Input.GetKey("e")) RayCasting();
    }
    
    //run once
    //set up the interactive tags
    void Start(){
        InteractingTags.Add("upgrade_menu");
        InteractingTags.Add("dialog");
    }
}
