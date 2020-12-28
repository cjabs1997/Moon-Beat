using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public KeyCode pauseKey;
    public AudioSource audioSource;
    public Composer composer;
    public Conductor conductor;


    private GameObject pauseMenu;
    private float defaultTimeScale;

    private float songPosInBeats = 0f;
    private float songPosition = 0f;

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
        conductor.songPosition = songPosition;
        conductor.songPositionInBeats = songPosInBeats;
        composer.SetSonPosInBeats(songPosInBeats);

        Time.timeScale = defaultTimeScale;
        audioSource.UnPause();
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        songPosInBeats = conductor.songPositionInBeats;
        songPosition = conductor.songPosition;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        audioSource.Pause();
    }
}
