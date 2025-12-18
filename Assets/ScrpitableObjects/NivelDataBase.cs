using UnityEngine;

[CreateAssetMenu(fileName = "NivelDataBase", menuName = "Scriptable Objects/NivelDataBase")]
public class NivelDataBase : ScriptableObject
{
    [Header("Información General")]
    public string nivelName;

    [Header("Personaje")]
    public Vector3 escalaPersonaje;
    
    [Header("Respawn Points")]
    public Vector3 respawnPoint;
    public Vector3[] respawnPoints;

    [Header("Cámara")]
    public float camaraZoom;
    public Vector2 screenPositionComposer;
    public bool esDeadZone;
    public Vector2 deadZoneWidthHeight;

    [Header("Confiner")]
    public GameObject confiner;
    public GameObject triggerDialogoInicial;
}
