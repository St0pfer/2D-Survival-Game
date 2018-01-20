using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Mute : MonoBehaviour {

    public AudioMixer Audiomixer;
    public Sprite Default;
    public Sprite Muted;
    public bool mute = false;

    private void Start()
    {
        mute = true;
    }

    public void MuteVolume()
    {
        if (mute == false)
        {
            Audiomixer.SetFloat("volume", -80);
            GetComponent<Image>().sprite = Muted;
            mute = true;
        }
        else
        {
            Audiomixer.SetFloat("volume", 0);
            GetComponent<Image>().sprite = Default;
            mute = false;
        }
    }

}
