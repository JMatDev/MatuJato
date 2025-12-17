using UnityEngine;

[CreateAssetMenu(fileName = "NivelDataBase", menuName = "Scriptable Objects/NivelDataBase")]
public class NivelDataBase : ScriptableObject
{
    public string nivelName;
    public Vector3 escalaPersonaje;
    public Vector3 respawnPoint;
    public bool esCamaraSeguimiento;
    public float camaraZoom;
    public Vector2 screenPositionComposer;
    public GameObject confiner;
}
