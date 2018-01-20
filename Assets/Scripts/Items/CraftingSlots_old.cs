using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingSlots_old : MonoBehaviour
{

    public Crafting myCrafting;
    public GameObject RezeptPanel;
    public List<string> Craftinglist;
    public int Counter;
    public Sprite Spritereader;
    public Sprite CraftSlotItem;
    public bool OverCraftSlot = false;
    public int childcounterCrafting;


    // Use this for initialization
    void Start ()
    {
        RezeptPanel.SetActive(false);
        Craftinglist = new List<string>(4);
    }

    // Update is called once per frame
    void Update()
    {

        childcounterCrafting = this.transform.childCount;

        // Anzahl Gegenstände im CraftingInventar ermitteln und Icons den Slots zuweisen

        if (RezeptPanel.activeSelf == true)
        {
            if (Craftinglist.Count > 0)
            {
                foreach (string List in Craftinglist)
                {
                    GameObject Child = GameObject.Find(List);
                    Counter++;
                    Spritereader = Child.gameObject.GetComponent<Items>().Icon;
                    string Slotcount = Counter.ToString();
                    string Slotname = "CraftItem";
                    string name = Slotname + Slotcount;
                    GameObject Slotsearch = GameObject.Find(name);
                    Slotsearch.gameObject.GetComponent<Image>().sprite = Spritereader;
                }
                Counter = 0;
            }
        }

 /*       // Nicht belegt Slots aufräumen
        if (RezeptPanel.activeSelf == true)
        {
            if (Crafting.Resourcenliste.Count == 0)
            {
                for (int j = 4; j > Craftinglist.Count; j--)
                {
                    string Slotcount = j.ToString();
                    string Slotname = "CraftItem";
                    string nameC = Slotname + Slotcount;
                    GameObject slotnameC = GameObject.Find(nameC);
                    slotnameC.gameObject.GetComponent<Image>().sprite = CraftSlotItem;
                }
            }            
        }   */     
    }
}

