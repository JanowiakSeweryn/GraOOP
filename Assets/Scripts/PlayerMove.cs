<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
<<<<<<< Updated upstream
     public float speed = 5f;
   
      public Raycast ray_cast;
=======
    public Transform playerbody;  

    private Vector3 originalScale;
    private float originalHeight;


    public Transform groundCheck;
    public float groudDistance = 0.4f;
    public LayerMask grounMask;

    public float jumpHeight = 3f;

    bool isGrounded;
    bool isCrouching = false;
>>>>>>> Stashed changes


<<<<<<< Updated upstream
=======
    public CharacterController controller;
    public Transform playerbody;

    public float speed = 10f;
    public float sprintSpeed = 15f;
    public float crouchSpeed = 5f;

<<<<<<< Updated upstream
    public float gravity = -19.62f;

    Vector3 velocity;

    Vector3 originalScale;
    float originalHeight;

    bool isCrouching = false;

    void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        if (playerbody == null)
            playerbody = transform.Find("Capsule");

        originalScale = playerbody.localScale;
        originalHeight = controller.height;
    }


=======
    public float speed = 10f;
    public float sprintSpeed = 15f;
    public float crouchSpeed = 5f;

    public float gravity = -19.62f;

    Vector3 velocity;
    private void Start()
    {
        originalHeight = playerbody.localScale.y;
        originalScale = playerbody.localScale;
        
    }
>>>>>>> Stashed changes
    // Update is called once per frame
>>>>>>> Stashed changes
    void Update()
    {
        if(!ray_cast.locked){
            float horizontalInput = 0f;
            float verticalInput = 0f;

            if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
            if (Input.GetKey(KeyCode.S)) verticalInput = -1f;
            if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;
            if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized * speed * Time.deltaTime;

            transform.Translate(movement, Space.Self);
        }
=======
=======
>>>>>>> Stashed changes
        if (isGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
<<<<<<< Updated upstream
        else if (isCrouching == true)
        {
            controller.Move(move * crouchSpeed * Time.deltaTime);
        }
        else 
=======
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            controller.Move(move * crouchSpeed * Time.deltaTime);
        }
        else
>>>>>>> Stashed changes
        {
            controller.Move(move * speed * Time.deltaTime);
        }

<<<<<<< Updated upstream
=======
        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching == false)
        {
            isCrouching = true;
            controller.height = controller.height / 2f;
            Debug.Log("Pomniejszono");
            playerbody.localScale = new Vector3(originalScale.x, originalScale.y / 2f, originalScale.z);
            velocity.y = -200f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching == true)
        {
            isCrouching = false;
            controller.height = originalHeight *2f;
            playerbody.localScale = originalScale;
        }

>>>>>>> Stashed changes
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching == false)
        {
            isCrouching = true;
            velocity.y = -200f;
            controller.height = controller.height / 2f;
            playerbody.localScale = new Vector3(originalScale.x, originalScale.y / 2f, originalScale.z);
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && isCrouching == true)
        {
            isCrouching = false;
            controller.height = originalHeight;
            playerbody.localScale = originalScale;
        }





        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
>>>>>>> Stashed changes
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
<<<<<<< Updated upstream
     public float speed = 5f;
   
      public Raycast ray_cast;
=======
    public Transform playerbody;  

    private Vector3 originalScale;
    private float originalHeight;


    public Transform groundCheck;
    public float groudDistance = 0.4f;
    public LayerMask grounMask;

    public float jumpHeight = 3f;

    bool isGrounded;
    bool isCrouching = false;
>>>>>>> Stashed changes


<<<<<<< Updated upstream
=======
    public CharacterController controller;
    public Transform playerbody;

    public float speed = 10f;
    public float sprintSpeed = 15f;
    public float crouchSpeed = 5f;

<<<<<<< Updated upstream
    public float gravity = -19.62f;

    Vector3 velocity;

    Vector3 originalScale;
    float originalHeight;

    bool isCrouching = false;

    void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        if (playerbody == null)
            playerbody = transform.Find("Capsule");

        originalScale = playerbody.localScale;
        originalHeight = controller.height;
    }


=======
    public float speed = 10f;
    public float sprintSpeed = 15f;
    public float crouchSpeed = 5f;

    public float gravity = -19.62f;

    Vector3 velocity;
    private void Start()
    {
        originalHeight = playerbody.localScale.y;
        originalScale = playerbody.localScale;
        
    }
>>>>>>> Stashed changes
    // Update is called once per frame
>>>>>>> Stashed changes
    void Update()
    {
        if(!ray_cast.locked){
            float horizontalInput = 0f;
            float verticalInput = 0f;

            if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
            if (Input.GetKey(KeyCode.S)) verticalInput = -1f;
            if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;
            if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized * speed * Time.deltaTime;

            transform.Translate(movement, Space.Self);
        }
=======
=======
>>>>>>> Stashed changes
        if (isGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
<<<<<<< Updated upstream
        else if (isCrouching == true)
        {
            controller.Move(move * crouchSpeed * Time.deltaTime);
        }
        else 
=======
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            controller.Move(move * crouchSpeed * Time.deltaTime);
        }
        else
>>>>>>> Stashed changes
        {
            controller.Move(move * speed * Time.deltaTime);
        }

<<<<<<< Updated upstream
=======
        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching == false)
        {
            isCrouching = true;
            controller.height = controller.height / 2f;
            Debug.Log("Pomniejszono");
            playerbody.localScale = new Vector3(originalScale.x, originalScale.y / 2f, originalScale.z);
            velocity.y = -200f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching == true)
        {
            isCrouching = false;
            controller.height = originalHeight *2f;
            playerbody.localScale = originalScale;
        }

>>>>>>> Stashed changes
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching == false)
        {
            isCrouching = true;
            velocity.y = -200f;
            controller.height = controller.height / 2f;
            playerbody.localScale = new Vector3(originalScale.x, originalScale.y / 2f, originalScale.z);
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && isCrouching == true)
        {
            isCrouching = false;
            controller.height = originalHeight;
            playerbody.localScale = originalScale;
        }





        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
>>>>>>> Stashed changes
    }
}
>>>>>>> Stashed changes
