using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdownmenu : MonoBehaviour {

    public GameObject Dropdown;
    public Button  Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9,
                   Button10, Button11, Button12, Button13, Button14, Button15, Button16, Button17,
                   Button18, Button19, Button20;
    public static List<Button> Buttonliste;



    // Use this for initialization
    void Start ()
    {
        Buttonliste = new List<Button>(20);
        Buttonliste.Add(Button1);
        Buttonliste.Add(Button2);
        Buttonliste.Add(Button3);
        Buttonliste.Add(Button4);
        Buttonliste.Add(Button5);
        Buttonliste.Add(Button6);
        Buttonliste.Add(Button7);
        Buttonliste.Add(Button8);
        Buttonliste.Add(Button9);
        Buttonliste.Add(Button10);
        Buttonliste.Add(Button11);
        Buttonliste.Add(Button12);
        Buttonliste.Add(Button13);
        Buttonliste.Add(Button14);
        Buttonliste.Add(Button15);
        Buttonliste.Add(Button16);
        Buttonliste.Add(Button17);
        Buttonliste.Add(Button18);
        Buttonliste.Add(Button19);
        Buttonliste.Add(Button20);
        int counter = Rezepte.Buildinglist.Count;
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void ChangeDropmenu()
    {
        int Dropdownvalue = Dropdown.GetComponent<Dropdown>().value;

        if (Dropdownvalue == 0)
        {
            ResetButtons();
            Handwerk();
        }
        if (Dropdownvalue == 1)
        {
            ResetButtons();
            Werkzeuge();
        }
        if (Dropdownvalue == 2)
        {
            ResetButtons();
            Essen();
        }
        if (Dropdownvalue == 3)
        {
            ResetButtons();
            Kleidung();
        }
        if (Dropdownvalue == 4)
        {
            ResetButtons();
            Baupläne();
        }
        if (Dropdownvalue == 5)
        {
            ResetButtons();
            Waffen();
        }
        if (Dropdownvalue == 6)
        {
            ResetButtons();
            Other();
        }

    }

    public void Handwerk()
    {

    }
        
    public void Baupläne()
    {
        for (int i = 0; i < Rezepte.Buildinglist.Count; i++)
        {
            Buttonliste[i].gameObject.SetActive(true);
        }
    }

    public void Werkzeuge()
    {
        for (int i = 0; i < Rezepte.Werkzeugliste.Count; i++)
        {
            Buttonliste[i].gameObject.SetActive(true);
        }
    }

    public void Essen()
    {
        for (int i = 0; i < Rezepte.Essensliste.Count; i++)
        {
            Buttonliste[i].gameObject.SetActive(true);
        }
    }

    public void Kleidung()
    {
        for (int i = 0; i < Rezepte.Kleidungsliste.Count; i++)
        {
            Buttonliste[i].gameObject.SetActive(true);
        }
    }

    public void Waffen()
    {
        for (int i = 0; i < Rezepte.Weaponlist.Count; i++)
        {
            Buttonliste[i].gameObject.SetActive(true);
        }
    }
    public void Other()
    {
        for (int i = 0; i < Rezepte.Otherlist.Count; i++)
        {
            Buttonliste[i].gameObject.SetActive(true);
        }
    }
    public void ResetButtons()
    {
        for (int i = 0; i < 20; i++)
        {
            Buttonliste[i].gameObject.SetActive(false);
        }
    }
}
