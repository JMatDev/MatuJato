using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoaderScript : MonoBehaviour {
    private static string sceneToLoad;

    public static void LoadScene(string targetScene) {
        sceneToLoad = targetScene;
        SceneManager.LoadScene("LoadingScene");
    }

    void Start() {
        StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync() {
        if (string.IsNullOrEmpty(sceneToLoad)) {
            Debug.LogError("Babomso");
            yield break;
        }

        Debug.Log("Iniciando carga de: " + sceneToLoad);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone) {
            float progress = Mathf.Clamp01(asyncLoad.progress/0.9f);
            Debug.Log("Progreso: " + (progress * 100) + " %");

            if (asyncLoad.progress>=0.9f) {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
