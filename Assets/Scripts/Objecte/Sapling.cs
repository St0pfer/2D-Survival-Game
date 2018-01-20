using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sapling : MonoBehaviour {

    public string  tree;
    public float minute;
    public float hour;
    public int day;
    public float speed = 1f;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.parent == null)
        {

            // Tages Stunden und Minutenzähler
            minute += Time.deltaTime *speed;
            if (minute > 59)
            {
                hour++;
                minute = 0;
            }
            if (hour > 23)
            {
                day++;
                hour = 0;
            }

            if (hour>0)
            { 
                int random = Random.Range(1, 2);
                if (random == 1)
                    tree = "Beech";
                if (random == 2)
                    tree = "Firtree";

                GameObject New = Instantiate(Prefabliste.Instance().GetGameObject(tree), new Vector3(0, 0, 0), Quaternion.identity);
                New.transform.position = this.transform.position;
                New.name = tree;
                Destroy(this.gameObject);
            }

        }
	}
}
