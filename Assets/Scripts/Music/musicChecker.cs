using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicChecker : MonoBehaviour
{
    // Update is called once per frame
    void Awake()
    {
        if ((SceneManager.GetActiveScene().name != "MainMenu"))
        {
            BGMusic.instance.GetComponent<AudioSource>().Pause();
        }

        else if ((SceneManager.GetActiveScene().name == "MainMenu"))
        {
            BGMusic.instance.GetComponent<AudioSource>().Play();
            return;
        }
        if ((SceneManager.GetActiveScene().name == "LevelSelection"))
        {
            BGMusic.instance.GetComponent<AudioSource>().Play();
        }
    }
}
