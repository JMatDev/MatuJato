using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CamaraLightController : MonoBehaviour
{
    public Light2D lightCamare;
    public Collider2D deathZone;
    public void Toggle(bool active)
    {
        if (lightCamare != null)
            lightCamare.enabled = active;

        if (deathZone != null)
            deathZone.enabled = active;
    }
    public bool GetIsActive()
    {
        return lightCamare.enabled;
    }
}
