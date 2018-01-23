using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackArms : MonoBehaviour {

    public Sprite SlotItem;
    public GameObject Rightarm;
    public GameObject Leftarm;
    public GameObject Slot;
    public GameObject Mouse;
    private Transform Child;
    private string childcounterstr;
    private int childcounterM;
    private int childcounterR;
    private int childcounterL;

    private int childcounter; //_
    public int _childcounter    
    {
        get
        {
            return childcounter; //_
        }
        set
        {
            if (value != _childcounter) //_
            {
                childcounter = value; //_
               // Invoke("UpdateSlot",0.1f);
                UpdateSlot();
            }
        }
    }
    private string childname; //_
    public string _childname
    {
        get
        {
            return childname; //_
        }
        set
        {
            if (value != _childname) //_
            {
                childname = value; //_                    
                                   // Invoke("UpdateSlot",0.1f);
                UpdateSlot();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Rightarm = GameObject.Find("Rightarm");
        Leftarm = GameObject.Find("Leftarm");
        Mouse = GameObject.Find("Mouse");
    }

    // Update is called once per frame
    void Update()
    {

        childcounterM = Mouse.transform.childCount;
        childcounterR = Rightarm.transform.childCount;
        childcounterL = Leftarm.transform.childCount;

        if (childcounterR > 0 && childcounterM >0)
        {
           Child = Rightarm.gameObject.transform.GetChild(0);
           _childname = Child.name;
        }
        if (childcounterL > 0 && childcounterM > 0)
        {
            Child = Leftarm.gameObject.transform.GetChild(0);
            _childname = Child.name;
        }

        if (this.name.Contains("Rightarm"))
        {
            Slot = Rightarm;
            _childcounter = Rightarm.transform.childCount;
        }
        else
        {
            Slot = Leftarm;
            _childcounter = Leftarm.transform.childCount;
        }
    }

    // Stack und Icons im Inventar anzeigen
    void UpdateSlot()
    {

        if (Slot.transform.childCount > 0) // c
        {
            childcounterstr = _childcounter.ToString(); // c
            this.GetComponentsInChildren<Text>()[0].text = childcounterstr;
            Transform ChildofSlot = Slot.gameObject.transform.GetChild(0);
            Sprite Spritereader = ChildofSlot.gameObject.GetComponent<Items>().Icon;
            ChildofSlot.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<Image>().sprite = Spritereader;
            if (this.name.Contains("Rightarm"))
            {
                Vector3 pos = Rightarm.transform.position;
                float x = pos.x;
                float y = pos.y;
                if(ChildofSlot.name == "Torch")
                {
                    ChildofSlot.transform.eulerAngles = new Vector3(0, 0, -60f);
                }
                ChildofSlot.transform.position = new Vector3(x, y - 0.1f, 0);
                ChildofSlot.GetComponent<Collider2D>().enabled = true;
                var spritetake = ChildofSlot.GetComponent<SpriteRenderer>();
                spritetake.sortingOrder = 10;
            }
            else
            {
                Vector3 pos = Leftarm.transform.position;
                float x = pos.x;
                float y = pos.y;
                if (ChildofSlot.name == "Torch")
                {
                    ChildofSlot.transform.eulerAngles = new Vector3(0, 0, -60f);
                }
                ChildofSlot.transform.position = new Vector3(x - 0.02f, y - 0.11f, 0);
                ChildofSlot.GetComponent<Collider2D>().enabled = true;
                var spritetake = ChildofSlot.GetComponent<SpriteRenderer>();
                spritetake.sortingOrder = 4;
            }
        }
        if (childcounter == 0) //_
        {
            childcounterstr = childcounter.ToString();
            this.GetComponentsInChildren<Text>()[0].text = childcounterstr;
            this.gameObject.GetComponent<Image>().sprite = SlotItem;
        }
    }
}
