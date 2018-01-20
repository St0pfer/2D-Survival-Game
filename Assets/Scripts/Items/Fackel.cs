using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fackel : MonoBehaviour {

    public Animator animator;
    public Light Light;
    public GameObject LightGO;
    public GameObject SoundManager;
    public bool lighton;
    public float switcher;
    public AudioSource AudioSource;
    public AudioClip fire;

	// Use this for initialization
	void Start ()
    {
        SoundManager = GameObject.Find("SoundManager");
        switcher = 1;
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (lighton)
        {
            LightGO.SetActive(true);
            SoundManager.SendMessage("PlaySound", "firesound");
            animator.SetBool("burn", true);
        }
        else
        {
            animator.SetBool("burn", false);
            LightGO.SetActive(false);
        }
        if(switcher <= 1 && switcher >0)
            Light.intensity = 100f;
        if (switcher < 0 && switcher > -1f)
        {
            Light.intensity = 90f;
        }
        if (switcher < -1)
            switcher = 1;

            switcher -= Time.deltaTime ;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tinderdrill"))
        {
            lighton = true;
        }
    }
}
