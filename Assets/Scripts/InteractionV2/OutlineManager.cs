using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    public float raycastDistance = 5f;
    private GameObject lastOutlinedObject;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            Outline outline = hit.collider.GetComponent<Outline>();

            if (outline != null)
            {
                if (lastOutlinedObject != hit.collider.gameObject)
                {
                    ClearLastOutline();
                    outline.enabled = true;
                    lastOutlinedObject = hit.collider.gameObject;
                }
            }
            else
            {
                ClearLastOutline();
            }
        }
        else
        {
            ClearLastOutline();
        }
    }

    void ClearLastOutline()
    {
        if (lastOutlinedObject != null)
        {
            Outline outline = lastOutlinedObject.GetComponent<Outline>();
            if (outline != null)
                outline.enabled = false;

            lastOutlinedObject = null;
        }
    }
}
