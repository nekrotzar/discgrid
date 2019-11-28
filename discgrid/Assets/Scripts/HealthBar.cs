using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	float maxHealth = 100.0f;
	public RectTransform healthBarFill;
	public Text healthText;

	public static float health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		healthText.text = ((int) maxHealth).ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//health -= 0.1f;

		////FIXME: This is just for debugging
		//if (health < 0)
		//	health = 0;

		//float ratio = health / maxHealth; 
		//healthBarFill.localScale = new Vector3 (ratio, 1f, 1f);
		//healthText.text = ((int) health).ToString(); 
	}

    public void UpdateHealth(float hitDamage)
    {
        health -= hitDamage;

        //FIXME: This is just for debugging
		if (health <= 0) {
			health = 0;
			GameManager.instance.KillPlayer ();
		}

        float ratio = health / maxHealth;
        healthBarFill.localScale = new Vector3(ratio, 1f, 1f);
        healthText.text = ((int)health).ToString();
    }
}
