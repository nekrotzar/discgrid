using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float lookSensitivity = 3.0f;
    [SerializeField]
    private DiskController disk;

    private PlayerMotor motor;

    private Interactable focusedObject;
    private bool hasFocusedObject = false;
    public bool HasFocusedObject
    {
        get { return hasFocusedObject; }
        set { hasFocusedObject = value; }
    }

    private void Start()
    {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

        motor = GetComponent<PlayerMotor>();

        Physics.IgnoreCollision(disk.gameObject.GetComponent<Collider>(), GetComponent<CapsuleCollider>());
    }

    private void Update()
    {
		// Kill player if he falls beneath a determined height
		if (transform.position.y <= -30){
			GameManager.instance.KillPlayer ();
		}

        // Calculate movement velocity as a 3D vector
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 hMovement = transform.right * xMovement;
        Vector3 vMovement = transform.forward * zMovement;

        Vector3 velocity = (hMovement + vMovement).normalized * speed;

        motor.Move(velocity);

        // Calculate rotation as a 3D vector (turning around)
        float yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0.0f, yRotation, 0.0f) * lookSensitivity;

        // Apply Rotation
        motor.Rotate(rotation);


        // Calculate camera rotation as a 3D vector (turning around)
        float xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRotation, 0.0f, 0.0f) * lookSensitivity;

        // Apply camera rotation
        motor.RotateCamera(cameraRotation);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inventory.instance.UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Inventory.instance.UseItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Inventory.instance.UseItem(3);
        }
			
        if (Input.GetButtonDown("Fire1") && disk != null)
        {
            disk.TryToThrow();
        }

        if (focusedObject != null && Input.GetButtonDown("Pick"))
        {
            if (focusedObject.CheckIfAndInteract())
            {
                hasFocusedObject = false;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name == "Disk")
    //    {

    //    }
    //}

    public void FocusObject(Interactable objectToFocus)
    {
        focusedObject = objectToFocus;
        focusedObject.OnFocused(transform);
        hasFocusedObject = true;
    }

    public void UnfocusObject()
    {
        focusedObject.OnDefocused();
        hasFocusedObject = false;
    }
}
