using UnityEngine;
using UnityEngine.UIElements;

public class MostrarFps : MonoBehaviour
{
    public UIDocument uiDocument;
    private Label fpsLabel;
    private float deltaTime = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable(){
        var root = GetComponent<UIDocument>().rootVisualElement;
        fpsLabel = root.Q<Label>("fpsLabel");
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = deltaTime > 0.0001f ? 1.0f / deltaTime : 0f;
        if (fpsLabel != null) fpsLabel.text = $"{Mathf.Ceil(fps)} FPS";
    }
}
