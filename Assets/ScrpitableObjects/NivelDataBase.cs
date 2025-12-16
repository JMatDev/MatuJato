using UnityEngine;

[CreateAssetMenu(fileName = "NivelDataBase", menuName = "Scriptable Objects/NivelDataBase")]
public class NivelDataBase : ScriptableObject
{
    public string nivelName;
    public Vector3 respawnPoint;
    public bool esCamaraSeguimiento;
    public float camaraZoom;
    public float camaraPosicionX;
    public float camaraPosicionY;
    public Vector3 camaraPosicion;
    public GameObject confiner;
}
