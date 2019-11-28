using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBox : MonoBehaviour
{
    public float hoverForce = 30.0f;
    public int signal = 1;

    private void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), GameManager.instance.player.GetComponent<CapsuleCollider>());
        Physics.IgnoreCollision(GetComponent<Collider>(), GameManager.instance.player.GetComponent<SphereCollider>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
			other.GetComponent<Rigidbody>().AddForce(Vector3.forward * signal * 6.0f, ForceMode.Acceleration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            signal = signal == 1 ? -1 : 1;
            Debug.Log(signal);
			other.GetComponent<Rigidbody>().AddForce(Vector3.back * -signal * hoverForce, ForceMode.VelocityChange);
        }
    }
}
