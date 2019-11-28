using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float motionRange = 10.0f;
    public float damageFactor = 10.0f;
    public float speed = 10.0f;

    public Transform initPos;

    public Vector3 lastPos;
    public Vector3 direction;
    private float distanceTravelled = 0.0f;
    public bool travelling = false;

    private void Update()
    {
        if (travelling)
        {
            Vector3 travelFactor = direction * speed * Time.deltaTime;
            transform.position += travelFactor;
            distanceTravelled += Vector3.Distance(lastPos, transform.position);
            if (distanceTravelled > motionRange)
            {
                ResetBullet();
            }

            lastPos = transform.position;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.name == "Player")
        {
            GameManager.instance.DealPlayerDamage(damageFactor);
        }
        ResetBullet();
    }

    private void ResetBullet()
    {
        transform.position = initPos.position;
        lastPos = initPos.position;
        distanceTravelled = 0.0f;
        travelling = false;
        gameObject.SetActive(false);
    }
}
