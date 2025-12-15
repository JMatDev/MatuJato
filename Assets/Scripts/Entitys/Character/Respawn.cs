using UnityEngine;

public class Respawn : MonoBehaviour
{
    [HideInInspector] public Vector3 respawnPoint;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("DeathZone")) RespawnCharacter();
    }
    
    public void RespawnCharacter()
    {
        transform.position = respawnPoint;
    }
}
