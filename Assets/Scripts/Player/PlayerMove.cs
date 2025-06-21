using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour
{

    public Transform playerbody;

    private Vector3 originalScale;
    private float originalHeight;

    public Transform groundCheck;

    public PlayerAtributes atributes;

    public float groudDistance = 0.4f;
    public LayerMask grounMask;

    public Raycast ray_cast; //check if player is interacing;

    public float jumpHeight = 3f;

    bool isGrounded;
    bool isCrouching = false;

    public bool interacting; //check if the player is interacting


    public CharacterController controller;

    public float speed ;
    public float speed_container ; //the same as speed, hold value to recover from speed;
    public float sprint_speed = 1.89f;
    public float crouchSpeed = 5f;

    public float gravity = -19.62f;

    Vector3 velocity;

    void Sprint(){
        Debug.Log("sprinting");

        if(atributes.Stamina > 0) {
            atributes.Stamina -= 0.01f;
            speed = sprint_speed*speed_container;
        }
        else{
            speed = speed_container;
        }
    }

    void SprintRecover(){
        speed = speed_container;
        if(atributes.Stamina < atributes.max_stamina) atributes.Stamina += 0.01f;
    }

    // Update is called once per frame
    private void Start()
    {
        originalHeight = playerbody.localScale.y;
        originalScale = playerbody.localScale;
    }
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

        /*sprint ----*/
        if (Input.GetKey(KeyCode.LeftShift) &&
   (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            Sprint();
        }
        else SprintRecover();

        if (!interacting){
            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching == false)
        {
            isCrouching = true;
            controller.height = controller.height / 2f;
            Debug.Log("Pomniejszono");
            playerbody.localScale = new Vector3(originalScale.x, originalScale.y / 2f, originalScale.z);
            velocity.y = -2000f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching == true)
        {
            isCrouching = false;
            controller.height = originalHeight * 2f;
            playerbody.localScale = originalScale;
        }

    }

}