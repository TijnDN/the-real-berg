using UnityEngine;

public class MountainSlide : MonoBehaviour
{
    [Header("Slide Settings")]
    public float slideSpeed = 12f;          // How fast you slide
    public float slideAcceleration = 20f;   // How quickly you start sliding
    public float maxSlideSpeed = 25f;       // Maximum slide speed

    private CharacterController controller;
    private Vector3 slideVelocity;
    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isSliding)
        {
            // Keep applying the slide force
            controller.Move(slideVelocity * Time.deltaTime);

            // Slowly increase speed while sliding
            if (slideVelocity.magnitude < maxSlideSpeed)
            {
                slideVelocity += slideVelocity.normalized * slideAcceleration * Time.deltaTime;
            }
        }
        else
        {
            // Reset slide velocity when not on mountain
            slideVelocity = Vector3.zero;
        }

        isSliding = false; // Reset every frame (will be set again in OnControllerColliderHit)
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Only slide on objects tagged "Mountain"
        if (hit.collider.CompareTag("Mountain"))
        {
            isSliding = true;

            // Calculate the direction down the slope
            Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal).normalized;

            // Add some force in that direction
            slideVelocity = slopeDirection * slideSpeed;
        }
    }
}