using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed = 10f;
    private Rigidbody rb;
    private float bounce = 300f;
    public ParticleSystem smoke;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        smoke = FindObjectOfType<ParticleSystem>();

        // transform.gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posToMoveTo = (player.transform.position - transform.position);
        rb.AddForce(posToMoveTo * speed * Time.deltaTime);

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //     other.gameObject.GetComponent<PlayerHealth>().health--; //reduces the health of the player by one when it comes in contact with the player
            // }
            other.gameObject.GetComponent<PlayerHealth>().Damage();
            other.transform.Translate(Vector3.back * bounce * Time.deltaTime);
        }
    }

    // public void Die()
    // {
    //     // ParticleSystem deathEffect = Instantiate(smoke, transform.position, Quaternion.identity);
    //     // deathEffect.Play();
    // }
}
