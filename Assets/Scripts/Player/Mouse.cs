using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour {

    public int childcounterM;
    private Transform mouseTransform;
    public GameObject MouseSlot;


    // Use this for initialization
    void Start ()
    {
        mouseTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        childcounterM = this.transform.childCount;
        
        Vector3 mousePos = Input.mousePosition;
        float x = mousePos.x;
        float y = mousePos.y;
        mousePos = new Vector3(x, y, +5);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = mousePos;
        float xs = mousePos.x;
        float ys = mousePos.y;
        mousePos = new Vector3(xs + 0.1f, ys-0.1f, +5);
        MouseSlot.transform.position = mousePos;


        // Sprite des MouseChilds anzeigen
        string childcounterMstr = childcounterM.ToString();

        if (childcounterM > 0)
        {
            Transform Child = this.gameObject.transform.GetChild(0);
            Sprite Spritereader = Child.gameObject.GetComponent<Items>().Icon;
            MouseSlot.gameObject.GetComponent<Image>().sprite = Spritereader;
            MouseSlot.GetComponentsInChildren<Text>()[0].text = childcounterMstr;
        }
    }
}
