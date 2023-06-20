using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector3 targetPosition;
    public float moveSpeed = 5f;

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the Missile has reached the target position
        if (transform.position == targetPosition)
        {
            // Destroy the Missile object
            Destroy(gameObject);
        }
    }
}
