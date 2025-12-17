using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private bool playerInside = false;
    private PlayerController player;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            Guardar();
        }
    }

    void Guardar()
    {
        SaveManager.SavePlayerData(player);
        Debug.Log("Juego guardado");
        // aquí luego puedes poner sonido o texto
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            player = other.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            player = null;
        }
    }
}
