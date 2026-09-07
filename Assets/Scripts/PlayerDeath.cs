using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform spawnPoint;            // Drag your SpawnPoint here
    public Lava lava;                       // Drag the Lava object here

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Make sure the player has the tag "Player"
        if (!CompareTag("Player"))
            Debug.LogWarning("Player is missing the 'Player' tag!");
    }

    public void Die()
    {
        // Move player back to spawn
        if (controller != null)
            controller.enabled = false;     // Temporarily disable so we can teleport

        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        if (controller != null)
            controller.enabled = true;

        // Reset the lava
        if (lava != null)
            lava.ResetLava();

        Debug.Log("Player died and respawned!");
    }
}