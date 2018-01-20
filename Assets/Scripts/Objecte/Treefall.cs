using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treefall : MonoBehaviour {

    public Animator animatorTree;
    public AudioClip fallTree;
    public AudioSource audioSourceFall;
    public static int number;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Treefaller()
    {
        number = Random.Range(1, 10);

        if (number < 5)
        {
            audioSourceFall.PlayOneShot(fallTree, 0.05F);
            animatorTree.SetBool("fallleft", true);
        }
        else
        {
            audioSourceFall.PlayOneShot(fallTree, 0.05F);
            animatorTree.SetBool("fallright", true);
        }
        return number;
    }
}
