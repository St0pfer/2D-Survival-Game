using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackChest : MonoBehaviour {


    public Sprite SlotItem;
    public GameObject UI;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Stack und Icons im Inventar anzeigen

        if (UI.activeSelf)
        {   

            int childcounter = this.transform.childCount;
            int childcountertxt = childcounter - 1;
            string childcounterstr = childcountertxt.ToString();

            if (childcounter > 1)
            {
                this.GetComponentsInChildren<Text>()[0].text = childcounterstr;
                Transform ChildthisSlot = this.gameObject.transform.GetChild(1);
                Sprite Spritereader = ChildthisSlot.gameObject.GetComponent<Items>().Icon;
                ChildthisSlot.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<Image>().sprite = Spritereader;
            }
            if (childcounter == 1)
            {
                this.GetComponentsInChildren<Text>()[0].text = childcounterstr;
                this.gameObject.GetComponent<Image>().sprite = SlotItem;
            }
        }
    }
}