using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int sceneToGoTo;
    // Start is called before the first frame update
    void Start()
    {
        StaticVariables.sceneIndex = sceneToGoTo;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
