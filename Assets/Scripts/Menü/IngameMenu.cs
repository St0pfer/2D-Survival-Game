using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour {

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Scenes/Start");
    }
}
