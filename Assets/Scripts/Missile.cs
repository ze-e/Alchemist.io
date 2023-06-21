using UnityEngine;

public class Missile : MonoBehaviour
{
    public int strength;

    public void SetStrength(int _str)
    {
        strength = _str;
    }

    public void Destroy(float _range)
    {
        Destroy(gameObject, _range);
    }
}
