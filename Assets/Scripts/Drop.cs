using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    EnemyType enemyType;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites = new Sprite[5];

    public void AssignType(EnemyType _type)
    {
        enemyType = _type;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[(int)_type];
        SetSpriteColor();
    }

    private void SetSpriteColor()
    {
        // Set the sprite color based on the enemy type
        switch (enemyType)
        {
            case EnemyType.Fire:
                spriteRenderer.color = Color.red;
                break;
            case EnemyType.Earth:
                spriteRenderer.color = Color.black;
                break;
            case EnemyType.Air:
                spriteRenderer.color = Color.white;
                break;
            case EnemyType.Water:
                spriteRenderer.color = Color.blue;
                break;
            case EnemyType.Aether:
                spriteRenderer.color = Color.green;
                break;
            default:
                spriteRenderer.color = Color.white;
                break;
        }
    }

}
