using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour {

    public GameObject UI;
    public GameObject Respawnpoint;

    // Use this for initialization
    void Start ()
    {
        Respawnpoint = GameObject.Find("Respawnpoint");
        UI.SetActive(false);      
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSpawnpoint()
    {
        Respawnpoint.transform.position = this.transform.position;
        UI.SetActive(false);
    }

    public void ExitSpawnmenu()
    {
        UI.SetActive(false);
    }

    public void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
            UI.SetActive(true);

    }
}
