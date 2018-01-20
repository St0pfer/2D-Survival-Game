using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class StackInventory : MonoBehaviour {

    public Sprite SlotItem;

 
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Stack und Icons im Inventar anzeigen
   
            int childcounter = this.transform.childCount;
            int childcountertxt = childcounter -1;
            string childcounterstr = childcountertxt.ToString();
            GameObject Slot = GameObject.Find(this.name);

            if (childcounter > 1)
            {
                Slot.GetComponentsInChildren<Text>()[0].text = childcounterstr;
                Transform ChildthisSlot = this.gameObject.transform.GetChild(1);
                Sprite Spritereader = ChildthisSlot.gameObject.GetComponent<Items>().Icon;
                ChildthisSlot.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Slot.gameObject.GetComponent<Image>().sprite = Spritereader;
            }
            if (childcounter == 1)
            {
                Slot.GetComponentsInChildren<Text>()[0].text = childcounterstr;
                Slot.gameObject.GetComponent<Image>().sprite = SlotItem;
            }
        }

    }
