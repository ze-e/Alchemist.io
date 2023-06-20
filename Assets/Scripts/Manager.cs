using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public int Score { get; private set; }

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
        spriteRenderer.color = new Color(spriteRenderer.color.r * n, spriteRenderer.color.g, spriteRenderer.color.b);
    }
}
