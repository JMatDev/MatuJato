using UnityEngine;

public class CameraRotationMode : MonoBehaviour
{
    [Header("Rotación")]
    public float rotationAngle = 45f;   // Ángulo máximo hacia cada lado
    public float rotationSpeed = 30f;   // Velocidad de rotación

    private float startZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startZ = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Oscila la rotación en Z usando seno
        float z = startZ + Mathf.Sin(Time.time * rotationSpeed * Mathf.Deg2Rad) * rotationAngle;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, z);
    }
}
