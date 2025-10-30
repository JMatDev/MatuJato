using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    public UIDocument uiDocument;

    private Button playButton;
    private Button exitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        playButton = uiDocument.rootVisualElement.Q<Button>("PlayButton");
        playButton.RegisterCallback<ClickEvent>(ev => OnPlayButtonClick());
        exitButton = uiDocument.rootVisualElement.Q<Button>("ExitButton");
        exitButton.RegisterCallback<ClickEvent>(ev => OnExitButtonClick());
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
