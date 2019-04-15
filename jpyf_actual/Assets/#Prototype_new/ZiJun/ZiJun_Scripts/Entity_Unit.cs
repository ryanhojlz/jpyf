using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Unit : MonoBehaviour
{
    enum Unit_Type
    {
        NONE,
        FLY,
        MELEE,
        RANGE
    }

    [SerializeField]
    Unit_Type Unit = Unit_Type.NONE;

    [SerializeField]
    float Health_Stat = 1f;//Attack stats of unit

    [SerializeField]
    float Attack_Stat = 1f;//Attack stats of unit

    [SerializeField]
    float Defence_Stat = 0f;//Defence stats of unit

    [SerializeField]
    float Attack_Speed_Stat = 0f;//Attack speed of unit

    [SerializeField]
    float Range_Stat = 1f;//Attack speed of unit

    [SerializeField]
    bool isMelee = true;//Decides whether projectile follows the player

    [SerializeField]
    GameObject Projectile_Prefeb = null;

    [SerializeField]
    List<GameObject> UnitsInRange = new List<GameObject>();

    Entity_Stats Unit_Stats = new Entity_Stats();

    // Use this for initialization
    void Start ()
    {
        SetHealthStat(Health_Stat);
        SetAttackStat(Attack_Stat);
        SetDefenceStat(Defence_Stat);
        SetAttackSpeedStat(Attack_Speed_Stat);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Attack()
    {
        //DO shooting projectile here
        if (Projectile_Prefeb)
        {

        }
    }

    // Getter 
    public float GetAttackStat() { return Unit_Stats.GetAtk(); }
    public float GetDefenceStat() { return Unit_Stats.GetDef(); }
    public float GetAttackSpeedStat() { return Unit_Stats.GetAtkS(); }
    public float GetHealthStat() { return Unit_Stats.GetHealth(); }
    public float GetRangeStat() { return Unit_Stats.GetRange(); }

    // Setter
    public void SetAttackStat(float _atk) { Unit_Stats.SetAtk(_atk); }
    public void SetDefenceStat(float _def) { Unit_Stats.SetDef(_def); }
    public void SetAttackSpeedStat(float _atkS) { Unit_Stats.SetAtkS(_atkS); }
    public void SetHealthStat(float _health) { Unit_Stats.SetHealth(_health); }
    public void SetRangeStat(float _range) { Unit_Stats.SetRange(_range); }

    //Other functions
    public void TakeDamage(float _damage)
    {
        Unit_Stats.TakeDamage(((_damage - Unit_Stats.GetDef() < 1) ? 1f : _damage - Unit_Stats.GetDef()));//If damage is lower then 1 after minusing defence, Damage dealt is 1
    }
}
