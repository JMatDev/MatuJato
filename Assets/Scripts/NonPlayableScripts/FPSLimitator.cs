using UnityEngine;

public class FPSLimitator : MonoBehaviour
{
    private int FPSlimit = 60;
    void Start()
    {
        Application.targetFrameRate = FPSlimit;
    }

}

