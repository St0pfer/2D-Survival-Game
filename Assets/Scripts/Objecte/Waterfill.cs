using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfill : MonoBehaviour {

    public Ctrl myCtrl;
    private GameObject Charakter;
    public GameObject Rightarm;
    public int childcounterR;
    private Transform Child;
    private Transform ChildofChild;
    public GameObject SoundManager;

    // Use this for initialization
    void Start ()
    {
        SoundManager = GameObject.Find("SoundManager");
        Charakter = GameObject.Find("Charakter");
        myCtrl = Charakter.GetComponent<Ctrl>();
        Rightarm = GameObject.Find("Rightarm");
	}
	
	// Update is called once per frame
	void Update ()
    {
        childcounterR = Rightarm.transform.childCount;
        if (childcounterR > 0)
        {
            Child = Rightarm.gameObject.transform.GetChild(0);
            int childcounterC = Child.childCount;
          //  if(childcounterC>0)
           // ChildofChild = Child.gameObject.transform.GetChild(0);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("waterfillable") && myCtrl.Wftiming == true && myCtrl.Wftimer < 0.5f)
        {
            string name = Child.name;
            Destroy((Child as Transform).gameObject);
            GameObject NewObj = Instantiate(Prefabliste.Instance().GetGameObject(name +"_water"), new Vector3(0, 0, 0), Quaternion.identity);
            NewObj.name = NewObj.name.Replace("(Clone)", "");
            NewObj.transform.position = col.transform.position;
            NewObj.transform.eulerAngles = new Vector3(0, 0, +30);
            NewObj.transform.SetParent(Rightarm.transform);
            var spritetake = NewObj.GetComponent<SpriteRenderer>();
            spritetake.sortingOrder = 10;

            if (myCtrl.Wftimer < 0.5)
            {
                SoundManager.SendMessage("PlaySound", "waterfill");
            }
        }
    }
}
