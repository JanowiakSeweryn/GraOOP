using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour
{
    

    public Transform groundCheck;
    public float groudDistance = 0.4f;
    public LayerMask grounMask;

    public Raycast ray_cast; //check if player is interacing;

    public float jumpHeight = 3f;

    bool isGrounded;
    public bool interacting; //check if the player is interacting


    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -19.62f;

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        if(ray_cast.locked) interacting = true;
        else interacting = false;
        isGrounded = Physics.CheckSphere(groundCheck.position, groudDistance, grounMask);

        if(isGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!interacting){
            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}