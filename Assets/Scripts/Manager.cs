using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    
    private Dictionary<Element, int> elementCounts;

    public GameObject bgObject;
    public Bounds bgBounds;

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
        ElementManager();
    }


    public void ElementManager()
    {
        elementCounts = new Dictionary<Element, int>();

        // Initialize the dictionary with default values
        foreach (Element element in Enum.GetValues(typeof(Element)))
        {
            elementCounts[element] = 0;
        }
    }

    public int GetElementCount(Element element)
    {
        if (elementCounts.ContainsKey(element))
        {
            return elementCounts[element];
        }
        
        return 0;
    }

    public void AddElementCount(Element element, int val)
    {
        if (elementCounts.ContainsKey(element))
        {
            elementCounts[element] += val;
            UpdateUI(element.ToString(), elementCounts[element].ToString());
        }
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

    public void UpdateUI(string key, string newVal, GameObject UI)
    {
        Transform[] children = UI.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.gameObject.name == "Error") child.gameObject.GetComponentInChildren<TMP_Text>().text = "";

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
