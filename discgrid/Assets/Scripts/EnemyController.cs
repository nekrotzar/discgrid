using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float maxHealth = 100.0f;
	public float range = 15f;
	public float turnSpeed = 10f;
    public int ammo = 10;
    public float fireRate = 1.0f; // Rate of one bullet per x seconds

	public ParticleSystem Smoke;
	public ParticleSystem Explosion;
	public GameObject spawnPoint;
    public GameObject bulletPrefab;
    
	private GameObject[] bullets;
	private Transform target;

	private ParticleSystem smoke;
	private ParticleSystem explode;


	private float health;
    private int currentBulletIndex = 0;
    private bool hasStartedShoot = false;
	private bool hasSmoke = false;
	private bool isDead = false;
    private bool isFrozen = false;
    private bool isLocked = false;

	void Start ()
	{
        // Set current health to full health
		health = maxHealth;

        // Store bullet prefabs
        bullets = new GameObject[ammo];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(bulletPrefab) as GameObject;
            bullets[i].SetActive(false);
            bullets[i].transform.position = spawnPoint.transform.position;
            bullets[i].GetComponent<BulletController>().initPos = spawnPoint.transform;
            bullets[i].GetComponent<BulletController>().lastPos = spawnPoint.transform.position;
        }
	}
		
	// Update is called once per frame
	void Update ()
	{

        if (!isFrozen)
        {
            if(!isLocked)
            {
                // Rotate around Y axis if target not in range and not frozen
                transform.RotateAround(transform.position, transform.up, Time.deltaTime * turnSpeed);

                if(hasStartedShoot)
                {
                    StopCoroutine(Shoot());
                    hasStartedShoot = false;
                }

            }
            else
            {
                // Target is inside range. Look at target
                LookAtTarget();

                if(!hasStartedShoot)
                {
                    StartCoroutine(Shoot());
                    hasStartedShoot = true;
                }                
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            target = other.transform;
            isLocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            target = null;
            isLocked = false;
        }
    }

    void OnCollisionEnter(Collision collision)
	{
		GameObject gameObj = collision.gameObject;
		DiskController disk = gameObj.GetComponent<DiskController>();

		if (disk != null)
		{
			TakeDamage (disk.damage);
		}
	}

    IEnumerator Shoot()
    {
        while (isLocked)
        {
            bullets[currentBulletIndex].GetComponent<BulletController>().direction = (target.position - spawnPoint.transform.position).normalized;
            bullets[currentBulletIndex].GetComponent<BulletController>().travelling = true;
            bullets[currentBulletIndex++].SetActive(true);
            if (currentBulletIndex == 10)
            {
                currentBulletIndex = 0;
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

	// Take damage from a particular disk
	public void TakeDamage(float amount)
	{
		health -= amount;

		smoke = Instantiate<ParticleSystem> (Smoke, gameObject.transform.position, Quaternion.Euler(-90,0,0) ,gameObject.transform);
		smoke.Stop ();
		var main = smoke.main;

		// Create smoke if health drops beneath 50%
		if (health <= 50 && !hasSmoke) 
		{
			main.startColor = new ParticleSystem.MinMaxGradient (new Color (1.0f, 1.0f, 1.0f, 0.15f));
			main.loop = true;
			smoke.Play ();
			hasSmoke = true;
		}

		// Change smoke color if it has only 25 % of health left
		if (health / maxHealth <= 0.25)
		{
			main.startColor = new ParticleSystem.MinMaxGradient (new Color (1.0f, 1.0f, 1.0f, 0.75f));
			smoke.Play ();
		}

		// Destroy object
		if (health <= 0)
		{
			explode = Instantiate<ParticleSystem> (Explosion, gameObject.transform.position, Quaternion.Euler(-90,0,0));
			explode.Stop();
			main = explode.main;
			main.loop = false;
			main.duration = 1.0f;
			explode.Play();

			GameManager.instance.IncrementPlayerScore();

            StopAllCoroutines();
			Destroy(gameObject);
			Destroy(explode.gameObject, main.duration);
		}
	}

    public void TakeDamageOverTime(float timeAmount, float damageAmount)
    {
        StartCoroutine(_TakeDamageOverTime(timeAmount, damageAmount));
    }

    private IEnumerator _TakeDamageOverTime(float timeAmount, float damageAmount)
    {
        float entryTime = Time.timeSinceLevelLoad;
        while ((Time.timeSinceLevelLoad - entryTime) < timeAmount)
        {
            TakeDamage(damageAmount);

            Debug.Log("HEALTH: " + health);

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void TakeFrostDamage(float amount)
    {
        StartCoroutine(_TakeFrostDamage(amount));
    }

    private IEnumerator _TakeFrostDamage(float timeAmount)
    {
        isFrozen = true;

        Transform holdTarget = target;
        target = null;
        StopCoroutine(Shoot());
        hasStartedShoot = false;

        yield return new WaitForSeconds(timeAmount);

        isFrozen = false;
        target = holdTarget;
    }

    // Rotate enemy if player is inside range
    void LookAtTarget()
	{
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.time );
        //transform.forward = direction.normalized;
	}
		
	// Draw range guidlines
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}

}
