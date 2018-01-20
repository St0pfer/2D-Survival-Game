using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

    public GameObject CoinTxt;
   
    public int Gesamt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CoinTxt.GetComponent<Text>().text = Gesamt.ToString("#0");
	}

    public void GCoins(int Zahl)
    {
        Gesamt = Gesamt + Zahl;
    }
}
