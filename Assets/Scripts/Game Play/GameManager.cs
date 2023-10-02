using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {set; get;}
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject winMenu;
   
    public string sceneToGoTo;
    public LevelScript levelScript;
    private int escCount;

    public static bool isPaused = false;
    public bool isDone = false;
    public bool isDead = false;
    public GameObject player;
    public TextMeshProUGUI text;
    private Timer timer;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(true);
        
         winMenu.SetActive(true);

        timer = FindObjectOfType<Timer>();

        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !isDead && !isDone)
        {
            Pause();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !isDead && !isDone)
        {
            Resume();

        }

        if (isDead || isDone)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<PlayerHealth>().enabled = false;
            player.GetComponent<Shooting>().enabled = false;
        }
    }
    public void Pause()
    {
        Cursor.visible = true;
        
        pauseMenu.GetComponent<Animator>().SetTrigger("Open");
        isPaused = true;
        
        Time.timeScale = 0f;
    }


    public void Death()
    {
        isDead = true;
        Cursor.visible = true;
        text.text = "You have been defeated";

        timer.enabled = false;

        gameOverMenu.GetComponent<Animator>().SetTrigger("GameOver");
        //Time.timeScale = 0f;

    }
    public void TimeUp()
    {
        isDead = true;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = false;
        
        gameOverMenu.SetActive(true);
        //Time.timeScale = 0f;

    }
    // gameOverMenu.SetActive(true);

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        pauseMenu.GetComponent<Animator>().SetTrigger("Close");
        isPaused = false;
        // pauseMenu.LeanScale(Vector3.zero, 0f);

        // StartCoroutine(freezeTime(1));
    }


    public void Win()
    {
        isDone = true;
        Cursor.visible = true;
        levelScript.Pass();
        timer.enabled = false;
        winMenu.GetComponent<Animator>().SetTrigger("Open");
        
    }


    public void GoToScene(string sceneToGoTo)
    {
        // StaticVariables.sceneIndex = sceneToGoTo;
        SceneManager.LoadScene(sceneToGoTo);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
