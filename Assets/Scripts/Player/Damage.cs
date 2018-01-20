using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour {

    public bool isDamaging;
    public float damage = 10;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Charakter")
            //    col.SendMessage((isDamaging)?"TakeDamage": "HealDamage", Time.deltaTime * damage);
            col.SendMessage("TakeDamage", Time.deltaTime * damage);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Charakter")
            col.SendMessage("EXP", 10);
    }
}

