using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    float Attack_Range_Stat = 5f;//Attack Range of unit

    [SerializeField]
    float Chase_Range_Stat = 5f;//Chase Range of unit

    [SerializeField]
    bool isMelee = true;//Decides whether projectile follows the player

    [SerializeField]
    GameObject Projectile_Prefeb = null;

    [SerializeField]
    List<GameObject> UnitsInRange = new List<GameObject>();

    [SerializeField]
    Image healthBar;//Put healthbar image inside here (The one that change not prefeb)

    Transform Target;

    

    Entity_Stats Unit_Stats = new Entity_Stats();

    Unit_StateMachine sm = new Unit_StateMachine();


    bool m_InAtkRange = false;
    Vector3 m_HitPoint = Vector3.zero;

    AudioSource UnitThatProduceSound;

    [SerializeField]
    AudioClip AttackSound;

    //For Attack
    float atkcooldown = 0f;
    float countdown = -1f;

    // Use this for initialization
    private void Awake()
    {
        SetHealthStat(Health_Stat);
        SetMaxHealthStat(Health_Stat);//Starting health = maxHealth
        SetAttackStat(Attack_Stat);
        SetDefenceStat(Defence_Stat);
        SetAttackSpeedStat(Attack_Speed_Stat);
        SetAttackRangeStat(Attack_Range_Stat);

        //Debug.Log("Range Value : " + Range_Stat);

        atkcooldown = 1f / GetAttackSpeedStat();

        SetChaseRangeStat(Chase_Range_Stat);
    }
    void Start ()
    {
        AddState();
        ChangeState("chase_cart");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Leave here for Debug purposes only v
        if (Debug.isDebugBuild)//Only in debug do we need to change Stats during runtime menually
        {
            SetHealthStat(Health_Stat);
            SetAttackStat(Attack_Stat);
            SetDefenceStat(Defence_Stat);
            SetAttackSpeedStat(Attack_Speed_Stat);
            UpdateHealth();//is in taking damage & Healing(If applicable)
        }
        //Debug Purposes only ^
        //FindNearestInList();

        if (countdown >= 0)
            countdown -= Time.deltaTime;

        if (GetHealthStat() <= 0 && sm.GetCurrentStateName() != "dead")
        {
            ChangeState("dead");
        }
        //Debug.Log("State machine is " + sm.GetCurrentStateName());
        sm.ExecuteStateUpdate();//Updating statemachine

    }

    public void Attack()
    {
        if (countdown < 0)
        {
            float lifeTime = 1f;//Temporary hard coding it here
                                //DO shooting projectile here
            if (Projectile_Prefeb)
            {
                GameObject bulletGO = (GameObject)Instantiate(Projectile_Prefeb, this.transform.position, this.transform.rotation);

                Entity_Projectile Projectile = bulletGO.GetComponent<Entity_Projectile>();

                if (AttackSound)
                {
                    UnitThatProduceSound.clip = AttackSound;
                    UnitThatProduceSound.Play();
                }

                if (!Projectile)
                {
                    Debug.Log(Projectile_Prefeb + " Does not have -Entity_Projectile_Class-");
                    return;
                }
                if (!Target)
                {
                    Projectile.SetDirection(this.transform.position + this.transform.forward, this.transform.position);
                }
                else
                {
                    Projectile.SetDirection(Target.position, this.transform.position);
                }
                Projectile.SetSpeed(GetAttackRangeStat() / lifeTime);
                Projectile.SetLifeTime(lifeTime);
                Projectile.SetDamage(GetAttackStat());

            }

            countdown = atkcooldown;
        }
    }

    // Getter 
    public float GetAttackStat() { return Unit_Stats.GetAtk(); }
    public float GetDefenceStat() { return Unit_Stats.GetDef(); }
    public float GetAttackSpeedStat() { return Unit_Stats.GetAtkS(); }
    public float GetHealthStat() { return Unit_Stats.GetHealth(); }
    public float GetChaseRangeStat() { return Unit_Stats.GetChaseRange(); }
    public float GetMaxHealthStat() { return Unit_Stats.GetMaxHealth(); }
    public Transform GetTarget() { if (Target) { return Target; } return null; }
    public float GetAttackRangeStat() { return Attack_Range_Stat; }
    public bool GetInAttackRange() { return m_InAtkRange; }
    public Vector3 GetHitPoint() { return m_HitPoint; }

    // Setter
    public void SetAttackStat(float _atk) { Unit_Stats.SetAtk(_atk); }
    public void SetDefenceStat(float _def) { Unit_Stats.SetDef(_def); }
    public void SetAttackSpeedStat(float _atkS) { Unit_Stats.SetAtkS(_atkS); }
    public void SetHealthStat(float _health) { Unit_Stats.SetHealth(_health); }
    public void SetChaseRangeStat(float _chaserange) { Unit_Stats.SetChaseRange(_chaserange); }
    public void SetMaxHealthStat(float _maxhealth) { Unit_Stats.SetMaxHealth(_maxhealth); }
    public void SetTarget(Transform _Target) { Target = _Target; }
    public void SetAttackRangeStat(float _atkRange) { Unit_Stats.SetAtkRange(_atkRange);  }
    public void SetInAttackRange(bool _InAtkRange) { m_InAtkRange = _InAtkRange; }
    public void SetHitPoint(Vector3 _hit) { m_HitPoint = _hit; }

    //Other functions
    public void TakeDamage(float _damage)
    {
        //If damage is lower then 1 after minusing defence, Damage dealt is 1
        Unit_Stats.TakeDamage(((_damage - Unit_Stats.GetDef() < 1) ? 1f : _damage - Unit_Stats.GetDef()));
        UpdateHealth();//Health only changes when taking damage or getting healed
    }

    public void UpdateHealth()
    {
        healthBar.fillAmount = GetHealthStat() / GetMaxHealthStat();
    }

    public void AddToUnitsInRange(GameObject Unit)
    {
        UnitsInRange.Add(Unit);
    }

    public void RemoveFromUnitsInRange(GameObject Unit)
    {
        if (Target.gameObject == Unit)
            Target = null;

        UnitsInRange.Remove(Unit);
    }

    public void UpdateCheckList()
    {
        for (int i = 0; i < UnitsInRange.Count; ++i)
        {
            if (!UnitsInRange[i].gameObject)
            {
                UnitsInRange.Remove(UnitsInRange[i]);
            }
        }
    }

    public void StopMoving()
    {
        if (this.transform.parent)
            if (this.transform.parent.GetComponent<AI_Movement>())
                this.transform.parent.GetComponent<AI_Movement>().StopMoving();
    }

    public void StartMoving()
    {
        if (this.transform.parent)
            if (this.transform.parent.GetComponent<AI_Movement>())
                this.transform.parent.GetComponent<AI_Movement>().StartMoving();
    }

    //Currently put here since no other special units yet
    public void FindNearestInList()
    {
        //if (UnitsInRange.Count <= 0)
        //{
        //    Target = null;//If there is nothing in the list, there is no target
        //    return;
        //}

        float nearest = float.MaxValue;
        float temp_dist = 0f;
        for (int i = 0; i < UnitsInRange.Count; ++i)
        {
            temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

            if (temp_dist < nearest)
            {
                nearest = temp_dist;
                Target = UnitsInRange[i].transform;
            }
        }
    }

    public void FindPayload()//Use this function if controller player is not found & target the payload / Init the target position
    {
        Target = GameObject.Find("PayLoad").transform;
    }

    void AddState()//Put all the required states here
    {
        sm.AddState("attack", new Unit_Attack_State(this));
        sm.AddState("chase", new Unit_Chase_State(this));
        sm.AddState("chase_cart", new Unit_ChaseCart_State(this));
        sm.AddState("dead", new Unit_Dead_State(this));
    }

    public void ChangeState(string name)
    {
        sm.ChangeState(name);
    }

    public int InRangeCount()
    {
        return UnitsInRange.Count;
    }

    public void Dead()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
