using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    public GameObject player;
    private Camera cam;

    [SerializeField]
    private DiskController diskController;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //print("I'm looking at " + hit.transform.name);
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && interactable.CheckDistanceToFocus(player.transform))
            {
                //interactable.OnFocused(player.transform);
                player.GetComponent<PlayerController>().FocusObject(interactable);
            }
            else
            {
                if (player.GetComponent<PlayerController>().HasFocusedObject)
                {
                    player.GetComponent<PlayerController>().UnfocusObject();
                }
                //diskController.SetTarget(hit.transform.position);
            }
        }
        else
        {
            //print("I'm looking at nothing!");
            //diskController.SetTarget(cam.transform.forward * Mathf.Infinity);
        }
    }
}