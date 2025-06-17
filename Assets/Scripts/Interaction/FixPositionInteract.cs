using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionInteract : MonoBehaviour
{
    public Raycast ray_cast;
    public float fix_y ;
    public float fix_z ;
    bool set = false ;
    Vector3 Obj_global_pos;
     Vector3 fix_pos;
     Vector3 Fixed;
    // Update is called once per frame
    void Update()
    {   

        if(ray_cast.locked){

            if(!set){
            fix_pos =  new Vector3(0f,fix_y,fix_z);
            Obj_global_pos = ray_cast.hit_obj.transform.position ;
             Fixed = Obj_global_pos - fix_pos;
             transform.position = Fixed;
            }
             set = true;

        }
        else{
            set = false;
        }
    }
}
