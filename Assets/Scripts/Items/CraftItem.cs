using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItem : MonoBehaviour {

    public Mouse myMouse;
    public GameObject Handwerk;
    public GameObject Maus;
    private Transform Mousechild;
    public int childcounterCrafting;
    public int Slotindex;
    public Transform This;
    public bool OverCraftSlot = false;

    // Use this for initialization
    void Start ()
    {
        Maus = GameObject.Find("Mouse");
        Handwerk = GameObject.Find("Handwerk");
    }
	
	// Update is called once per frame
	void Update ()
    {
        childcounterCrafting = Handwerk.transform.childCount;
    }

    void OnMouseOver()
    {
        // Item von Mouse Drag in Handwerk aufnehmen
        OverCraftSlot = true;     
        if (Input.GetMouseButtonUp(0) && myMouse.childcounterM > 0 && childcounterCrafting < 4)
        {
            Mousechild = Maus.gameObject.transform.GetChild(0);
            Mousechild.transform.SetParent(Handwerk.transform);
            Mousechild.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Mousechild.transform.localPosition = new Vector3(0, 0, 0);
            string childname = Mousechild.name;
        }

        // Drag

        if (Input.GetMouseButtonDown(0) && childcounterCrafting > 0 && myMouse.childcounterM == 0)
        {
            string Slotname = this.name;
            int Stringlenght = Slotname.Length;
            Slotindex = int.Parse(Slotname.Substring(9));

            if (Slotindex <= childcounterCrafting)
            {
                Slotindex--;
                This = Handwerk.gameObject.transform.GetChild(Slotindex);
                This.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                This.transform.SetParent(Maus.transform);
                Vector3 mousePos = Input.mousePosition;
                float x = mousePos.x;
                float y = mousePos.y;
                mousePos = new Vector3(x + 30, y - 20, +5);
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                This.transform.position = mousePos;
                var spritetake = This.GetComponent<SpriteRenderer>();
                spritetake.sortingOrder = 25;
            }
        }

    }

    private void OnMouseExit()
    {
        OverCraftSlot = false;
    }
}
