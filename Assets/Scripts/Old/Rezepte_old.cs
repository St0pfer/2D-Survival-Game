using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rezepte_old : MonoBehaviour
{

    public Dropdownmenu myDropdown;
    public GameObject Dropdown;
    public GameObject Handwerk;
    public Sprite Axe,Pickaxe,Gras,Stein,Ast;
    public Transform Axt;
    public Transform Spitzhacke;
    public GameObject ResourceItem1, ResourceItem2, ResourceItem3, Description, CraftedItem, Rightarm;
    public int Index;


	// Use this for initialization
	void Start ()
    {
        Handwerk = GameObject.Find("Handwerk");
        Dropdown = GameObject.Find("Dropdown");
        myDropdown = Dropdown.GetComponent<Dropdownmenu>();       
        ResourceItem1 = GameObject.Find("ResourceItem1");
        ResourceItem2 = GameObject.Find("ResourceItem2");
        ResourceItem3 = GameObject.Find("ResourceItem3");
        Description = GameObject.Find("Description");
        CraftedItem = GameObject.Find("CraftedItem");
        Rightarm = GameObject.Find("Rightarm");
    }
	
	// Update is called once per frame
	void Update ()
    {
        Index = Dropdown.GetComponent<Dropdown>().value;
        if (Index == 1)
        {
            AxetoButton();
            PickaxetoButton();
        }
    }
    // Werkzeugrezepte
    void AxetoButton()
    {            
            myDropdown.Button1.GetComponentsInChildren<Text>()[0].text = "Axt";
            myDropdown.Button1.GetComponent<Button>();
            myDropdown.Button1.onClick.RemoveListener(RecipeAxe);
            myDropdown.Button1.onClick.AddListener(RecipeAxe);     
    }
    void PickaxetoButton()
    {
        myDropdown.Button2.GetComponentsInChildren<Text>()[0].text = "Pickaxe";
        myDropdown.Button2.GetComponent<Button>();
        myDropdown.Button2.onClick.RemoveListener(RecipePickaxe);
        myDropdown.Button2.onClick.AddListener(RecipePickaxe);
    }

    void RecipeAxe()
    {
        ResourceItem1.GetComponent<Image>().sprite = Gras;
        ResourceItem2.GetComponent<Image>().sprite = Stein;
        ResourceItem3.GetComponent<Image>().sprite = Ast;
        CraftedItem.GetComponent<Image>().sprite = Axe;
        Description.GetComponent<Text>().text = "Mit der Axt können Bäume gefällt werden und " +
                                                "Logs in Scheite aufgespalten werden. Zur Herstellung " +
                                                "benötigt 1xGras,1xStein,1xAst.";

       
        
            print("Alles ist drin");
            Instantiate(Axt, new Vector3(0, 0, 0), Quaternion.identity);
            Axt.transform.SetParent(Rightarm.transform);
            Axt.transform.localPosition = new Vector3(0,  0, 0);

    }


    void RecipePickaxe()
    {
        ResourceItem1.GetComponent<Image>().sprite = Gras;
        ResourceItem2.GetComponent<Image>().sprite = Stein;
        ResourceItem3.GetComponent<Image>().sprite = Ast;
        CraftedItem.GetComponent<Image>().sprite = Axe;
        Description.GetComponent<Text>().text = "Mit der Pickaxe können Steine abgebaut werden " +
                                                "Zur Herstellung benötigt 1xGras,1xStein,1xAst.";



        
            print("Alles ist drin");
            Instantiate(Axt, new Vector3(0, 0, 0), Quaternion.identity);
            Spitzhacke.transform.SetParent(Rightarm.transform);
            Spitzhacke.transform.localPosition = new Vector3(0, 0, 0);
        


            print("Es fehlt was");


    }
}


