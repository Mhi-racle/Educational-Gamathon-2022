using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string sceneToGo;
    public void mainMenu()
    {
        // StaticVariables.sceneIndex = indexToGoTo;
        // // SceneManager.LoadScene("LoadingScene");
        SceneManager.LoadScene(sceneToGo);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}
