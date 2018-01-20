using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

    public int Zahl = 1;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Charakter")
            col.SendMessage("GCoins", Zahl);
            Destroy(this.gameObject);
    }
}
