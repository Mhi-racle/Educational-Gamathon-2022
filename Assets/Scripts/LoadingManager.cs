using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour
{
    public Slider slider;
    public int index;

    void Awake()
    {
        index = StaticVariables.sceneIndex;
        LoadLevel(index);
    }

    void LoadLevel(int index)
    {

        StartCoroutine(LoadAsynLevel(index));
    }

    IEnumerator LoadAsynLevel(int index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            slider.value = progress;
            yield return null;
        }
    }
}
