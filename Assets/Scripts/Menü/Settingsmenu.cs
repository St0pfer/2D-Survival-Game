using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settingsmenu : MonoBehaviour {

    public AudioMixer Audiomixer;
    public Text VolumeValue;
    public float volume;
    public Text SFXValue;
    public float sfx;
    public Text MusicValue;
    public float music;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Game")
        SetMusic(0.05f);
    }

    public void SetVolume(float volume)
    {
        float volumetxt = volume * 100;
        VolumeValue.text = volumetxt.ToString("0") + "%";
        if (volume == 0)
            volume = -80;
        else
        {
            volume = 20.0f * Mathf.Log10(volume);
        }
        Audiomixer.SetFloat("volume", volume);
    }

    public void SetSFX(float sfx)
    {
        float sfxtxt = sfx * 100;
        SFXValue.text = sfxtxt.ToString("0") + "%";
        if (sfx == 0)
            sfx = -80;
        else
        {
            sfx = 20.0f * Mathf.Log10(sfx);
        }
        Audiomixer.SetFloat("sfx", sfx);
    }

    public void SetMusic(float music)
    {
        float musictxt = music * 100;
        MusicValue.text = musictxt.ToString("0") + "%";
        if (music == 0)
            music = -80;
        else
        {
            music = 20.0f * Mathf.Log10(music);
        }
        Audiomixer.SetFloat("music", music);
    }



    public void SetFullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
}
