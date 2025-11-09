using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    public InputActionAsset InputActions;
    public GameObject ObjetoMenuPausa;

    private InputActionMap ActionMapUI;
    private InputActionMap ActionMapGameplay;
    
    void Start()
    {
        //Bindear Input Actions
        ActionMapUI = InputActions.FindActionMap("UI");
        ActionMapGameplay = InputActions.FindActionMap("Gameplay");
        //Asignar eventos
        InputAction pausaAction = ActionMapGameplay.FindAction("Pause");
        InputAction DespauseAction = ActionMapUI.FindAction("Despause");

        pausaAction.performed += OnPause;
        DespauseAction.performed += OnDespause;
    }

    void OnPause(InputAction.CallbackContext context)
    {
        ActionMapGameplay.Disable();
        ActionMapUI.Enable();
        Time.timeScale = 0;
        ObjetoMenuPausa.SetActive(true);
        
    }

    void OnDespause(InputAction.CallbackContext context)
    {
        ActionMapUI.Disable();
        ActionMapGameplay.Enable();
        Time.timeScale = 1;
        ObjetoMenuPausa.SetActive(false);
        
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
