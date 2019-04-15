using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Stats
{
    float m_health;
    float m_maxhealth;
    float m_defence;
    float m_attack_speed;
    float m_attack;
    float m_range;

    // Getter
    public float GetHealth() { return m_health; }
    public float GetMaxHealth() { return m_maxhealth; }
    public float GetDef() { return m_defence; }
    public float GetAtkS() { return m_attack_speed; }
    public float GetAtk() { return m_attack; }
    public float GetRange() { return m_range; }

    // Setter
    public void SetHealth(float _health) { m_health = _health; }
    public void SetMaxHealth(float _maxhealth) { m_maxhealth = _maxhealth; }
    public void SetDef(float _defence) { m_defence = _defence; }
    public void SetAtkS(float _attack_speed) { m_attack_speed = _attack_speed; }
    public void SetAtk(float _attack) { m_attack = _attack; }
    public void SetRange(float _range) { m_range = _range; }

    //Other functions
    public void TakeDamage(float _damage) { m_health -= _damage; }
}
