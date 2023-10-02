using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class levelSelector : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelText;


    void Start()
    {
        levelText.text = level.ToString();

    }
    public void OpenScene()
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
