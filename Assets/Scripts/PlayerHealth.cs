using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int health;
    public MenusManger menusManger;
    public Transform healthGroup;
    // Start is called before the first frame update
    void Start()
    {
        health = healthGroup.childCount;
        healthGroup.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogManager.isActive)
        {
            healthGroup.gameObject.SetActive(true);
        }

        if (health == 0)
        {
            Debug.Log("Game Over");
            menusManger.Death();
        }
    }
    public void Damage()
    {
        if (health > 0)
        {
            healthGroup.GetChild(health - 1).gameObject.GetComponent<Image>().color = Color.black;
            StartCoroutine(damageColor());

            // Destroy(healthGroup.GetChild(health).gameObject);
            health--;
        }

    }

    IEnumerator damageColor()
    {
        for (int i = 0; i < 3; i++)
        {

            transform.GetChild(i).GetComponent<Renderer>().material.color = Color.red;
        }
        transform.gameObject.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(.3f);
        for (int i = 0; i < 3; i++)
        {

            transform.GetChild(i).GetComponent<Renderer>().material.color = Color.white;
        }
        transform.gameObject.GetComponent<PlayerController>().enabled = true;
    }

    // public void Heal()
    // {


    //     if (health < 5)
    //     {

    //         healthGroup.GetChild(health + 1).gameObject.GetComponent<Image>().color = new Color(202, 60, 112, 255);
    //     }

    // }
}


