using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenusManger : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject winMenu;
    public GameObject timeUpMenu;
    public int sceneToGoTo;
    public LevelScript levelScript;
    private int escCount;
    public static bool isPaused = false;
    public bool isDone = false;
    public bool isDead = false;
    public GameObject player;
    public TextMeshProUGUI text;
    private Timer timer;
    void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(false);
        timeUpMenu.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(false);


        isPaused = false;
        isDone = false;
        isDead = false;
        // pauseMenu.transform.localScale = Vector3.zero;
        // deathMenu.transform.localScale = Vector3.zero;
        // winMenu.transform.localScale = Vector3.zero;
        // timeUpMenu.transform.localScale = Vector3.zero;
        // deathMenu.transform.localScale = Vector3.zero;
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
        pauseMenu.SetActive(true);
        isPaused = true;
        // pauseMenu.LeanScale(Vector3.one, 0f).setEaseInOutExpo();
        // StartCoroutine(freezeTime(0));
        Time.timeScale = 0f;
    }


    public void Death()
    {
        isDead = true;
        Cursor.visible = true;
        text.text = "You have been defeated";

        timer.enabled = false;
        // freezeTime(0);
        // StartCoroutine(loadDeathAfterSomeSeconds());
        deathMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    public void TimeUp()
    {
        isDead = true;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = false;
        // timeUpMenu.LeanScale(Vector3.one, .1f).setEaseInOutExpo();
        // freezeTime(0);
        timeUpMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    // deathMenu.SetActive(true);

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
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
        // winMenu.SetActive(true);
        winMenu.SetActive(true);
        // Time.timeScale = 0f;
        // BGMusic.instance.GetComponent<AudioSource>().Play();
        // Time.timeScale = 0;

    }



    public void GoToScene(int sceneToGoTo)
    {
        // StaticVariables.sceneIndex = sceneToGoTo;
        SceneManager.LoadScene(sceneToGoTo);
    }

    IEnumerator freezeTime(int time)
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = time;
    }

    IEnumerator loadWinAfterSomeSeconds()
    {
        yield return new WaitForSeconds(2f);

        winMenu.LeanScale(Vector3.one, .1f).setEaseInOutExpo();
        freezeTime(0);
    }
    IEnumerator loadDeathAfterSomeSeconds()
    {
        yield return new WaitForSeconds(.5f);

        deathMenu.LeanScale(Vector3.one, .1f).setEaseInOutExpo();
        freezeTime(0);
    }
    IEnumerator timeUpAfterSomeSeconds()
    {
        yield return new WaitForSeconds(.5f);

        timeUpMenu.LeanScale(Vector3.one, .1f).setEaseInOutExpo();
        freezeTime(0);
    }
}
