using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{

    public Transform InteractorSource;
    public float interactRange = 5;
    private KeyCode interactionKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if(Physics.Raycast(r, out RaycastHit hitinfo, interactRange))
            {
                if (hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }

        }
    }
}
