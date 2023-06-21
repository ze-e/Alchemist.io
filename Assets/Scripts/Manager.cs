using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public int Score { get; private set; }

    public GameObject bgObject;
    public GameObject enemyPrefab;
    private Bounds bgBounds;
    public int initEnemies = 100;
    public float spawnInterval = 30f;

    /* UI */
    public GameObject UI;

    private void Awake()
    {
        // Ensure only one instance of Manager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bgBounds = bgObject.GetComponent<Collider2D>().bounds;
    }

    private void Start()
    {
        CreateEnemies(initEnemies);
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    public void CreateEnemies(int totalEnemies)
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            Vector3 randomPosition = GetRandomPositionWithinBounds();
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomPosition = GetRandomPositionWithinBounds();
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

    private Vector3 GetRandomPositionWithinBounds()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(bgBounds.min.x, bgBounds.max.x),
            Random.Range(bgBounds.min.y, bgBounds.max.y),
            0f
        );

        return randomPosition;
    }

    public void AddScore(int value)
    {
        Score += value;
        UpdateUI("Score", Score.ToString());
    }

    public void ResetScore()
    {
        Score = 0;
        UpdateUI("Score", Score.ToString());
    }

    /* UI */
    public void UpdateUI(string key, string newVal)
    {
        Transform[] children = UI.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.gameObject.name == key)
            {
                child.gameObject.GetComponentInChildren<TMP_Text>().text = newVal;
            }
        }
    }

    /* FX */
    public void ReddenSprite(GameObject gameObject, float val, float maxVal)
    {
        // Darken SpriteRenderer based on remaining health
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float n = val / maxVal;
        spriteRenderer.color = new Color(spriteRenderer.color.r * n, spriteRenderer.color.g * n, spriteRenderer.color.b * n);
    }
}
