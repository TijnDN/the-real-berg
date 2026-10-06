using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public Transform cameraTransform;
    public float bobSpeed = 14f;
    public float bobAmount = 0.05f;

    private float defaultY;
    private float timer;

    void Start()
    {
        defaultY = cameraTransform.localPosition.y;
    }

    void Update()
    {
        // Only bob when moving
        float move = Input.GetAxis("Horizontal") + Input.GetAxis("Vertical");

        if (Mathf.Abs(move) > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;
            float newY = defaultY + Mathf.Sin(timer) * bobAmount;
            cameraTransform.localPosition = new Vector3(0f, newY, 0f);
        }
        else
        {
            // Return to normal
            timer = 0f;
            cameraTransform.localPosition = new Vector3(0f, defaultY, 0f);
        }
    }
}