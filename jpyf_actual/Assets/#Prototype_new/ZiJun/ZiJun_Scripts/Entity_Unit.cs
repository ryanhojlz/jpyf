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
    float atk = 1f;//Attack stats of unit

    [SerializeField]
    float def = 0f;//Defence stats of unit

    [SerializeField]
    float atkS = 0f;//Attack speed of unit

    [SerializeField]
    bool isMelee = true;//Decides whether projectile follows the player

    [SerializeField]
    GameObject Projectile_Prefeb = null;

    // Use this for initialization
    void Start ()
    {
		
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
    public float GetAttackStat() { return atk; }
    public float GetDefenceStat() { return def; }
    public float GetAttackSpeedStat() { return atkS; }

    // Setter
    public void SetAttackStat(float _atk) { atk = _atk; }
    public void SetDefenceStat(float _def) { def = _def; }
    public void SetAttackSpeedStat(float _atkS) { atkS = _atkS; }
}
