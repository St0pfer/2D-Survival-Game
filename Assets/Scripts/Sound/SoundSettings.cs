using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings : MonoBehaviour {

    public AudioSource AudioSource;
    public AudioSource AudioSource2;
    public AudioSource AudioSource3;
    public AudioSource Music;
    public AudioClip axt, treefall, bite, fishstruggle, Throw, waterfill, brokentool,
                     closechest, drinking, eating, fireout, firesound, flop, heal, openchest,
                     startfire, stonebreak,pickaxe;
    public AudioClip clip;
    public AudioClip Track1, Track2, Track3, Track4;
    public List<AudioClip> Player = new List<AudioClip>();

	// Use this for initialization
	void Start ()
    {
        AudioSource = GetComponent<AudioSource>();
        Player.Add(Track1);
        Player.Add(Track2);
        Player.Add(Track3);
        Player.Add(Track4);

    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayMusic();

    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "axt": AudioSource.clip = axt; break;
            case "pickaxe": AudioSource.clip = pickaxe; break;
            case "stonebreak": AudioSource.clip = stonebreak; break;
            case "treefall": AudioSource.clip = treefall; break;
            case "bite": AudioSource.clip = bite; break;
            case "fishstruggle": AudioSource.clip = fishstruggle; break;
            case "throw": AudioSource.clip = Throw; break;
            case "waterfill": AudioSource.clip = waterfill; break;
            case "brokentool": AudioSource.clip = brokentool; break;
            case "closechest": AudioSource.clip = closechest; break;
            case "drinking": AudioSource.clip = drinking; break;
            case "eating": AudioSource.clip = eating; break;
            case "fireout": AudioSource.clip = fireout; break;
            case "firesound": AudioSource.clip = firesound; break;
            case "flop": AudioSource.clip = flop; break;
            case "heal": AudioSource.clip = heal; break;
            case "openchest": AudioSource.clip = openchest; break;
            case "startfire": AudioSource.clip = startfire; break;
            case "stop": AudioSource.Stop(); break;

            default: Debug.Log("error"); break;
        }
        if (!AudioSource.isPlaying)
            AudioSource.Play();
        if(AudioSource.isPlaying && !AudioSource2.isPlaying)
        {
            AudioSource2.clip = AudioSource.clip;
            AudioSource2.Play();
        }
        if(AudioSource.isPlaying && AudioSource2.isPlaying && !AudioSource3.isPlaying)
        {
            AudioSource3.clip = AudioSource.clip;
            AudioSource3.Play();
        }
    }

    public void PlayMusic()
    {
        if (!Music.isPlaying)
        {
            int random = Random.Range(1, 4);

            AudioClip Clip = Player[random];
            Music.clip = Clip;
            Music.Play();
        }
    }

}
