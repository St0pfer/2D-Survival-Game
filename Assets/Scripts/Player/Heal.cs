using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour {

    public bool isHealing;
    public float heal = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Charakter")
            //    col.SendMessage((isDamaging)?"TakeDamage": "HealDamage", Time.deltaTime * damage);
            col.SendMessage("HealDamage", Time.deltaTime * heal);
    }
}
