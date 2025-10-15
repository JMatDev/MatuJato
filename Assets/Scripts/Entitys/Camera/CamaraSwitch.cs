using UnityEngine;

public class CamaraSwitch : MonoBehaviour
{
    public CamaraLightController[] camarasToToggle;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // cuando el jugador entra al switch
        {
            toggleCamara();
        }
    }
    public void toggleCamara()
    {
        foreach (var cam in camarasToToggle)
        {
            if (cam != null)
            {
                bool state = !cam.GetIsActive();
                cam.Toggle(state);
            }
        }
    }
}
