using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelScript : MonoBehaviour
{

    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked", 1))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
        }
        Debug.Log("LEVEL " + PlayerPrefs.GetInt("levelsUnlocked", currentLevel + 1) + " UNLOCKED");
    }
}
