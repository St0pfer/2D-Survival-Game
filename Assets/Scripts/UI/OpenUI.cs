using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenUI : MonoBehaviour
{

    public GameObject UI;
    private Color tmp;

    // Use this for initialization
    void Start ()
    {
        UI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        tmp = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, 0.5f);
    }

    private void OnMouseExit()
    {
        tmp = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, 255f);
    }

    private void OnMouseOver()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, 0.5f);
        {
            if(Input.GetMouseButtonDown(0))
            {
                UI.SetActive(!UI.activeSelf);
            }
        }
    }

    public void ExitUI()
    {
        UI.SetActive(false);
    }
}
