using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    public Settingsmenu mySettingsmenu;
    public GameObject Optionsmenu;
    public SaveGameSettings mySaveGameSettings;
    public GameObject SaveGameManager;
    public AudioMixer Audiomixer;
    public bool loaded;
    public float volume;
    public float sfx;
    public float music;
    public bool destroy = false;

    // Use this for initialization
    void Start()
    {
        mySettingsmenu = Optionsmenu.GetComponent<Settingsmenu>();
    }

    // Update is called once per frame
    void Update()
    {

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Start")
        {
            volume = mySettingsmenu.volume;
            sfx = mySettingsmenu.sfx;
            music = mySettingsmenu.music;
        }
        if (scene.name == "Game")
        {
            Audiomixer.SetFloat("volume", volume);
            Audiomixer.SetFloat("volume", sfx);
            Audiomixer.SetFloat("volume", music);
            if (loaded)
            {
                SaveGameManager = GameObject.Find("SaveGameManager");
                mySaveGameSettings = SaveGameManager.GetComponent<SaveGameSettings>();
                mySaveGameSettings.Load();
            }
            destroy = true;
        }

        if (destroy == false)
            DontDestroyOnLoad(transform.gameObject);
        if (scene.name == "Game" && destroy)
            Destroy(transform.gameObject);
    }

    public void LoadtheGame()
    {
        SceneManager.LoadScene("Scenes/Game");
        loaded = true;
    }


}
