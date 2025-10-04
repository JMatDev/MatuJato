using UnityEngine;

public class CameraRotationMode : MonoBehaviour
{
    [Header("Rotación")]
    public float rotationAngle;
    public float rotationDuration;
    public float pauseTime;

    private float startZ;
    private float targetZ;
    private bool rotatingLeft = true;
    private float rotationTimer = 0f;
    private float pauseTimer = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startZ = transform.eulerAngles.z;
        targetZ = startZ - rotationAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        rotationTimer += Time.deltaTime;
        float t = Mathf.Clamp01(rotationTimer / rotationDuration);

        // Interpolación suave (ease in-out)
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        float fromZ = rotatingLeft ? startZ : targetZ;
        float toZ = rotatingLeft ? targetZ : startZ;

        float newZ = Mathf.LerpAngle(fromZ, toZ, smoothT);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newZ);

        if (t >= 1f)
        {
            rotatingLeft = !rotatingLeft;
            rotationTimer = 0f;
            pauseTimer = pauseTime;
        }
    }
}
