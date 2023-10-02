using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public GameObject bullerSpawnPoint;
    public float waitTime;
    public GameObject bulletPrefab;
    public float bulletForce;

    public float reloadTime;
    //for reloading and shooting
    public int currentAmmo;
    public int maxAmmo;
    public static bool isReloading;
    public TextMeshProUGUI ammoText;
    
    public PlayerController playerController;
    public Animator playerAnimator;
    public GameObject gun;
    public AudioSource gunShot;

    //gets an object and uses it as the cursor
    public ParticleSystem muzzleShot;
    // Start is called before the first frame update
    void Start()
    {

        isReloading = false; //disable the question panel
        currentAmmo = maxAmmo;

    }

    // Update is called once per frame
    void Update()
    {
        //gets the position of the mouse

        if (DialogManager.isActive)
        {
            return;
        }

        
        if (Input.GetButtonDown("Fire1") && !playerController.isRolling)
        {
            Shoot();
        }

        //check is Ammo is less than 0, if it is, it calls the reloading script
        if (currentAmmo == 0 && !isReloading)
        {
            Debug.Log("Reloading");
            isReloading = false;
            playerAnimator.SetBool("isReloading", true);
            // gun.SetActive(false);

            // questionPanel.SetActive(true);
            StartCoroutine(Reload());
        }

        if (!isReloading)
        {
            ammoText.text = currentAmmo.ToString();
        }

    }

    public void Shoot()
    {
        //shoots only if the currentAmmo is more than zero and the player is not reloading
        if (currentAmmo > 0 && !isReloading && !playerController.isRolling && !MenusManger.isPaused)
        {
            // gunShot.time = .8f;
            gunShot.PlayOneShot(gunShot.clip, 0.3f);
            GameObject bullet = Instantiate(bulletPrefab, bullerSpawnPoint.transform.position, transform.rotation);
            currentAmmo--;
            muzzleShot.Play();
        }

       

    }

    IEnumerator Reload()
    {
        isReloading = true;
        gun.SetActive(false);
        ammoText.text = "-";
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        // StartCoroutine(showGun());
        gun.SetActive(true);
        playerAnimator.SetBool("isReloading", false);
        gun.SetActive(true);
        isReloading = false;
    }

    // IEnumerator showGun()
    // {
    //     yield return new WaitForSeconds(1f);
    //     gun.SetActive(true);
    // }
}
