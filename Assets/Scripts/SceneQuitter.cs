using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneQuitter : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(QuitApplicationAfterDelay(3f));
    }

    IEnumerator QuitApplicationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
