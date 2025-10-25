using System.Drawing;
using UnityEngine;

public class MapTransition : MonoBehaviour {
    public float xCameraPosition;
    public float yCameraPosition;  
    public float sizeCamera;
    public float xRespawn;
    public float yRespawn;
    public GameObject respawnPoint;
    public GameObject newMapTrigger;
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            Camera.main.transform.position = new Vector3(xCameraPosition, yCameraPosition, -10f);
            Camera.main.orthographicSize = sizeCamera;
            respawnPoint.transform.position = new Vector3(xRespawn, yRespawn, respawnPoint.transform.position.z);
            newMapTrigger.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
