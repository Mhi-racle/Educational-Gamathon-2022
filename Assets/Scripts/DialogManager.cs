using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class DialogManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    int activeMessage = 0;
    public static bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // sets the background box to be zero on start
        backgroundBox.transform.localScale = Vector3.zero;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            isActive = false;
            OpenDialog(currentMessages);
        }
        else
        {
            isActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            NextMessage();
        }
    }
    public void OpenDialog(Message[] messages)
    {
        currentMessages = messages;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started conversation ! Loaded messages : " + messages.Length);
        DisplayMessage();

        //animating the dialog box using lean tween
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo(); //setEaseInoutExpo smoothens the animation
    }

    public void DisplayMessage()
    {
        Message messageDisplay = currentMessages[activeMessage];
        messageText.text = messageDisplay.message;
        actorImage.sprite = messageDisplay.sprite;

        //animates the text per every change
        AnimateTextColor();
    }

    //uses Lean Tween to animate the text's opacity to give a fade in and fade out effect
    void AnimateTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Coversaion ended");
            isActive = false;
            //animaition to close the dialog box
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
        }
    }
}
