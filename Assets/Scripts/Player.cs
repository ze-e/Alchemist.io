using UnityEngine;
public class Player : MonoBehaviour
{
    public GameObject missilePrefab;

    public GameObject circlePrefab;
    public float moveSpeed = 2f;

    private GameObject circleInstance;
    private Vector3 targetPosition;
    private bool isMoving;

    Vector3 bgMin;
    Vector3 bgMax;

    Rigidbody2D rb;
    Vector2 mousePosition;

    private LineRenderer lineRenderer;

    private Quaternion initialRotation;
    public Transform spriteTransform;

    // Stats
    public int strength = 1; // earth
    public float range = .1f; // water
    public float speed = 2f; // air
    public float rapidFire = 1; // fire increment by .1

    float lastFireTime;
    float maxFireTime = 10;

    /* health */
    public Transform healthBar;
    public int maxHealth = 100;
    public int health;

    private void Start()
    {
        GetSceneBoundaries();
        rb = gameObject.GetComponent<Rigidbody2D>();
        initialRotation = transform.rotation;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        //health
        health = maxHealth;
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {

        if ( Input.GetMouseButton(0))
        {
            float timeSinceLast = Time.time - lastFireTime;
            if(timeSinceLast >= ((maxFireTime - ( 8 + rapidFire))))
            {
                Shoot();
                lastFireTime = Time.time;
            }
        }
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
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void LateUpdate()
    {
        FixRotation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _gameObject = collision.gameObject;
        if (_gameObject.CompareTag("EnemyMissile"))
        {
            Damage(_gameObject.GetComponent<EnemyMissile>().strength);
            Destroy(_gameObject);
        }
    }

    void FixRotation()
    {
        // Reset the rotation of the sprite to the initial rotation
        spriteTransform.rotation = initialRotation;
        healthBar.rotation = initialRotation;
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
        if( lineRenderer.enabled == false) lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetPosition);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        // Check if the player has reached the target position
        if (transform.position == targetPosition)
        {
            isMoving = false;
            Destroy(circleInstance);
            lineRenderer.enabled = false;
        }
    }

    void Shoot()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        Rigidbody2D missileRB = missile.GetComponent<Rigidbody2D>();
        Missile missileScr = missile.GetComponent<Missile>();
        missileRB.velocity = speed * transform.up;
        missileScr.SetStrength(strength);
        missileScr.Destroy(range);
    }

    private void DrawCircle()
    {
        if(isMoving) Destroy(circleInstance); 
        circleInstance = Instantiate(circlePrefab, targetPosition, Quaternion.identity);
    }

    /* health */

    void Damage(int by)
    {
        health -= by;
        DrawHealth();
        if (health <= 0) Die();
        else if (health < maxHealth) healthBar.gameObject.SetActive(true);
        else healthBar.gameObject.SetActive(false);
    }

    void Die()
    {
        Debug.LogError("GAME OVER");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    void DrawHealth()
    {
        float healthRatio = (float)health / (float)maxHealth;
        Transform bar = healthBar.Find("Bar");
        bar.localScale = new Vector2(healthRatio, bar.localScale.y);
    }

    // TODO: crafting system and inventory
}
