using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    Element element;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites = new Sprite[5];
    public int val = 10;

    public void AssignType(Element _type)
    {
        element = _type;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[(int)_type];
        SetSpriteColor();
    }

    private void SetSpriteColor()
    {
        // Set the sprite color based on the enemy type
        switch (element)
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
        if (_gameObject.name == "Player")
        {
            Manager.Instance.SetElementCount(element, val);
            Destroy(gameObject);
        }
    }
}
