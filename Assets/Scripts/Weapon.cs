using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat { Strength, Range, Speed, Rate, Cost };

// technically we could go without this class and just use the player stats, but it might be more helpful in the long run to do it this way if we add
// new features later
public class Weapon : MonoBehaviour
{
    // Stats
    public int strength = 1; // earth
    public float range = .1f; // water
    public float speed = 2f; // air
    public float rapidFire = 1; // fire increment by .1

    public Weapon(int _str, float _rng, float _spd, float _rte)
    {
        this.strength = _str;
        this.range = _rng;
        this.speed = _spd;
        this.rapidFire = _rte;
    }
}
