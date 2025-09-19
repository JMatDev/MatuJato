using Unity.Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class MapTransition : MonoBehaviour
{
    public GameObject CineMachine;
    public BoxCollider2D newMapTrigger;
    public PolygonCollider2D newMapBoundary;

    private BoxCollider2D boxCol;
    private CinemachineConfiner2D confiner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        confiner = CineMachine.GetComponent<CinemachineConfiner2D>();
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            confiner.BoundingShape2D = newMapBoundary;
            boxCol.enabled = false;
            newMapTrigger.enabled = true;
        }
    }
}
