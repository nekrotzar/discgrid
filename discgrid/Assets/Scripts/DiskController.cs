using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DiskType
{
    NORMAL,
    FROST,
    FIRE
}

[RequireComponent(typeof(Rigidbody))]
public class DiskController : MonoBehaviour
{
	public float speed = 5.0f;
	public float damage = 10.0f;
	public float catchRadius = 0.5f;
	public float maxThrowDistance = 30.0f;

    public Item disk;

	public Camera fpsCamera;
	public GameObject player;

	public GameObject glowComponent;

	private Vector3 targetDirection;
	private Vector3 localPosition;
	private Quaternion localRotation;

	private bool isThrown = false;

    public bool IsThrown
    {
        get { return IsThrown; }
    }
	private bool isCatchable = false;

	private Rigidbody rigidBody;

    private MeshRenderer meshRenderer;

    private DiskType diskType = DiskType.NORMAL;

    private int ownerTriggerCounter = 0;

	void Start()
	{
		// Get relative transfomr of disk to camera
		localPosition = transform.localPosition;
		localRotation = transform.localRotation;

		// Set the rigidboy to avoid collisions with player
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.isKinematic = true;
		rigidBody.detectCollisions = false;

        meshRenderer = GetComponent<MeshRenderer>();

        Inventory.instance.Add(disk);
	}

	void OnCollisionEnter(Collision collision)
	{
        /*float distanceToPlayer = Vector3.Distance (transform.position, fpsCamera.transform.position);

		if (distanceToPlayer > catchRadius) 
			isCatchable = true;*/

        if (collision.collider.tag == "Enemy")
        {
            switch (diskType)
            {
                case DiskType.NORMAL:
                    collision.collider.GetComponent<EnemyController>().TakeDamage(damage);
                    break;
                case DiskType.FROST:
                    collision.collider.GetComponent<EnemyController>().TakeFrostDamage(disk.damageFactor);
                    break;
                case DiskType.FIRE:
                    collision.collider.GetComponent<EnemyController>().TakeDamageOverTime(5.0f, disk.damageFactor);
                    break;
                default:
                    break;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            ownerTriggerCounter++;
            if (ownerTriggerCounter == 2)
            {
                ResetDisk();
            }
        }
    }

    void Update()
	{
		// Rotate the disk around the y axis
		transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90.0f);


		// Check if thrown disk is in range with player
		if (isThrown && isCatchable)
        {
			ResetDisk ();
		}

		//// TODO: RETURN DISK BY PRESSING FIRE WHILE FLYING
		//if (Input.GetButtonDown ("Fire1")) 
		//{
		//	if (!isThrown) {
		//		Shoot ();
		//	} else {
		//		isCatchable = true;
		//	}
		//}
			
	}

    public void TryToThrow()
    {
        if (!isThrown)
        {
            Shoot();
        }
        else
        {
            isCatchable = true;
        }
    }

	void Shoot()
	{
		isThrown = true;
		// Remove the parent from disk and set the rigidboy to listern to collisions
		gameObject.transform.parent = null;
		rigidBody.isKinematic = false;
		rigidBody.detectCollisions = true;
		rigidBody.AddTorque (3000.0f, 400.0f, 0.0f);
		rigidBody.AddForce (fpsCamera.transform.forward * speed, ForceMode.Impulse);
	}

	public void ResetDisk()
	{
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
		rigidBody.detectCollisions = false;
		rigidBody.isKinematic = true;

		gameObject.transform.parent = fpsCamera.transform;

		transform.position = fpsCamera.transform.position;
		transform.rotation = fpsCamera.transform.rotation;
		transform.localPosition = localPosition;
		transform.localRotation = localRotation;

		isThrown = false;
		isCatchable = false;

        ownerTriggerCounter = 0;
    }

	// Draw range guidlines
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (fpsCamera.transform.position, catchRadius);
	}

    public void SetDisk(string name, Item _disk)
    {
        disk = _disk;
        switch (name)
        {
            case "Normal":
                diskType = DiskType.NORMAL;
                break;
            case "Frost":
                diskType = DiskType.FROST;
                break;
            case "Fire":
                diskType = DiskType.FIRE;
                break;
            default:
                break;
        }

		MeshRenderer mshRenderer = glowComponent.GetComponent < MeshRenderer> ();
		mshRenderer.material = disk.itemMaterial;
    }
}

