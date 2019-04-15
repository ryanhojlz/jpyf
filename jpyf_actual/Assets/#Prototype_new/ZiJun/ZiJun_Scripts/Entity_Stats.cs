using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Stats
{
    float health;

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;
    }
}
