using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColor : MonoBehaviour {

    private Color tmp;
    private Color tmpE;
    public GameObject ItemTarget = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {

        if ((col.gameObject.GetComponent("Items") as Items) != null && ItemTarget == null)
        {
            tmp = col.GetComponent<SpriteRenderer>().color;
            col.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 255f);
            ItemTarget = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.gameObject.GetComponent("Items") as Items) != null)
        {
            tmpE = col.GetComponent<SpriteRenderer>().color;
            col.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 255f);
        }
        ItemTarget = null;
    }
}
