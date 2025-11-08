using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 respawnPoint;
    public Transform player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("DeathZone")) RespawnCharacter();
    }
    
    public void RespawnCharacter()
    {
        player.position = respawnPoint;
    }
}
