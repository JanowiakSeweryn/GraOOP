using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public GameObject showUI_button;
    public Transform camera;
    public float show_dist = 5; // distance from which you can see the object

    bool show;
    // Update is called once per frame
    void Update()
    {
        //Distance betwen camera and an object
        float distance = Vector3.Distance(camera.position, transform.position);
        show = show_dist >= distance; // if show_dist > dist show = true,etc..
        showUI_button.SetActive(show);
        Debug.Log(distance);
    }
}
