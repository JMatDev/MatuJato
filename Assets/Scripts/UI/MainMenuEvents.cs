
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        Time.timeScale = 1;
        playButton.onClick.AddListener(OnPlayButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnPlayButtonClick()
    {
        SceneManager.LoadScene("ZonaCamaras");
    }

    private void OnExitButtonClick()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
