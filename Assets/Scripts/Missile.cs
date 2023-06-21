using UnityEngine;

public class Missile : MonoBehaviour
{
    public int strength = 1;
    public float range = .1f;
    public float speed = 2f;

    private void Start()
    {
        Destroy(gameObject, range);
    }
}
