using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Grappler Settings")]
    public float maxGrappleDistance = 60f;
    public float launchForce = 32f;           // Main fling power
    public float upwardBoost = 8f;            // Extra height so the arc feels good
    public float airDrag = 0.4f;              // How fast horizontal speed slows down in air
    public KeyCode grappleKey = KeyCode.Mouse1;

    [Header("References")]
    public Transform playerCamera;
    public LineRenderer lineRenderer;

    private CharacterController controller;
    private Vector3 velocity;                 // Our custom velocity for the fling
    private bool isFlying = false;
    private Vector3 grapplePoint;
    private float ropeShowTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(grappleKey) && !isFlying)
        {
            TryGrapple();
        }

        HandleFlight();
        DrawRope();
    }

    void TryGrapple()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxGrappleDistance))
        {
            if (hit.collider.CompareTag("Hook"))
            {
                grapplePoint = hit.point;

                // Direction toward the hook
                Vector3 direction = (grapplePoint - transform.position).normalized;

                // Strong fling + upward boost for a nice arc
                velocity = direction * launchForce;
                velocity.y += upwardBoost;

                isFlying = true;
                ropeShowTimer = 0.6f;

                if (lineRenderer != null)
                    lineRenderer.enabled = true;
            }
        }
    }

    void HandleFlight()
    {
        if (!isFlying) return;

        // Apply gravity
        velocity.y += -20f * Time.deltaTime;   // same gravity as your movement script

        // Light air resistance so you don't fly forever
        velocity.x = Mathf.Lerp(velocity.x, 0f, airDrag * Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, 0f, airDrag * Time.deltaTime);

        // Move the player
        controller.Move(velocity * Time.deltaTime);

        // Stop the special flight when we land
        if (controller.isGrounded && velocity.y <= 0f)
        {
            isFlying = false;
            velocity = Vector3.zero;

            if (lineRenderer != null)
                lineRenderer.enabled = false;
        }
    }

    void DrawRope()
    {
        if (lineRenderer == null) return;

        if (ropeShowTimer > 0f)
        {
            ropeShowTimer -= Time.deltaTime;
            Vector3 startPos = transform.position + Vector3.up * 1.3f;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, grapplePoint);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}