using UnityEngine;

public class CamaraSwitch : MonoBehaviour
{
    public CamaraLightController camara;

    [Header("Opciones del Switch")]
    public bool soloUnaVez = false;   // 👈 CONFIGURABLE DESDE UNITY

    private bool isOn;
    private bool usado = false;

    void Start()
    {
        if (camara != null)
            isOn = camara.GetIsActive();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (soloUnaVez && usado) return; // 🚫 no hace nada si ya fue usado

        isOn = !isOn;
        camara.Toggle(isOn);

        usado = true;
    }

    // 🔄 opcional para puzzles con reset
    public void ResetSwitch()
    {
        usado = false;
        isOn = camara.GetIsActive();
    }
}
