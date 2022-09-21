using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20;
    private SpawnManager spawnManager;
    public int enemyCount;
    private GameObject ballsParent;
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        ballsParent = GameObject.FindWithTag("ballParent");
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Answer"))
        {
            // spawnManager.CreateMoreEnemies();
            // other.gameObject.GetComponent<Enemy>().Die();
            other.transform.parent.GetComponent<AudioSource>().time = 0.4f;
            other.transform.parent.GetComponent<AudioSource>().Play();
            Timer.timeLeft += 10;
            Destroy(other.gameObject);


            for (int i = 0; i < ballsParent.transform.childCount; i++)
            {
                Destroy(ballsParent.transform.GetChild(i).gameObject);
            }
            Destroy(gameObject);

        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // other.gameObject.GetComponent<Enemy>().Die();
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 5, ForceMode.Impulse);


            for (int i = 0; i < ballsParent.transform.childCount; i++)
            {
                ballsParent.transform.GetChild(i).GetComponent<Enemy>().speed += spawnManager.enemySpeedIncrement;
            }
            Destroy(gameObject);
        }


    }
}
