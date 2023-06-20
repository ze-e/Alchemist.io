using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement
    public Vector3 offset; // Offset from the target position

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position based on the target's position and offset
            Vector3 desiredPosition = target.position + offset;

            // Use SmoothDamp to smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
