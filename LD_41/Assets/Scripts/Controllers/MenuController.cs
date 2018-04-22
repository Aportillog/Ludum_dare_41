using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Button startGame_btn;
    public Button quitGame_btn;
    public Button muteGame_btn;

    private AudioController m_AudioController;

    private void Start()
    {
        m_AudioController = FindObjectOfType<AudioController>();
        Button btnStrt = startGame_btn.GetComponent<Button>();
        Button btnQut = quitGame_btn.GetComponent<Button>();
        Button btnMute = muteGame_btn.GetComponent<Button>();

        btnStrt.onClick.AddListener(startGame);
        btnQut.onClick.AddListener(quitGame);
        btnMute.onClick.AddListener(muteGame);

    }
    public void startGame()
    {
        //On hit Play button
        SceneManager.LoadScene("Main");
    }
    public void quitGame()
    {
        //On hit Quit Game button
        Application.Quit();
    }

    public void muteGame()
    {
        m_AudioController.muteGame();
    }
}
