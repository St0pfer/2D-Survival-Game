using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchwimmerAnimation : MonoBehaviour {

    public Animator animatorSwim;
    public FishingRod myFishingRod;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Schwimmeranimation();

    }

    public void Schwimmeranimation()
    {
        if (myFishingRod.reactiontimer > 0f && myFishingRod.reactiontimer < 2f)
            animatorSwim.SetBool("schwimmer", true);
        else
            animatorSwim.SetBool("schwimmer", false);
    }
}
