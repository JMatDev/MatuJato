using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    public UIDocument uiDocument;

    private Button playButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        playButton = uiDocument.rootVisualElement.Q<Button>("PlayButton");
        playButton.RegisterCallback<ClickEvent>(ev => OnPlayButtonClick());
    }

    private void OnPlayButtonClick()
    {
        SceneLoaderScript.LoadScene("TestScene3");
    }
}
