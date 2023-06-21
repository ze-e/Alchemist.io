using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    int health;

    private void Start()
    {
        health = maxHealth;
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
        Manager.Instance.AddScore(10);
        Destroy(gameObject);
    }
}
