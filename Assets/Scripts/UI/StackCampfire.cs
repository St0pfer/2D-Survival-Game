using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackCampfire : MonoBehaviour {

    public Sprite SlotItem;
    public GameObject UI;
    public Sprite Ast_grau;
    public Sprite Keule_grau;
    public Sprite Erz_grau;
    private Transform ChildthisSlot;
    public GameObject Fuel;
    public GameObject Roast;
    public GameObject RoastRdy;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
 
        int childcounter = this.transform.childCount;
        if (childcounter > 1)
        {
            ChildthisSlot = this.gameObject.transform.GetChild(1);

        }

        // Stack und Icons im Inventar anzeigen

        if (UI.activeSelf)
        {
            string childcounterstr = (childcounter - 1).ToString();

            if (childcounter > 1) //>0
            {
                this.GetComponentsInChildren<Text>()[0].text = childcounterstr;

                foreach (Transform Child in this.transform)
                {
                    if (Child.name != "Text")
                    {
                        if (this.name == "RoastSlotItemUI" || this.name == "MeltSlotItemUI")
                        {
                            Child.transform.localPosition = new Vector3(0, 0, 1);
                            Roast.GetComponent<SpriteRenderer>().enabled = true;
                            Roast.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                            var spritetake = Roast.GetComponent<SpriteRenderer>();
                            Sprite Sprite = Child.GetComponent<SpriteRenderer>().sprite;
                            Roast.GetComponent<SpriteRenderer>().sprite = Sprite;
                            spritetake.sortingOrder = 12;
                        }
                        if (this.name == "RoastRdySlotItemUI" || this.name == "MeltRdySlotItemUI")
                        {                          
                            Child.GetComponent<SpriteRenderer>().enabled = false;
                            Child.transform.localPosition= new Vector3(0, 0, 1);
                            RoastRdy.transform.localScale = new Vector3(0.5f, +0.5f, 1);
                            var spritetake = RoastRdy.GetComponent<SpriteRenderer>();
                            Sprite Sprite = Child.GetComponent<SpriteRenderer>().sprite;
                            RoastRdy.GetComponent<SpriteRenderer>().sprite = Sprite;
                            spritetake.sortingOrder = 12;
                        }
                        if (this.name == "FuelSlotItemUI")
                        {
                            Child.transform.localPosition = new Vector3(0, 0, 1);
                            Fuel.GetComponent<SpriteRenderer>().enabled = true;
                            if(Child.name == "Log")
                            Fuel.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                            else
                            Fuel.transform.localScale = new Vector3(1, 1, 1);
                            var spritetake = Fuel.GetComponent<SpriteRenderer>();
                            Sprite Sprite = Child.GetComponent<SpriteRenderer>().sprite;
                            Fuel.GetComponent<SpriteRenderer>().sprite = Sprite;
                            spritetake.sortingOrder = 11;
                        }                   
                        Transform ObjTemp = this.gameObject.transform.GetChild(1);
                        Sprite Spritereader = ObjTemp.gameObject.gameObject.GetComponent<Items>().Icon;
                        this.gameObject.GetComponent<Image>().sprite = Spritereader;
                    }
                }
            }
            if (childcounter == 1)
            {
                this.GetComponentsInChildren<Text>()[0].text = childcounterstr;
                if (this.name == "FuelSlotItemUI")
                {
                    this.gameObject.GetComponent<Image>().sprite = Ast_grau;
                    Fuel.GetComponent<SpriteRenderer>().sprite = null;
                }
                if (this.name == "RoastSlotItemUI")
                {
                    this.gameObject.GetComponent<Image>().sprite = Keule_grau;
                    Roast.GetComponent<SpriteRenderer>().sprite = null;
                }
                if (this.name == "MeltSlotItemUI")
                {
                    this.gameObject.GetComponent<Image>().sprite = Erz_grau;
                    Roast.GetComponent<SpriteRenderer>().sprite = null;
                }
                if(this.name == "MeltRdySlotItemUI")
                {
                    this.gameObject.GetComponent<Image>().sprite = SlotItem;
                }
                if (this.name == "RoastRdySlotItemUI")
                {
                    this.gameObject.GetComponent<Image>().sprite = SlotItem;
                }
                if (this.name == "RoastRdySlotItemUI" || this.name == "MeltRdySlotItemUI")
                {
                    RoastRdy.GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }
    }
}
