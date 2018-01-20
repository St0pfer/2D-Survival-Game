using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

    private float Speed = 50f;
    private Vector2 Targetpos;
    private Vector2 Startpos;

    // Use this for initialization
    void Start ()
    {
        // Start Position
        Startpos = this.transform.localPosition = new Vector2(0, 800);
        Targetpos = new Vector2(+960, -360); // Faktor X 960 Y +540
    }
	
	// Update is called once per frame
	void Update ()
    {
        float step = Speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, Targetpos, step);

        if(Targetpos.y == this.transform.position.y)
        {
            this.transform.position = new Vector2(+960,1340);
        }
    }

}
