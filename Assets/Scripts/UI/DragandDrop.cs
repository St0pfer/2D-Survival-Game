using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class DragandDrop : MonoBehaviour
{
    public Inventar myInventar;
    public Mouse myMouse;
    public Crafting myCrafting;
    public GameObject Handwerk;
    public GameObject Mouse;
    public GameObject Rightarm;
    public GameObject Leftarm;
    private Transform Mousechild;
    private Transform Dropchild;
    private string Craftslotname;
    private Transform Child;


    // Use this for initialization
    void Start ()
    {
        Rightarm = GameObject.Find("Rightarm");
        Leftarm = GameObject.Find("Leftarm");
        Mouse = GameObject.Find("Mouse");
        myMouse = Mouse.GetComponent<Mouse>();
        Handwerk = GameObject.Find("Handwerk");
        myCrafting = Handwerk.GetComponent<Crafting>();
        myInventar = Handwerk.GetComponent<Inventar>();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnMouseOver()
    {

        // Stack Drag and Drop-------------------------------------------------------------------------

        // Drag
        int childcounterR = Rightarm.transform.childCount;
        int childcounterL = Leftarm.transform.childCount;
        int mousechildcounter = Mouse.transform.childCount;

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && mousechildcounter == 0)
        {

            GameObject ObjectSlot = this.gameObject;
            int childcounter = ObjectSlot.transform.childCount;
            int childcounterM = Mouse.transform.childCount;

            // All
            if (childcounter > 1 && childcounterM == 0 && !Input.GetMouseButtonDown(1)
                && !Input.GetKey(KeyCode.LeftControl))
            {    
                ItemAmount(childcounter, 0, 0, null, ObjectSlot);
            }
            // Single
            if (Input.GetMouseButtonDown(1) && childcounter > 1 && childcounterM == 0 
                && !Input.GetKeyDown(KeyCode.LeftControl) && !Input.GetMouseButtonDown(0))
            {
                ItemAmount(2, 0, 0, null, ObjectSlot);
            }
            // Split
            if (Input.GetKey(KeyCode.LeftControl) && childcounter > 1 && childcounterM == 0 
                && !Input.GetMouseButtonDown(1))
            {
                ItemAmount((childcounter+1)/2, 0, 0, null, ObjectSlot);
            }
            // Hände draggen
            if(childcounterR>0 && childcounterM == 0 && ObjectSlot.name.Contains("Rightarm"))
            {
                ItemAmount(2, 0, 0, null, Rightarm);
            }
            if (childcounterL > 0 && childcounterM == 0 && ObjectSlot.name.Contains("Leftarm"))
            {
                ItemAmount(2, 0, 0, null, Leftarm);
            }
        }

        // Drop

        if ((Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) && mousechildcounter > 0)
        {
            // UI von Name wegschneiden

            GameObject Target = this.gameObject;
            int childcounter = Target.transform.childCount;
            int childcounterM = Mouse.transform.childCount;

            // Item von TargetName wegschneiden
            string NameTarget = Target.name.Substring(0, Target.name.Length-6);

            if (childcounter > 1)
            {
                    Dropchild = Target.gameObject.transform.GetChild(1);               
            }
            if(childcounterM >0)
            {
                    Mousechild = Mouse.gameObject.transform.GetChild(0);
            }
            // Singledrop
            if(!Target.CompareTag("Armour") && !Target.name.Contains("arm") && childcounter == 1)
            {
                ItemAmount(0, childcounterM, 0, Target, null);
            }
            // Gleiche Items stacken
            if (!Target.CompareTag("Armour") && !Target.name.Contains("arm") && childcounter > 1 && Mousechild.name == Dropchild.name)          
            {
                ItemAmount(0, childcounterM, 0, Target, null);
            }
            // Alle Verschiedene Items switchen
            if (!Target.CompareTag("Armour") && !Target.name.Contains("arm") && childcounter >1 && childcounterM >0 && Mousechild.name != Dropchild.name)              
            {
                ItemAmount(0,childcounterM,childcounter, Target, null);
            }
            // Switch zwischen Maus und Rightarm
            if (Target.name.Contains("Rightarm") && childcounterM > 0 && childcounterR>0)
            {
                ItemAmount(0, childcounterM, 2, Rightarm, null);
            }
            // Switch zwischen Maus und Leftarm
            if (Target.name.Contains("Leftarm") && childcounterM > 0 && childcounterL > 0)
            {
                ItemAmount(0, childcounterM, 2, Leftarm, null);
            }
            // Switch Equipment
            if (Target.CompareTag("Armour") && childcounter > 1 && childcounterM > 0 && Mousechild.name.Contains(NameTarget))
            {
                ItemAmount(0, childcounterM, childcounter, Target, null);
            }
            // Equpiment anlegen                              
            if (Target.tag == Mousechild.tag && childcounter == 1 && Mousechild.name.Contains(NameTarget))
            {
                ItemAmount(0, 1, 0, Target, null);
            }
            // Hände droppen
            if(Target.name.Contains("RightarmS") && childcounter == 1 && childcounterR == 0)
            {
                ItemAmount(0, 1, 0, Rightarm, null);
            }
            if (Target.name.Contains("LeftarmS") && childcounter == 1 && childcounterL == 0)
            {
                ItemAmount(0, 1, 0, Leftarm, null);
            }
        }
    }

    //  Anzahl Item Handling

    private void ItemAmount (int childcounter, int childcounterM, int childcounterswitch, GameObject Target, GameObject ObjectSlot)
    {
        int childindex = 0;
        if(ObjectSlot !=null && ObjectSlot.name.Contains("arm"))
        { childindex = 0; }
        if (Target != null && Target.name.Contains("arm"))
        { childindex = 0; }      
        if (ObjectSlot != null && ObjectSlot.name.Contains("Item"))
        { childindex = 1; }
        if (Target != null && Target.name.Contains("Item"))
        { childindex = 1; }

        // Drag
        for (int i = 1; i < childcounter; i++)
        {
                Child = ObjectSlot.gameObject.transform.GetChild(childindex);
                DeActivateSpriteChilds(Child.gameObject);
                Child.transform.SetParent(Mouse.transform);
                Child.GetComponent<Collider2D>().enabled = false;
                Child.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                Child.transform.localScale = new Vector3(1, 1, 1);
                Vector3 mousePos = Input.mousePosition;
                float x = mousePos.x;
                float y = mousePos.y;
                mousePos = new Vector3(x + 30, y - 20, +5);
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Child.transform.position = mousePos;
                var spritetake = Child.GetComponent<SpriteRenderer>();
                spritetake.sortingOrder = 0;
                if (ObjectSlot.CompareTag("Armour"))
                {
                    Equipment.Instance.UnEquipItem(Child.gameObject);
                }     
        }

        // Drop
        for (int i = 0; i < childcounterM; i++)
        {
            Mousechild = Mouse.gameObject.transform.GetChild(0);
            var spritetake = Mousechild.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 0;
            DeActivateSpriteChilds(Mousechild.gameObject);
            Mousechild.transform.SetParent(Target.transform);
            Mousechild.transform.localPosition = new Vector3(0, 0, 0);
            Mousechild.transform.localScale = new Vector3(1, 1, 1);
            Mousechild.GetComponent<Collider2D>().enabled = false;
            if (Mousechild.CompareTag("Armour") && Target.CompareTag("Armour"))
            {
                Equipment.Instance.EquipItem(Mousechild.gameObject);
            }
            if(Target.name.Contains("Leftarm") || Target.name.Contains("Rightarm"))
            {
                ActivateSpriteChilds(Mousechild.gameObject);
            }
        }
        // Switch von Slot zwischen Maus
        for (int i = 1; i < childcounterswitch; i++)
        {
            Dropchild = Target.gameObject.transform.GetChild(childindex);
            Dropchild.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Dropchild.transform.SetParent(Mouse.transform);
            Dropchild.transform.localPosition = new Vector3(0.12f, -0.12f, 0);
            var spritetake = Dropchild.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 0;
        }
    }

    public void ActivateSpriteChilds(GameObject Parent)
    {
        int childcounterP = Parent.transform.childCount;

        if(childcounterP>0)
        {
            for (int i = 0; i < childcounterP; i++)
            {
                Transform Child = Parent.transform.GetChild(i);
                Child.GetComponent<SpriteRenderer>().enabled = true;
                print(Child.name);
                ActivateSpriteChilds(Child.gameObject);
            }
        }
    }
    public void DeActivateSpriteChilds(GameObject Parent)
    {
        int childcounterP = Parent.transform.childCount;

        if (childcounterP > 0)
        {
            for (int i = 0; i < childcounterP; i++)
            {
                Transform Child = Parent.transform.GetChild(i);
                Child.GetComponent<SpriteRenderer>().enabled = false;
                print(Child.name);
                DeActivateSpriteChilds(Child.gameObject);
            }
        }
    }
}

