using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public KeyCode pauseKey;
    public AudioSource audioSource;


    private GameObject pauseMenu;
    private float defaultTimeScale;

    private void Awake()
    {
        pauseMenu = this.transform.GetChild(0).gameObject;
        defaultTimeScale = Time.timeScale;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if(pauseMenu.activeSelf == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = defaultTimeScale;
        audioSource.Play();
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        audioSource.Pause();
    }
}
