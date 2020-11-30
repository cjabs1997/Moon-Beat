using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    [Tooltip("The index of the scene in the build to load.")]
    public int sceneToLoad;

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
