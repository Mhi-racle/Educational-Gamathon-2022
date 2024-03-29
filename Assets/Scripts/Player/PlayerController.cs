using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public float speed = 6f;
    public float turnSmoothSpeed = 0.1f;
    float turnSmoothVelocity;
    private Rigidbody rb;

    public Animator playerAnimator;
    public bool isRolling;
    public GameObject gun;
    Vector3 lookPos;
    public Joystick Joystick;


    public GameObject Crosshair;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true; //hides the cursor

        rb = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    void FixedUpdate()
    {
        //if dialogue manager is active, the player controller script wont be active
        if (DialogManager.isActive)
        {
            gun.SetActive(false);

        }
        //gets the vertical and horizontal inputs of the player and stores it in floats

        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || 1==1)
        {
            float verticalInput = Joystick.Vertical;
            float horizontalInput = Joystick.Horizontal;
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;


            //if the direction is greater than zero
            if (direction.magnitude >= 0.1f)
            {
                //if player is moving, then play the rolling animation and also set isRolling to true
                //isRolling notifies the shooting script so that it doesnt shoot when the player is rolling
                playerAnimator.SetBool("isRolling", true);
                isRolling = true;

                //disables the gun
                gun.SetActive(false);
                //calculates the target angle to be rotated to 
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //moves the player in the direction of the camera
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
                // rb.AddForce(moveDirection * speed);

            }
            //if the player is not moving, the rolling stops and isRolling is set to false
            else
            {
                //enable the gun if the player is stagnant and is not reloading
                // StartCoroutine(showGun());
                if (!Shooting.isReloading)
                {
                    gun.SetActive(true);
                    playerAnimator.SetBool("isRolling", false);
                    isRolling = false;
                }

            }

        }
        else
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;


            //if the direction is greater than zero
            if (direction.magnitude >= 0.1f)
            {
                //if player is moving, then play the rolling animation and also set isRolling to true
                //isRolling notifies the shooting script so that it doesnt shoot when the player is rolling
                playerAnimator.SetBool("isRolling", true);
                isRolling = true;

                //disables the gun
                gun.SetActive(false);


                //calculates the target angle to be rotated to 
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //moves the player in the direction of the camera
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
                // rb.AddForce(moveDirection * speed);

            }
            //if the player is not moving, the rolling stops and isRolling is set to false
            else
            {
                //enable the gun if the player is stagnant and is not reloading
                // StartCoroutine(showGun());
                if (!Shooting.isReloading)
                {
                    gun.SetActive(true);
                    playerAnimator.SetBool("isRolling", false);
                    isRolling = false;
                }

            }
        }


        //makes the player look at the position of the mouse
        if (!isRolling)
        {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }


        Vector3 lookDir = lookPos - transform.position;

        lookPos.y = 2;


        lookDir.y = 0;
        transform.LookAt(transform.position + lookDir, Vector3.up);

        }
    }
}


   
