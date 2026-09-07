using UnityEngine;

public class Lava : MonoBehaviour
{
    [Header("Lava Settings")]
    public float riseSpeed = 1.5f;          // How fast the lava rises
    public bool startRising = true;         // Start rising immediately?

    private Vector3 startPosition;
    private bool isRising = false;

    void Start()
    {
        startPosition = transform.position;
        isRising = startRising;

        // Make sure the collider is a Trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    void Update()
    {
        if (isRising)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player touched the lava
        if (other.CompareTag("Player"))
        {
            PlayerDeath death = other.GetComponent<PlayerDeath>();
            if (death != null)
            {
                death.Die();
            }
        }
    }

    // Called by the death script to reset the lava
    public void ResetLava()
    {
        transform.position = startPosition;
        isRising = true;
    }

    public void StopLava()
    {
        isRising = false;
    }

    public void StartLava()
    {
        isRising = true;
    }
}