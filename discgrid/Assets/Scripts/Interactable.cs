using UnityEngine;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour
{

    public float radius = 3f;               // How close do we need to be to interact?
    public Transform interactionTransform;  // The transform from where we interact in case you want to offset it

    bool isFocus = false;   // Is this interactable currently being focused?
    Transform player;       // Reference to the player transform

    bool hasInteracted = false; // Have we already interacted with the object?

    public Material normalMat;
    public Material focusedMat;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        //Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        /*// If we are currently being focused
        // and we haven't already interacted with the object
        if (isFocus && !hasInteracted)
        {
            // If we are close enough
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                // Interact with the object
                Interact();
                hasInteracted = true;
            }
        }*/
    }

    // Called when the object starts being focused
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
        GetComponent<MeshRenderer>().material = focusedMat;
    }

    // Called when the object is no longer focused
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
        GetComponent<MeshRenderer>().material = normalMat;
    }

    // Draw our radius in the editor
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public bool CheckDistanceToFocus(Transform playerTransform)
    {
        bool canFocus = false;

        float distance = Vector3.Distance(playerTransform.position, interactionTransform.position);
        if (distance <= radius)
        {
            canFocus = true;
        }
        return canFocus;
    }

    public bool CheckIfAndInteract()
    {
        // If we are currently being focused
        // and we haven't already interacted with the object
        if (isFocus && !hasInteracted)
        {
            // If we are close enough
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                // Interact with the object
                Interact();
                hasInteracted = true;
            }
        }
        return hasInteracted;
    }
}