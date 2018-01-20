using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject Charakter;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name != "Rightarm" && col.name != "Leftarm")
        {
            GameObject Col = GameObject.Find(col.name);
            Col.SendMessage("TakeDamage", 10);
        }
    }
}
