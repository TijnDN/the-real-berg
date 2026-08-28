using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Grapple Settings")]
    public float maxGrappleDistance = 50f;
    public float pullSpeed = 25f;
    public float stopDistance = 2f;          // How close before it stops pulling
    public LayerMask grappleLayer;           // Optional: set this if you want
    public KeyCode grappleKey = KeyCode.Mouse1; // Right mouse button

    [Header("References")]
    public Transform playerCamera;           // Drag your camera here
    public LineRenderer lineRenderer;        // Optional but recommended

    private CharacterController controller;
    private bool isGrappling = false;
    private Vector3 grapplePoint;

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
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }

        if (Input.GetKeyUp(grappleKey))
        {
            StopGrapple();
        }

        if (isGrappling)
        {
            PullPlayer();
            DrawRope();
        }
    }

    void StartGrapple()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxGrappleDistance))
        {
            if (hit.collider.CompareTag("Hook"))
            {
                isGrappling = true;
                grapplePoint = hit.point;

                if (lineRenderer != null)
                    lineRenderer.enabled = true;
            }
        }
    }

    void PullPlayer()
    {
        Vector3 direction = (grapplePoint - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, grapplePoint);

        if (distance > stopDistance)
        {
            controller.Move(direction * pullSpeed * Time.deltaTime);
        }
        else
        {
            StopGrapple();
        }
    }

    void StopGrapple()
    {
        isGrappling = false;

        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    void DrawRope()
    {
        if (lineRenderer == null) return;

        lineRenderer.SetPosition(0, transform.position + Vector3.up * 1.2f);
        lineRenderer.SetPosition(1, grapplePoint);
    }
}