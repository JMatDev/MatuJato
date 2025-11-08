using UnityEngine;

[CreateAssetMenu(fileName = "NivelDataBase", menuName = "Scriptable Objects/NivelDataBase")]
public class NivelDataBase : ScriptableObject
{
    public float camaraPosicionX;
    public float camaraPosicionY;
    public float camaraZoom;
    public Vector3 respawnPoint;
}
