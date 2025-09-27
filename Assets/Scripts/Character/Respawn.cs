using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("DeathZone")) RespawnCharacter();
    }
    
    public void RespawnCharacter()
    {
        player.position = respawnPoint.position;
    }
}
