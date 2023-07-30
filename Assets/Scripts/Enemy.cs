using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element  { Air, Water, Fire, Earth, Aether };

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public int maxHealth = 15;
    public int minHealth = 5;
    int fullHealth;
    int health;
    Element enemyType;

    public GameObject dropPrefab;

    GameObject player;
    public float detectionRadius = 5f;
    public GameObject missilePrefab;

    float lastFireTime;
    float maxFireTime = 3;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        AssignHealth();
        AssignType();
    }

    private void Update()
    {
            float timeSinceLast = Time.time - lastFireTime;

            // Check if the player is within the detection radius
            if (timeSinceLast >= maxFireTime && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
            {
                // Fire a missile towards the player
                //FireMissile();
                lastFireTime = Time.time;
            }
    }

    private void FireMissile()
    {
        // Calculate the direction towards the player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Instantiate a missile prefab at the current position of the enemy
        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);

        // Retrieve the Missile script component from the instantiated missile
        EnemyMissile missileScript = missile.GetComponent<EnemyMissile>();

        // Set the direction for the missile to travel
        missileScript.SetDirection(direction);
    }

    void AssignHealth()
    {
        fullHealth = Random.Range(minHealth, maxHealth);
        health = fullHealth;
    }

    private void AssignType()
    {
        // Get a random enemy type
        enemyType = (Element)Random.Range(0, 5);
        SetSpriteColor();
    }

    private void SetSpriteColor()
    {
        // Set the sprite color based on the enemy type
        switch (enemyType)
        {
            case Element.Fire:
                spriteRenderer.color = Color.red;
                break;
            case Element.Earth:
                spriteRenderer.color = new Color(1f, 0.65f, 0f);
                break;
            case Element.Air:
                spriteRenderer.color = Color.white;
                break;
            case Element.Water:
                spriteRenderer.color = Color.blue;
                break;
            case Element.Aether:
                spriteRenderer.color = Color.green;
                break;
            default:
                spriteRenderer.color = Color.white;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _gameObject = collision.gameObject;
        if (_gameObject.CompareTag("Missile"))
        {
            Damage(_gameObject.GetComponent<Missile>().strength);
            Destroy(_gameObject);
        }
    }

    void Damage(int by)
    {
        health = health - by;
        Manager.Instance.ReddenSprite(gameObject, health, maxHealth);
        if (health < 0) Die();
    }

    void Die()
    {
        GameObject drop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        drop.GetComponent<Drop>().AssignType(enemyType);
        if (drop != null) Destroy(gameObject);
    }

}

