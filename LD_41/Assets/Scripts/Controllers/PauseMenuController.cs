using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    private bool isPaused;

    private Animator muteBtnAnimtr;
    public Button muteGame_btn;

    public GameObject pauseMenuCanvas;

    private void Awake()
    {
        isPaused = false;
        //muteBtnAnimtr = muteGame_btn.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            GameController.instance.isPaused = isPaused;
        }
    }

    public void Resume()
    {
        isPaused = false;
        GameController.instance.isPaused = false;
    }

    public void QuitGame()
    {
        //On hit Quit Game button
        Application.Quit();
    }

    public void muteGame()
    {
        //bool soundStatus = muteBtnAnimtr.GetBool("isMuted");
        //muteBtnAnimtr.SetBool("isMuted", !soundStatus);
        AudioController.instance.muteGame();
    }
}
