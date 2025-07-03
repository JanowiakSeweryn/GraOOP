using UnityEngine;

public class InspectItneraction : MonoBehaviour, IInteractable
{
    public Transform holdPos;          
    public GameObject player;          
    public float throwForce = 500f;

    private Rigidbody rb;
    private bool isHeld = false;
    private bool canDrop = true;
    private float rotationSensitivity = 1f;
    private int originalLayer;

    private GameObject cameraLock;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalLayer = gameObject.layer;

        
    }

    void Update()
    {
        if (isHeld)
        {
            MoveObject();
            RotateObject();

            if (Input.GetMouseButtonDown(0) && canDrop)
                ThrowObject();

            if (Input.GetKeyUp(KeyCode.E) && canDrop)
                DropObject();
        }
    }

    public void Interact()
    {
        if (!isHeld)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        if (holdPos == null || player == null)
        {
            Debug.LogWarning("Brakuje referencji do holdPos lub gracza.");
            return;
        }

        isHeld = true;
        rb.isKinematic = true;
        transform.SetParent(holdPos);
        transform.localPosition = Vector3.zero;
        gameObject.layer = LayerMask.NameToLayer("holdLayer");
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), true);
    }

    private void DropObject()
    {
        isHeld = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        gameObject.layer = originalLayer;
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), false);
    }

    private void ThrowObject()
    {
        DropObject();
        rb.AddForce(player.transform.forward * throwForce);
    }

    private void MoveObject()
    {
        transform.position = holdPos.position;
    }

    private void RotateObject()
    {
        if (Input.GetKey(KeyCode.N))
        {
            GameObject weaponHolder = GameObject.Find("WeaponHolder");
            WeaponSway weaponSway = weaponHolder.GetComponent<WeaponSway>();
            GameObject mainCamera = GameObject.Find("MainCamera");
            MauseMove mouseMove = mainCamera.GetComponent<MauseMove>();
            weaponSway.enabled = false;
            mouseMove.enabled = false;
            canDrop = false;
            float x = Input.GetAxis("Mouse X") * rotationSensitivity;
            float y = Input.GetAxis("Mouse Y") * rotationSensitivity;
            transform.Rotate(Vector3.down, x);
            transform.Rotate(Vector3.right, y);
        }
        else
        {
            GameObject weaponHolder = GameObject.Find("WeaponHolder");
            WeaponSway weaponSway = weaponHolder.GetComponent<WeaponSway>();
            GameObject mainCamera = GameObject.Find("MainCamera");
            MauseMove mouseMove = mainCamera.GetComponent<MauseMove>();
            canDrop = true;
            mouseMove.enabled = true;
            weaponSway.enabled = true;
        }
    }
}
