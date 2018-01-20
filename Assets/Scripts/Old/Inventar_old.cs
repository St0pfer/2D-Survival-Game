using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventar_old : MonoBehaviour
{
    public Crafting myCrafting;
    public Mouse myMouse;
    public Transform SelectedItem;
    private Items myItems;
    public GameObject InventoryPanel;
    public GameObject Handwerk;
    private GameObject Leftarm;
    private GameObject Rightarm;
    private GameObject Invent;
    private Transform Childleftarm;
    private Transform Childrightarm;
    private GameObject slotnameC;
    private GameObject slotnamecursor;
    private GameObject slotnamecursorsave;
    public Sprite SlotBackground;
    public Sprite SlotItem;
    public Sprite SlotCursor;
    public int CursorIndex = 1;
    private int Cursorsave;
    public List<string> Inventorylist;
    public List<string> InventorylistTemp;
    public int Counter;
    public int childcounterI;
    public GameObject Ast, Axe, Pickaxe, Hoe, smallstone, Log, Gras;
    public Sprite Spritereader;
    public int CursorsaveCrafting;
    public string fromTemp;
    int loopcounter = 0;
    int loopcountertemp = 0;
    int count = 1;
    public int IndexofInventorylist;

    void Start()
    {
        InventoryPanel.SetActive(false);
        Leftarm = GameObject.Find("Leftarm");
        Rightarm = GameObject.Find("Rightarm");
        Invent = GameObject.Find("Inventar");
        Handwerk = GameObject.Find("Handwerk");
        Inventorylist = new List<string>(20);
        InventorylistTemp = new List<string>(20);
    }

    void Update()
    {
        int childcounterR = Rightarm.transform.childCount;
        int childcounterL = Leftarm.transform.childCount;
            childcounterI = Invent.transform.childCount;

        // Items Stacken

         if (InventoryPanel.activeSelf == true)
            {

            foreach (string Obj in Inventorylist)
            {
                string Objectname = Obj;

                if (loopcounter > 0)
                {
                    fromTemp = InventorylistTemp[loopcountertemp];
                }
                if (loopcounter > 0)
                {
                    if (Objectname == fromTemp)
                    {
                        count++;
                        IndexofInventorylist = Inventorylist.IndexOf(Obj);
                        int IndexofInventorylistTemp = IndexofInventorylist + 1;
                        string Slotname = "Item";
                        string name = Slotname + IndexofInventorylistTemp;
                        GameObject Slot = GameObject.Find(name);
                        Slot.gameObject.GetComponent<Image>().sprite = SlotBackground;
                        string countstr = count.ToString();
                        Slot.GetComponentsInChildren<Text>()[0].text = countstr;
                    }
                }
                InventorylistTemp.Add(Obj);
                loopcounter++;
                loopcountertemp = loopcounter - 1;
            }
            count = 1;
            loopcounter = 0;
            loopcountertemp = 0;
            InventorylistTemp.Clear();
        }
        



        // Item von rechter und linker Hand in Inventar aufnehmen 
        Transform Setparent = transform.parent;
        if (Input.GetKey(KeyCode.X) && childcounterR > 0 && childcounterL == 0 && childcounterI < 20)
        {
            Childrightarm = Rightarm.gameObject.transform.GetChild(0);
            var spritetake = Childrightarm.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 5;
            Childrightarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Childrightarm.transform.SetParent(Invent.transform);
            string childname = Childrightarm.name;
            Inventorylist.Add(childname);
        }
        if (Input.GetKey(KeyCode.X) && childcounterL > 0 && childcounterR == 0 && childcounterI < 20)
        {
            Childleftarm = Leftarm.gameObject.transform.GetChild(0);
            var spritetake = Childleftarm.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 5;
            Childleftarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Childleftarm.transform.SetParent(Invent.transform);
            Childleftarm.transform.localPosition = new Vector3(0,0, 1);
            Childleftarm.transform.eulerAngles = new Vector3(0, 0, 0);
            string childname = Childleftarm.name;
            Inventorylist.Add(childname);
        }
        if (Input.GetKey(KeyCode.X) && childcounterL > 0 && childcounterR > 0 && childcounterI < 20)
        {
            Childrightarm = Rightarm.gameObject.transform.GetChild(0);
            var spritetake = Childrightarm.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 5;
            Childrightarm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Childrightarm.transform.SetParent(Invent.transform);
            string childname = Childrightarm.name;
            Inventorylist.Add(childname);
        }

        // Item von Inventar in rechte Hand nehmen
            if (Input.GetKey(KeyCode.Y) && childcounterL == 0 && childcounterR == 0
                 && Input.GetKey(KeyCode.X) == false && Inventorylist.Count > 0
                 && CursorIndex <= Inventorylist.Count)

        {
            int CursorIndexRef = CursorIndex - 1;
            Transform Child = Invent.gameObject.transform.GetChild(CursorIndexRef);
            Child.transform.SetParent(Rightarm.transform);
            Inventorylist.RemoveAt(CursorIndexRef);
            Child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 pos = Rightarm.transform.position;
            float x = pos.x;
            float y = pos.y;
            transform.position = new Vector3(x, y - 0.2f, 1);
            transform.eulerAngles = new Vector3(0, 0, 0);
            var ScriptThatYouWant = Child.GetComponent<Items>();
            ScriptThatYouWant.isgrounded = false;
            var spritetake = Child.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 5;
        }
        // Anzahl Gegenstände im Inventar ermitteln und Icons den Slots zuweisen

        if (InventoryPanel.activeSelf == true)
        {
            if (Inventorylist.Count > 0)
            {
                    foreach (string List in Inventorylist)
                {
                    GameObject Child = GameObject.Find(List);
                    Counter ++;
                    Spritereader = Child.gameObject.GetComponent<Items>().Icon;
                    string Slotcount = Counter.ToString();
                    string Slotname = "Item";
                    string name = Slotname + Slotcount;
                    GameObject Slotsearch = GameObject.Find(name);
                    Slotsearch.gameObject.GetComponent<Image>().sprite = Spritereader;
                }
                Counter = 0;
            }
        }
              // Nicht belegt Slots aufräumen
               if (InventoryPanel.activeSelf == true)
              {
                  for (int j = 20; j > Inventorylist.Count; j--)
                  {
                    string Slotcount = j.ToString();
                    string Slotname = "Item";
                    string nameC = Slotname + Slotcount;
                    slotnameC = GameObject.Find(nameC);
                    slotnameC.gameObject.GetComponent<Image>().sprite = SlotItem;
                    slotnameC.GetComponentsInChildren<Text>()[0].text = "0";
                  }
               } 

        // Inventar Cursor------------------------------------------------------------------------

        // Rechts
        if (Input.GetKeyDown(KeyCode.RightArrow) && CursorIndex > 0 && CursorIndex < 24)
        {
            Cursorsave = CursorIndex;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "Slot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            CursorIndex++;
        }

        // Links
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CursorIndex > 1 && CursorIndex <= 24)
        {
            Cursorsave = CursorIndex;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "Slot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            CursorIndex--;
        }

        // Oben
        if (Input.GetKeyDown(KeyCode.UpArrow) && CursorIndex > 4)
        {
            Cursorsave = CursorIndex;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "Slot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            CursorIndex -= 4;
        }

        // Unten

        if (Input.GetKeyDown(KeyCode.DownArrow) && CursorIndex <=20)
        {
            if (CursorIndex <= 16)
            {
                Cursorsave = CursorIndex;
                string Cursorsavecount = Cursorsave.ToString();
                string Cursorslot = "Slot";
                string Cursorsaveslot = Cursorslot + Cursorsavecount;
                slotnamecursorsave = GameObject.Find(Cursorsaveslot);
                slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
            }
               CursorIndex += 4;           
        }

        // Cursor Inventar nach unten zu Handwerk
        if (Input.GetKeyDown(KeyCode.DownArrow) && CursorIndex > 16)
        {
            Cursorsave = CursorIndex;
            Cursorsave -= 16;
            string Cursorsavecount = Cursorsave.ToString();
            string Cursorslot = "CraftSlot";
            string Cursorsaveslot = Cursorslot + Cursorsavecount;
            slotnamecursorsave = GameObject.Find(Cursorsaveslot);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotBackground;
        }

            // Item von Cursor in Handwerk verschieben

            if (Input.GetKeyDown(KeyCode.Return) && childcounterI >0)
        {
            int CursorIndexRef = CursorIndex - 1;
            Transform Child = Invent.gameObject.transform.GetChild(CursorIndexRef);
            string childname = Child.name;
            Child.transform.SetParent(Handwerk.transform);
            Inventorylist.Remove(Inventorylist[CursorIndexRef]);

        }

        if (InventoryPanel.activeSelf == true)
            Cursorsprites();

        // Item von Cursor in Inventar verschieben

        if (Input.GetKeyDown(KeyCode.Return) && CursorIndex > 20       
            && childcounterI < 20
           )
        {
            int CursorCrafting = CursorsaveCrafting;
            CursorCrafting -= 1;
            Transform Child = Handwerk.gameObject.transform.GetChild(CursorCrafting);
            string childname = Child.name;
            string count = CursorsaveCrafting.ToString();
            string Craft = "CraftItem";
            string name = Craft + count;
            GameObject Temp = GameObject.Find(name);
          /*  if (Crafting.Resourcenliste.Count == 0)
            {
                Temp.gameObject.GetComponent<Image>().sprite = SlotItem;
            }
            else
            {
                Temp.gameObject.GetComponent<Image>().sprite = SlotItem;
                myCrafting.Spritechanger();
            }
            Child.transform.SetParent(Invent.transform);      
            Inventorylist.Add(childname);*/
        }
    }

    // Cursor Sprites ändern
    public void Cursorsprites()
    {
        if (CursorIndex <= 20 && CursorIndex >0)
        {
            string Cursorcount = CursorIndex.ToString();
            string Cursorslot = "Slot";
            string Cursorslotname = Cursorslot + Cursorcount;
            slotnamecursor = GameObject.Find(Cursorslotname);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotCursor;
        }
        if (CursorIndex > 20)
        {
            CursorsaveCrafting = CursorIndex;
            CursorsaveCrafting -= 20;
            string Cursorcount = CursorsaveCrafting.ToString();
            string Cursorslot = "CraftSlot";
            string Cursorslotname = Cursorslot + Cursorcount;
            slotnamecursor = GameObject.Find(Cursorslotname);
            slotnamecursor.gameObject.GetComponent<Image>().sprite = SlotCursor;
        }
    }
}
            

      
        
   

