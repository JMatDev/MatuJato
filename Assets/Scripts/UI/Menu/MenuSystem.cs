using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool pausa = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (pausa == false)
            {
                Pausa();
                pausa = true;
            }
            else
            {
                Reanudar();
                pausa = false;
            }
        }
    }
    public void Reanudar()
    {
        ObjetoMenuPausa.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pausa()
    {
        ObjetoMenuPausa.SetActive(true);
        Time.timeScale = 0;
    }
    public void moverPantalla(string nombrePantalla)
    {
        SceneManager.LoadScene(nombrePantalla);
    }
    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
