using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;



public class Entity_Unit : MonoBehaviour
{

    protected enum Unit_Type
    {
        NONE,
        FLY,
        MELEE,
        RANGE
    }

    protected enum Piority
    {
        NONE,
        PAYLOAD,
        PLAYER
    }

    [SerializeField]
    Unit_Type Unit = Unit_Type.NONE;

    [SerializeField]
    Piority Piority_Unit = Piority.NONE;

    [SerializeField]
    bool idle = false;

    [SerializeField]
    bool instantChasePlayer = false;

    //Target Player Instant
    Transform PlayerTransform = null;

    protected string priority = "";

    [SerializeField]
    protected float Health_Stat = 1f;//Attack stats of unit

    [SerializeField]
    protected float Attack_Stat = 1f;//Attack stats of unit

    [SerializeField]
    protected float Defence_Stat = 0f;//Defence stats of unit

    [SerializeField]
    protected float Attack_Speed_Stat = 0f;//Attack speed of unit

    [SerializeField]
    protected float Attack_Range_Stat = 5f;//Attack Range of unit

    [SerializeField]
    protected float Chase_Range_Stat = 5f;//Chase Range of unit

    [SerializeField]
    protected float Move_Speed_Stat = 3f;//Attack stats of unit

    [SerializeField]
    protected GameObject Projectile_Prefeb = null;

    [SerializeField]
    protected List<GameObject> UnitsInRange = new List<GameObject>();

    [SerializeField]
    protected Image healthBar;//Put healthbar image inside here (The one that change not prefeb)

    [SerializeField]
    protected string statename;

    [SerializeField]
    protected float Summoning_timer = 0f;

    [SerializeField]
    protected float Take_Damage_timer = 0f;

    protected Transform Target;



    protected Entity_Stats Unit_Stats = new Entity_Stats();

    protected Unit_StateMachine sm = new Unit_StateMachine();


    protected bool m_InAtkRange = false;
    protected Vector3 m_HitPoint = Vector3.zero;

    protected AudioSource UnitThatProduceSound;

    [SerializeField]
    protected AudioClip AttackSound;

    //For Attack
    protected float atkcooldown = 0f;
    protected float countdown = -1f;
    protected bool stillAttacking = false;

    //For day night cycle
    DayNightCycle daynightInstance = null;
    protected Stats_ResourceScript resource = null;
    // Use this for initialization

    private void Awake()
    {
        SetHealthStat(Health_Stat);
        SetMaxHealthStat(Health_Stat);//Starting health = maxHealth
        SetAttackStat(Attack_Stat);
        SetDefenceStat(Defence_Stat);
        SetAttackSpeedStat(Attack_Speed_Stat);
        SetAttackRangeStat(Attack_Range_Stat);
        SetOriginalMoveSpeed(Move_Speed_Stat);
        SetMoveSpeed(Move_Speed_Stat);

        //Debug.Log("Range Value : " + Range_Stat);

        atkcooldown = 1f / GetAttackSpeedStat();

        SetChaseRangeStat(Chase_Range_Stat);
        AddState();



    }
    void Start ()
    {

        Stats_ResourceScript.Instance.EnemyCount++;
        resource = Stats_ResourceScript.Instance;
        ChangeState("summon");

        if (Piority_Unit == Piority.PAYLOAD)
        {
            priority = "Payload";//Tag payload
        }
        else if (Piority_Unit == Piority.PLAYER)
        {
            priority = "Player2";//Tag controller player
        }

        PlayerTransform = GameObject.Find("PS4_Player").transform;
        //if (Random.Range(0f, 1f) < 0.5f)
        //{
        //    idle = true;
        //}

        daynightInstance = DayNightCycle.Instance;
        SelfStart();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Day : " + daynightInstance.isDaytime);
        //Debug.Log("Hi from Entity_Unit Script");
        //Leave here for Debug purposes only v
        //if (Debug.isDebugBuild)//Only in debug do we need to change Stats during runtime menually
        //{
        //    SetHealthStat(Health_Stat);
        //    SetAttackStat(Attack_Stat);
        //    SetDefenceStat(Defence_Stat);
        //    SetAttackSpeedStat(Attack_Speed_Stat);
        //    UpdateHealth();//is in taking damage & Healing(If applicable)
        //}
        //Debug Purposes only ^

        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    Stun();
        //}

        //Debug.Log("Yes yes have is active and not null");

        UpdateCheckList();

        if (Target)
        {
            if (!Target.gameObject.activeSelf || (Target.tag == "Player2" && (int)resource.m_P2_hp <= 0))
            {
                Debug.Log("NUll leh");
                Target = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(1);
        }

        SelfUpdate();

        if (countdown >= 0)
            countdown -= Time.deltaTime;

        if (GetHealthStat() <= 0 && sm.GetCurrentStateName() != "dead")
        {
            ChangeState("dead");
        }
        //Debug.Log("State machine is " + sm.GetCurrentStateName());
        sm.ExecuteStateUpdate();//Updating statemachine
        MoveSpeedUpdate();
        statename = sm.GetCurrentStateName();

        UpdateisParented();
        UpdateInspector();

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
    public bool GetStillAttacking() { return stillAttacking; }
    public bool GetisIdle() { return idle; }
    public float GetOriginalMoveSpeed() { return Unit_Stats.GetOriginalMoveSpeed(); }
    public float GetMoveSpeed() { return Unit_Stats.GetMoveSpeed(); }

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
    public void SetStillAttacking(bool _stillAttacking) { stillAttacking = _stillAttacking; }
    public void SetOriginalMoveSpeed(float _originalmovespeed) { Unit_Stats.SetOriginalMoveSpeed(_originalmovespeed); }
    public void SetMoveSpeed(float _movespeed) { Unit_Stats.SetMoveSpeed(_movespeed); }

    //Getter & Setter For AnimationTimer
    public void SetSummoningTimer(float _Summoning_timer) { Summoning_timer = _Summoning_timer; }
    public float GetSummoningTimer() { return Summoning_timer; }

    public void SetTakeDamageTimer(float _Take_Damage_timer) { Take_Damage_timer = _Take_Damage_timer; }
    public float GetTakeDamageTimer() { return Take_Damage_timer; }

    //Other functions
    public virtual void Attack()
    {

        if (countdown < 0)
        {
            // Animation start animation
            if (GetComponent<AnimationScript>())
            {
                GetComponent<AnimationScript>().SetAnimTrigger(0);
            }

            float lifeTime = 1f;//Temporary hard coding it here
                                //DO shooting projectile here
            if (Projectile_Prefeb)
            {
                if (GetComponent<AnimationScript>())
                {
                    if (GetComponent<AnimationScript>().Sync_Projectile())
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
                        Projectile.SetProjectileTag(this.tag);

                    }
                }
                
            }

            countdown = atkcooldown;
        }
    }

    public void MoveSpeedUpdate()
    {
        if (!daynightInstance)
            return;

        //Debug.Log(this.GetMoveSpeed());

        if (daynightInstance.isDaytime)
        {
            SetMoveSpeed(GetOriginalMoveSpeed());
        }
        else
        {
            SetMoveSpeed(GetOriginalMoveSpeed() * 3);
        }

        ChangeAgentMovespeed(GetMoveSpeed());
    }

    //Currently put here since no other special units yet
    //public virtual void FindNearestInList()
    //{
    //    //if (UnitsInRange.Count <= 0)
    //    //{
    //    //    Target = null;//If there is nothing in the list, there is no target
    //    //    return;
    //    //}

    //    float nearest = float.MaxValue;
    //    float temp_dist = 0f;
    //    for (int i = 0; i < UnitsInRange.Count; ++i)
    //    {
    //        if (!UnitsInRange[i] || !UnitsInRange[i].activeSelf)
    //            continue;

    //        temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

    //        if (temp_dist < nearest)
    //        {
    //            nearest = temp_dist;
    //            Target = UnitsInRange[i].transform;
    //        }
    //    }
    //}

    //public virtual void FindNearestInList()
    //{
    //    float nearest = float.MaxValue;
    //    float temp_dist = 0f;
    //    for (int i = 0; i < UnitsInRange.Count; ++i)
    //    {
    //        if (UnitsInRange[i].tag == "Player2")
    //        {
    //            if ((int)resource.m_P2_hp <= 0)//Auto skip if player is dead
    //                continue;

    //            if (UnitsInRange[i].transform.parent != null)
    //            {
    //                //if (UnitsInRange[i].transform.parent != this.transform)
    //                if (UnitsInRange[i].transform.parent.GetComponent<Entity_Tengu>())
    //                {
    //                    continue;
    //                }
    //            }
    //        }

    //        temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

    //        if (Target && Target.gameObject.activeSelf && (int)resource.m_P2_hp > 0)
    //        {
    //            //If the current target is not piority && the currently compared unit is piority
    //            if (Target.tag != priority && (UnitsInRange[i].tag == priority))
    //            {
    //                //Force Assign
    //                nearest = temp_dist;
    //                Target = UnitsInRange[i].transform;
    //                continue;
    //            }
    //            //If the current target is piority && the currently compared unit is not piority
    //            else if (Target.tag == priority && (UnitsInRange[i].tag != priority))
    //            {
    //                //Ignore and continue
    //                continue;
    //            }
    //        }

    //        if (temp_dist < nearest)
    //        {
    //            nearest = temp_dist;
    //            Target = UnitsInRange[i].transform;
    //        }
    //    }
    //}

    #region use this for playtest
    //For Testing(Attack only Piority)
    public virtual void FindNearestInList()
    {
        float nearest = float.MaxValue;
        float temp_dist = 0f;
        for (int i = 0; i < UnitsInRange.Count; ++i)
        {

            if (UnitsInRange[i].tag != priority)
                continue;

            if (UnitsInRange[i].tag == "Player2")
            {
                if ((int)resource.m_P2_hp <= 0)//Auto skip if player is dead
                    continue;

                if (UnitsInRange[i].transform.parent != null)
                {
                    //if (UnitsInRange[i].transform.parent != this.transform)
                    if (UnitsInRange[i].transform.parent.GetComponent<Entity_Tengu>())
                    {
                        continue;
                    }
                }
            }

            temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

            if (Target && Target.gameObject.activeSelf && (int)resource.m_P2_hp > 0)
            {
                //If the current target is not piority && the currently compared unit is piority
                if (Target.tag != priority && (UnitsInRange[i].tag == priority))
                {
                    //Force Assign
                    nearest = temp_dist;
                    Target = UnitsInRange[i].transform;
                    continue;
                }
                //If the current target is piority && the currently compared unit is not piority
                else if (Target.tag == priority && (UnitsInRange[i].tag != priority))
                {
                    //Ignore and continue
                    continue;
                }
            }

            if (temp_dist < nearest)
            {
                nearest = temp_dist;
                Target = UnitsInRange[i].transform;
            }
        }
    }
    #endregion

    #region use this for actual game
    //public virtual void FindNearestInList()
    //{
    //    Debug.Log("Testing_Part_1");
    //    float nearest = float.MaxValue;
    //    float temp_dist = 0f;
    //    for (int i = 0; i < UnitsInRange.Count; ++i)
    //    {
    //        if (UnitsInRange[i].tag == "Player2")
    //        {
    //            if (UnitsInRange[i].transform.parent != null)
    //            {
    //                //if (UnitsInRange[i].transform.parent != this.transform)
    //                if (UnitsInRange[i].transform.parent.GetComponent<Entity_Tengu>())
    //                {
    //                    continue;
    //                }
    //            }
    //        }

    //        temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;
    //        Debug.Log("Testing_Part_2");
    //        //Debug.Log(UnitsInRange[i].name);

    //        if (Target && Target.gameObject.activeSelf)
    //        {
    //            Debug.Log("Testing name : " + Target.name);
    //            //If the current target is not piority && the currently compared unit is piority
    //            if (Target.tag != priority && (UnitsInRange[i].tag == priority))
    //            {
    //                //Force Assign
    //                nearest = temp_dist;
    //                Target = UnitsInRange[i].transform;
    //                continue;
    //            }
    //            //If the current target is piority && the currently compared unit is not piority
    //            else if (Target.tag == priority && (UnitsInRange[i].tag != priority))
    //            {
    //                //Ignore and continue
    //                continue;
    //            }
    //        }

    //        if (temp_dist < nearest)
    //        {
    //            Debug.Log("Got reach here");
    //            nearest = temp_dist;
    //            Target = UnitsInRange[i].transform;
    //        }



    //        //Debug.Log("Here");

    //        //if (UnitsInRange[i].tag == "Player2")//Checking to see if it is player 2
    //        //{
    //        //    if (!UnitsInRange[i] || !UnitsInRange[i].activeSelf || (int)resource.m_P2_hp > 0)
    //        //    {
    //        //        continue;//if it is not alive, dont target it
    //        //    }
    //        //    else if (UnitsInRange[i].transform.parent != null)
    //        //    {
    //        //        //if (UnitsInRange[i].transform.parent != this.transform)
    //        //        if (UnitsInRange[i].transform.parent.GetComponent<Entity_Tengu>())
    //        //        {
    //        //            continue;
    //        //        }
    //        //    }
    //        //}

    //        //temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

    //        //if (Target && Target.gameObject.activeSelf && (int)resource.m_P2_hp > 0)
    //        //{
    //        //    //If the current target is not piority && the currently compared unit is piority
    //        //    if (Target.tag != priority && (UnitsInRange[i].tag == priority))
    //        //    {
    //        //        //Force Assign
    //        //        nearest = temp_dist;
    //        //        Target = UnitsInRange[i].transform;
    //        //        continue;
    //        //    }
    //        //    //If the current target is piority && the currently compared unit is not piority
    //        //    else if (Target.tag == priority && (UnitsInRange[i].tag != priority))
    //        //    {
    //        //        //Ignore and continue
    //        //        continue;
    //        //    }
    //        //}

    //        //if (temp_dist < nearest && UnitsInRange[i].tag == priority)
    //        //{
    //        //    nearest = temp_dist;
    //        //    Target = UnitsInRange[i].transform;
    //        //}
    //    }
    //}
    #endregion

    public virtual void AddState()//Put all the required states here
    {
        sm.AddState("attack", new Unit_Attack_State(this));
        sm.AddState("chase", new Unit_Chase_State(this));
        //sm.AddState("chase_cart", new Unit_ChaseCart_State(this));
        sm.AddState("dead", new Unit_Dead_State(this));
        sm.AddState("stun", new Unit_Stun_State(this, 5f));
        sm.AddState("roam", new Unit_Roam_State(this));
        sm.AddState("afk", new Unit_AFK_State(this));
        sm.AddState("takedamage", new Unit_TakeDamage_State(this));
        sm.AddState("summon", new Unit_Summoned_State(this));
    }

    public virtual void SelfUpdate()//Use this to update units without overrideing original update
    {

    }

    public virtual void SelfStart()
    {

    }

    public void TakeDamage(float _damage)
    {
        if (GetHealthStat() <= 0)
            return;

        if (this.GetComponent<Force_Field>() && this.GetComponent<Force_Field>().GetIsActive())
            return;

        gameObject.AddComponent<Entity_Take_Damage>();
        //If damage is lower then 1 after minusing defence, Damage dealt is 1
        Unit_Stats.TakeDamage((_damage - Unit_Stats.GetDef() < 1) ? 1f : _damage - Unit_Stats.GetDef());

        ChangeState("takedamage");

        UpdateHealth();//Health only changes when taking damage or getting healed
    }

    public void UpdateHealth()
    {
        if (healthBar)
        {
            healthBar.fillAmount = GetHealthStat() / GetMaxHealthStat();
        }
    }

    public void AddToUnitsInRange(GameObject Unit)
    {
        UnitsInRange.Add(Unit);
    }

    public void RemoveFromUnitsInRange(GameObject Unit)
    {
        if(Target)
            if (Target.gameObject == Unit)
                Target = null;

        UnitsInRange.Remove(Unit);
    }

    public void UpdateCheckList()
    {
        for (int i = 0; i < UnitsInRange.Count; ++i)
        {
            if (!UnitsInRange[i].gameObject || !UnitsInRange[i].gameObject.activeSelf || (UnitsInRange[i].tag == "Player2" && (int)resource.m_P2_hp <= 0))
            {
                //Debug.Log("Hehe");
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

    public void MoveToTargetedPosition(Vector3 _target)
    {
        if (!instantChasePlayer)
        {
            if (!this.transform.parent)
                return;

            if (!this.transform.parent.GetComponent<AI_Movement>())
                return;

            Vector3 MoveTo = _target;
            MoveTo.y = this.transform.parent.transform.position.y;
            this.transform.parent.GetComponent<AI_Movement>().SetTargetPosition(MoveTo);
        }
        else
        {
            Vector3 MoveTo = PlayerTransform.position;
            MoveTo.y = this.transform.parent.transform.position.y;
            this.transform.parent.GetComponent<AI_Movement>().SetTargetPosition(MoveTo);
        }
    }

    public void ChangeAgentPosition(Vector3 pos)
    {
        if (this.transform.parent)
            if (this.transform.parent.GetComponent<AI_Movement>())
                this.transform.parent.GetComponent<AI_Movement>().ChangeNavAgentPosition(pos);
    }

    public void ChangeAgentMovespeed(float movespeed)
    {
        if (this.transform.parent)
            if (this.transform.parent.GetComponent<NavMeshAgent>())
                this.transform.parent.GetComponent<NavMeshAgent>().speed = movespeed;
    }

    public void FindPayload()//Use this function if controller player is not found & target the payload / Init the target position
    {
        Target = GameObject.Find("PayLoad").transform;
    }

    public void ChangeState(string name)
    {
        sm.ChangeState(name);
    }

    public void ReturnPreviousState()
    {
        sm.ChangeToPrevious();
    }

    public int InRangeCount()
    {
        return UnitsInRange.Count;
    }

    public virtual void Dead()
    {

        Destroy(this.gameObject.transform.parent.gameObject);
    }

    public void Stun()
    {
        this.sm.ChangeState("stun");
    }

    public bool GetinstantChasePlayer()
    {
        return instantChasePlayer;
    }

    private void UpdateisParented()
    {
        //used to check if it is being grabbed
        if (this.transform.parent && this.transform.parent.parent)
        {
            if (this.transform.parent.parent.GetComponent<PickupHandlerScript>())
                Stun();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!transform.parent.GetComponent<NavMeshAgent>().enabled)
        {
            if (other.tag == "floor")
            {
                transform.parent.GetComponent<NavMeshAgent>().enabled = true;
                transform.parent.GetComponent<AI_Movement>().enabled = true;
                transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.parent.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }


    void UpdateInspector()
    {
       Health_Stat = GetHealthStat();//Attack stats of unit

       Attack_Stat = GetAttackStat();//Attack stats of unit

       Defence_Stat = GetDefenceStat();//Defence stats of unit

       Attack_Speed_Stat = GetAttackSpeedStat();//Attack speed of unit

       Attack_Range_Stat = GetAttackRangeStat();//Attack Range of unit

       Chase_Range_Stat = GetChaseRangeStat();//Chase Range of unit

       Move_Speed_Stat = GetMoveSpeed();//Attack stats of unit
    }
   


}
