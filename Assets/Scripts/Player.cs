using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject circlePrefab;
    public float moveSpeed = 2f;

    private GameObject circleInstance;
    private Vector3 targetPosition;
    private bool isMoving;

    Vector3 bgMin;
    Vector3 bgMax;

    Rigidbody2D rb;
    Vector2 mousePosition;

    private void Start()
    {
        GetSceneBoundaries();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
            DrawCircle();
        }

        if (isMoving)
        {
            MoveToTargetPosition();
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }

    private void SetTargetPosition()
    {
        // Convert the mouse position to world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Maintain the same Z position as the player

        // Clamp the mouse position within the boundaries of the "BG" GameObject
        float clampedX = Mathf.Clamp(mousePosition.x, bgMin.x, bgMax.x);
        float clampedY = Mathf.Clamp(mousePosition.y, bgMin.y, bgMax.y);
        targetPosition = new Vector3(clampedX, clampedY, transform.position.z);

        isMoving = true;
    }

    void GetSceneBoundaries()
    {
        // Calculate the boundaries of the "BG" GameObject
        GameObject bgObject = GameObject.Find("BG");
        Collider2D bgCollider = bgObject.GetComponent<Collider2D>();
        bgMin = bgCollider.bounds.min;
        bgMax = bgCollider.bounds.max;
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Check if the player has reached the target position
        if (transform.position == targetPosition)
        {
            isMoving = false;
            Destroy(circleInstance);
        }
    }

    private void DrawCircle()
    {
        if(isMoving) Destroy(circleInstance); 
        circleInstance = Instantiate(circlePrefab, targetPosition, Quaternion.identity);
    }
}
