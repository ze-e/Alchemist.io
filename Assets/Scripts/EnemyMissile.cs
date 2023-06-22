using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 direction;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void Update()
    {
        // Move the missile in the specified direction
        transform.position += direction * speed * Time.deltaTime;
    }
}
