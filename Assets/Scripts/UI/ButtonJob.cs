using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonJob : MonoBehaviour
{
    public Crafting myCrafting;
    public Dropdownmenu myDropdownmenu;
    public Button Button;
    public GameObject Handwerk;
    public GameObject DropDownMenu;
    

    // Use this for initialization
    void Start()
    {
        DropDownMenu = GameObject.Find("Dropdown");
        Handwerk = GameObject.Find("Handwerk");
        myCrafting = Handwerk.GetComponent<Crafting>();
        myDropdownmenu = DropDownMenu.GetComponent<Dropdownmenu>();
        Button = this.GetComponent<Button>();
        Button.onClick.AddListener(Buttonjob);
        Button.onClick.AddListener(myCrafting.SelectRecipe);
        Button.onClick.AddListener(myCrafting.SelectRecipe);
        Button.onClick.AddListener(myCrafting.ProcessRecipeResources);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buttonjob()
    {
        string text = this.GetComponentInChildren<Text>().text;
        int length = text.Length -6;
        string withoutLvL = text.Substring(0, length);
        Crafting.TextofButton = withoutLvL;
    }

}