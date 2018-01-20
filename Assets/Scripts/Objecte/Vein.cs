using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vein : MonoBehaviour {

    public int day;
    public string vein;
    public float minute;
    public float hour;
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

            if (hour > 0)
            {
                    GameObject New = Instantiate(Prefabliste.Instance().GetGameObject(vein), new Vector3(0, 0, 0), Quaternion.identity);
                    New.transform.position = this.transform.position;
                    New.name = vein;
                    Destroy(this.gameObject);
                }
            }
        }
    }
