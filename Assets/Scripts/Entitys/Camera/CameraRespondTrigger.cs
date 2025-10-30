using UnityEngine;

public class CameraRespondTrigger : MonoBehaviour
{
    public float rotationAmount;
    public float rotationDuration;

    private bool activated = false;
    private float startZ;
    private float targetZ;
    private float rotationTimer;

    public void Girar()
    {
        if (!activated)
        {
            activated = true;
            startZ = transform.eulerAngles.z;
            targetZ = startZ + rotationAmount;
            rotationTimer = 0f;
        }
    }
    
    public void Update()
    {
        if (rotationTimer < rotationDuration && activated)
        {
            rotationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(rotationTimer / rotationDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            float newZ = Mathf.LerpAngle(startZ, targetZ, smoothT);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newZ);
        }
        if(rotationTimer >= rotationDuration)
        {
            activated = false;
        }
    }
 }
