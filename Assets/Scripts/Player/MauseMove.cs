using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauseMove : MonoBehaviour
{
    public float mouseSensitivity = 300f;
    public Transform orientation;  //gracz

    public Raycast ShowUI;
    public PauseGame pause_game;

    
    private float x_rot = 0f;
    private float y_rot = 0f;

    bool UnlockCursor(){
        if(ShowUI.locked || pause_game.isPaused) return true;
        else return false;
    
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Hides and locks cursor to center
        Cursor.visible = false;
    }

    void Update()
    {
        if (!UnlockCursor())
        {   
            Cursor.lockState = CursorLockMode.Locked;  // Hides and locks cursor to center
            Cursor.visible = false;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            x_rot -= mouseY;
            y_rot += mouseX;
            x_rot = Mathf.Clamp(x_rot, -90f, 90f);  //zakresy katow poolozenia camery;

            transform.rotation = Quaternion.Euler(x_rot, y_rot, 0f);  //zmiana orientacji kamera
            orientation.rotation = Quaternion.Euler(0, y_rot, 0f);
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;  // Hides and locks cursor to center
            Cursor.visible = true;
        }

        }
}
