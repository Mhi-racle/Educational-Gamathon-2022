using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public static int timeLeft = 50;
    public bool isCounting = false;
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {

        timerText.gameObject.SetActive(false);
        isCounting = false;
        timeLeft = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogManager.isActive)
        {
            timerText.gameObject.SetActive(true);
            if (!isCounting && timeLeft > 0)
            {
                StartCoroutine(count());
            }
            else if (timeLeft == 0)
            {
                timerText.text = "Time is up";
                GameManager.TimeUp();
            }
        }

    }
    IEnumerator count()
    {
        isCounting = true;
        yield return new WaitForSeconds(1f);
        timeLeft--;
        timerText.text = timeLeft + "s";
        isCounting = false;
    }
}
