using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTarget : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float thresHold;



    void Update()
    {
        //gets the mouse position in the world position
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(mousePos);
        //adds the player's pos to the mousePos
        Vector3 targetPos = (player.position + mousePos) / 2f;

        //it then clamps the x and y of the target pose within the threshold
        targetPos.x = Mathf.Clamp(targetPos.x, -thresHold + player.position.x, thresHold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -thresHold + player.position.y, thresHold + player.position.y);

        //sets the CamPos of the camera to the targetPos
        this.transform.position = targetPos;
    }
}
