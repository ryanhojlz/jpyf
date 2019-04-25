using System.Collections.Generic;
using UnityEngine;

public class Entity_Tengu : Entity_Unit
{
    //The waypoints infront of cart////
    GameObject TargetEscape = null;  //
    GameObject TargetLeft = null;    //
    GameObject TargetRight = null;   //
    ///////////////////////////////////

    //Bool For Attacking Animation/////
    bool isAttacking = false;

    ////Bool for Attacking unit
    bool returnTo = false;
    bool moveRight = false;
    bool isGrabbing = false;

    //Variables////////////////////////
    public float flyspeed = 10f;
    GameObject HoldUnit = null;

    float StayDuration = 2f;
    float NextTime = 0f;

    enum AtkPlayer
    {
        GRAB = 0,
        POSITIONING,
        HOVER
    }

    enum AtkPayload
    {
        ATTACK,
        GOBACK,
    }

    AtkPlayer AttackPlayerSeq = AtkPlayer.GRAB;//Initing it to be grabbing player
    AtkPayload AttackPayloadSeq = AtkPayload.ATTACK;

    //Override functions
    public override void SelfStart()
    {
        TargetEscape = GameObject.Find("TenguEscapePoint");
        TargetLeft = GameObject.Find("TenguEscapePointLeft");
        TargetRight = GameObject.Find("TenguEscapePointRight");

        AttackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().TNG_attack;
        UnitThatProduceSound = this.GetComponent<AudioSource>();
        UnitThatProduceSound.clip = AttackSound;


        this.transform.position = GetOriginalPosition();//Vector3(this.transform.position.x, transform.parent.position.y + this.GetAttackRangeStat(), this.transform.position.z)
    }

    public override void SelfUpdate()
    {

        if (GetHealthStat() <= 0)
        {
            if (HoldUnit)
            {
                HoldUnit.transform.parent = null; // Unparent
                HoldUnit.GetComponent<Rigidbody>().isKinematic = false; // make gravity true so that the target drop to the ground after tengu dies
                Destroy(this.gameObject.transform.parent.gameObject);
            }
        }

        if (HoldUnit)
        {
            AttackPlayer();
        }
        else if (isAttacking && GetTarget())
        {
            if (GetTarget().tag == "Player2")
            {
                if (GetTarget().gameObject && GetTarget().gameObject.activeSelf)
                    HoldUnit = GetTarget().gameObject;
            }
            else if (GetTarget().tag == "Payload")
            {
                AttackPayload();
            }
        }

        Debug.Log("This one " + GetTarget());

        if ((!HoldUnit || !HoldUnit.activeSelf) && isGrabbing)//This will run if it used to be grabbing something but now not
        {
            if (HoldUnit)
            {
                HoldUnit.transform.parent = null; // Unparent
                HoldUnit.GetComponent<Rigidbody>().isKinematic = false; // make gravity true so that the target drop to the ground after tengu dies
            }
            HoldUnit = null;
            SetStillAttacking(false);
            SetTarget(null);
            AttackPlayerSeq = AtkPlayer.GRAB;
            this.transform.position = GetOriginalPosition();
            isGrabbing = false;
            SetInAttackRange(false);
            isAttacking = false;

            GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>().SetGropper(null);
        }
    }

    public override void Attack()
    {
        SetStillAttacking(true);//Set to start attacking
        isAttacking = true;
    }

    public override void Dead()
    {
        GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>().SetGropper(null);

        if (HoldUnit)
        {
            HoldUnit.transform.parent = null; // Unparent
            HoldUnit.GetComponent<Rigidbody>().isKinematic = false; // make gravity true so that the target drop to the ground after tengu dies
            Destroy(this.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }

    //Tengu Only Function

    Vector3 GetOriginalPosition()
    {
        Vector3 Temp = Vector3.zero;
        if (transform.parent)
        {
            if (transform.parent.GetComponent<AI_Movement>())
            {
                Temp.x = this.transform.parent.position.x;
                Temp.y = transform.parent.position.y + this.GetAttackRangeStat();
                Temp.z = this.transform.parent.position.z;
                return Temp;
            }
        }
        return Temp;
    }

    Vector3 GetOppositePosition(Vector3 _OriginalPos, Vector3 CenterPoint)
    {
        Vector3 OppositePos = Vector3.zero;

        Vector3 OriginalPos = _OriginalPos;

        Vector3 Direction = CenterPoint - OriginalPos;

        Direction.y = -Direction.y;//Making it up the other way

        OppositePos += Direction;//This is the opposite position

        return OppositePos;
    }

    void AttackPlayer()
    {
        //Debug.Log(HoldUnit);
        //Debug.Log("Still attacking");
        if (!HoldUnit || !HoldUnit.activeSelf)
        {
            Debug.Log("Oh no, U are not holding at units");
            return;
        }

        switch (AttackPlayerSeq)
        {
            case AtkPlayer.GRAB:
                {
                    if (FlyToTarget(HoldUnit.transform.position, flyspeed))// <- This portion will be used to fly to target position && tell if it has reached
                    {
                        if (HoldUnit.transform.parent)//Being parented to a cart
                        {
                            HoldUnit.transform.parent = null;
                        }
                        
                        HoldUnit.transform.parent = this.transform;
                        isGrabbing = true;
                        HoldUnit.GetComponent<Rigidbody>().isKinematic = true;
                        GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>().SetGropper(this.transform);
                        AttackPlayerSeq = AtkPlayer.POSITIONING;
                    }
                }
                break;
            case AtkPlayer.POSITIONING:
                {
                    if(FlyToTarget(TargetEscape.transform.position, flyspeed))
                    {
                        AttackPlayerSeq = AtkPlayer.HOVER;
                    }
                }
                break;
            case AtkPlayer.HOVER:
                {
                    if (FlyToLeft_Right(moveRight))
                    {
                        moveRight = !moveRight;
                    }
                }
                break;
        }
    }

    void AttackPayload()
    {
        if (!GetTarget())
        {
            return;
        }

        switch (AttackPayloadSeq)
        {
            case AtkPayload.ATTACK:
                {
                    if (Time.time > NextTime)
                    {
                        if (FlyToTarget(GetTarget().position, flyspeed))
                        {
                            GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>().Lantern_TakeDmg((int)this.GetAttackStat());
                            NextTime = Time.time + StayDuration;//Setting the timer for tengu to stay on the spot its attacking
                            AttackPayloadSeq = AtkPayload.GOBACK;
                        }
                    }
                }
                break;

            case AtkPayload.GOBACK:
                {
                    if (Time.time > NextTime)
                    {
                        if (FlyToTarget(GetOriginalPosition(), flyspeed))
                        {
                            AttackPayloadSeq = AtkPayload.ATTACK;
                            NextTime = Time.time + StayDuration;//Setting the timer for tengu to stay on the spot after flying back
                        }
                    }
                }
                break;
        }
    }

    bool FlyToTarget(Vector3 _Pos, float speed)
    {
        Vector3 Temp = _Pos - this.transform.position;
        if (Temp != Vector3.zero)
        {
            Vector3 Dir = (_Pos - this.transform.position).normalized;//Get the direction to fly to

            this.transform.position += Dir * speed * Time.deltaTime;
        }

        if ((_Pos - this.transform.position).magnitude < 0.1f)
            return true;//If have reached target position, return true

        return false;
    }

    //bool FlyToTarget(Vector3 _Pos, float speed, float time)
    //{
    //    Vector3 Temp = _Pos - this.transform.position;
    //    if (Temp != Vector3.zero)
    //    {
    //        Vector3 Dir = (_Pos - this.transform.position).normalized;//Get the direction to fly to

    //        this.transform.position += Dir * speed * Time.deltaTime;
    //    }

    //    if ((_Pos - this.transform.position).magnitude < 0.1f)
    //        return true;//If have reached target position, return true

    //    return false;
    //}

    bool FlyToLeft_Right(bool right)
    {
        //this.GetTarget().transform.position = this.transform.position;

        if (this.transform.childCount > 0)
        {
            this.transform.GetChild(0).position = this.transform.position;
        }
        if (right)
        {

            Vector3 TargetPos = TargetRight.transform.position;

            if ((TargetPos - this.transform.position).magnitude < 1f)
            {
                return true;
            }
            Vector3 dir = Vector3.zero;


            dir = (TargetPos - this.transform.position).normalized;

            Vector3 vel = dir * flyspeed * Time.deltaTime;

            this.transform.position += vel;


        }
        else
        {

            Vector3 TargetPos = TargetLeft.transform.position;

            if ((TargetPos - this.transform.position).magnitude < 1f)
            {
                return true;
            }

            Vector3 dir = Vector3.zero;

            dir = (TargetPos - this.transform.position).normalized;

            Vector3 vel = dir * flyspeed * Time.deltaTime;


            this.transform.position += vel;


        }

        //Debug.Log("Testing Sin : " + Mathf.Sin(SinCounter += Time.deltaTime));

        this.transform.position += new Vector3(0, (Mathf.Sin(Time.time % 3.45f )) * 0.05f, 0);

        return false;
    }
}

//public class Entity_Tengu : Entity_Unit
//{
//    private Vector3 prevPosition;
//    private bool startAttack;

//    private bool GoBack = false;
//    private bool reachPrev = false;
//    private bool hovering = false;

//    float SinCounter = 0f;

//    bool moveRight = true;
//    //For Grabbing player
//    GameObject TargetEscape = null;
//    GameObject TargetLeft = null;
//    GameObject TargetRight = null;

//    public float flySpeed = 10f;
//    public float glidingSpeed = 5f;
//    //For Grabbing player

//    //For hitting cart
//    bool hitCart = false;
//    bool goOriginPos = false;
//    //For hitting cart

//    //Testing new attack animation
//    bool change_opp = false;
//    Vector3 oppPos = Vector3.zero;
//    bool isoppTrue = false;
//    float staytimer = 0f;


//    public override void SelfStart()
//    {
//        TargetEscape = GameObject.Find("TenguEscapePoint");
//        TargetLeft = GameObject.Find("TenguEscapePointLeft");
//        TargetRight = GameObject.Find("TenguEscapePointRight");

//        startAttack = true;
//        AttackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().TNG_attack;
//        UnitThatProduceSound = this.GetComponent<AudioSource>();
//        UnitThatProduceSound.clip = AttackSound;


//        this.transform.position = SetOriginalPosition();//Vector3(this.transform.position.x, transform.parent.position.y + this.GetAttackRangeStat(), this.transform.position.z)
//    }

//    public override void SelfUpdate()
//    {
//        //if (Input.GetKeyDown(KeyCode.A))
//        //{
//        //    this.SetHealthStat(0);
//        //}

//        if (GetHealthStat() == 0) // if tengu dies
//        {
//            this.GetTarget().parent = null; // Unparent
//            this.GetTarget().GetComponent<Rigidbody>().useGravity = true; // make gravity true so that the target drop to the ground after tengu dies
//            //Debug.Log("child count: " + this.transform.childCount);
//            //Dead(); 
//            Destroy(gameObject); // destroy tengu gameobject
//        }

//        if (GoBack)
//        {
//            //Debug.Log("GO BACK");
//            GoToCartFront();
//        }
//        else if (hovering)
//        {
//            if (FlyToLeft_Right(moveRight))
//            {
//                moveRight = !moveRight;
//            }
//        }

//        if (goOriginPos)
//        {
//            //ReturnToOrigin();
//            FlyToOpposite();
//        }
//    }

//    public override void Attack()
//    {
//        //if (transform.childCount > 0)
//        //{
//        //    if (reachPrev == false)
//        //        GoBack = true;

//        //    return;
//        //}

//        ////Debug.Log("Still Attack for what");

//        ////this.gameObject.transform.position = new Vector3(Target.position.x, Target.position.y, Target.position.z);

//        //if ((this.GetTarget().position - this.transform.position).magnitude > 0.5f && this.transform.childCount == 0) // Distance less than 2 and tengu not grabbing the target
//        //{
//        //    //Debug.Log("Tengu found player");
//        //    // Go down to the target
//        //    Vector3 dir = Vector3.zero; // get direction
//        //    Vector3 TargetPos = this.GetTarget().position; // get the target position

//        //    if (startAttack)
//        //    {
//        //        startAttack = false;
//        //        reachPrev = false;
//        //    }

//        //    //TargetPos.y += this.GetTarget().lossyScale.y;

//        //    dir = (this.GetTarget().position - this.transform.position).normalized; // direction equals to distance between the two normalised

//        //    Vector3 vel = dir * 10 * Time.deltaTime; // velocity

//        //    this.transform.position += vel; // Making the tengu move
//        //}
//        //else if (this.transform.childCount == 0) // tengu not grabbing the target
//        //{
//        //    this.GetTarget().transform.position = this.transform.position;
//        //    this.GetTarget().parent = this.transform; // parent the target to the tengu as the tengu grabs the target
//        //    this.GetTarget().GetComponent<Rigidbody>().useGravity = false; // make gravity false so that the target won't drop to the ground
//        //    GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>().SetGropper(this.transform);
//        //    Debug.Log(GetTarget().parent);
//        //    //Grabbing();
//        //}
//        //SetStillAttacking(true);

//        if (Target.tag == "Player2")
//        {
//            HitPlayer();
//        }
//        else if (Target.tag == "Payload")
//        {
//            HitCart();
//        }
//    }

//    public void HitCart()
//    {
//        if ((this.GetTarget().position - this.transform.position).magnitude > 0.5f && !goOriginPos) // Distance less than 2 and tengu not grabbing the target
//        {
//            //Debug.Log("Tengu found player");
//            // Go down to the target
//            Vector3 dir = Vector3.zero; // get direction
//            Vector3 TargetPos = this.GetTarget().position; // get the target position

//            //TargetPos.y += this.GetTarget().lossyScale.y;

//            dir = (this.GetTarget().position - this.transform.position).normalized; // direction equals to distance between the two normalised

//            Vector3 vel = dir * 10 * Time.deltaTime; // velocity

//            this.transform.position += vel; // Making the tengu move

//            if (isoppTrue)
//            {
//                //this.transform.Rotate(this.transform.up * 10000 * Time.deltaTime);
//            }
//        }
//        else if (!hitCart)
//        {
//            hitCart = true;
//            goOriginPos = true;

//            GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>().Lantern_TakeDmg((int)this.GetAttackStat());
//        }
//    }

//    public void ReturnToOrigin()
//    {
//        Vector3 dir = Vector3.zero; // get direction
//        Vector3 TargetPos = SetOriginalPosition(); // get the target position

//        dir = (TargetPos - this.transform.position).normalized; // direction equals to distance between the two normalised

//        Vector3 vel = dir * (flySpeed * 0.2f) * Time.deltaTime; // velocity

//        this.transform.position += vel; // Making the tengu move

//        //this.transform.position = TargetPos;

//        if ((TargetPos - this.transform.position).magnitude < 0.5f)
//        {
//            goOriginPos = false;
//            hitCart = false;
//        }
//    }

//    public void FlyToOpposite()
//    {
//        if (!change_opp)
//        {
//            Vector3 Origin = SetOriginalPosition();//Finding its originalPosition
//            change_opp = true;
//            oppPos = this.transform.position - Origin;
//            oppPos.y = -oppPos.y;

//            oppPos = this.transform.position + oppPos;

//            if (isoppTrue)
//            {
//                staytimer = 1f;
//            }
//        }

//        Vector3 dir = Vector3.zero; // get direction
//        Vector3 TargetPos = oppPos; // get the target position
//        float tempSpeed = flySpeed;

//        if (isoppTrue)//If is at the opposite side
//        {
//            TargetPos = SetOriginalPosition();//Fly back to original
//            staytimer -= Time.deltaTime;
//            if (staytimer > 0f)
//            {
//                tempSpeed *= 0.2f;
//            }
//        }




//        dir = (TargetPos - this.transform.position).normalized; // direction equals to distance between the two normalised

//        Vector3 vel = dir * (tempSpeed) * Time.deltaTime; // velocity

//        this.transform.position += vel; // Making the tengu move

//        //this.transform.position = TargetPos;


//        if ((TargetPos - this.transform.position).magnitude < 0.1f)
//        {
//            Vector3 Origin = SetOriginalPosition();
//            goOriginPos = false;
//            hitCart = false;
//            change_opp = false;
//            oppPos.y = Origin.y;
//            isoppTrue = !isoppTrue;
//            //ChangeAgentPosition(new Vector3 (this.transform.position.x, Origin.y , this.transform.position.z));

//            //Origin = SetOriginalPosition();

//            //this.transform.position = new Vector3(Origin.x, this.transform.position.y, Origin.z);
//        }

//    }

//    public void HitPlayer()
//    {
//        if (transform.childCount > 0)
//        {
//            if (reachPrev == false)
//                GoBack = true;

//            return;
//        }

//        //Debug.Log("Still Attack for what");

//        //this.gameObject.transform.position = new Vector3(Target.position.x, Target.position.y, Target.position.z);

//        if ((this.GetTarget().position - this.transform.position).magnitude > 0.5f && this.transform.childCount == 0) // Distance less than 2 and tengu not grabbing the target
//        {
//            //Debug.Log("Tengu found player");
//            // Go down to the target
//            Vector3 dir = Vector3.zero; // get direction
//            Vector3 TargetPos = this.GetTarget().position; // get the target position

//            if (startAttack)
//            {
//                startAttack = false;
//                reachPrev = false;
//            }

//            //TargetPos.y += this.GetTarget().lossyScale.y;

//            dir = (this.GetTarget().position - this.transform.position).normalized; // direction equals to distance between the two normalised

//            Vector3 vel = dir * 10 * Time.deltaTime; // velocity

//            this.transform.position += vel; // Making the tengu move
//        }
//        else if (this.transform.childCount == 0) // tengu not grabbing the target
//        {
//            this.GetTarget().transform.position = this.transform.position;
//            this.GetTarget().parent = this.transform; // parent the target to the tengu as the tengu grabs the target
//            this.GetTarget().GetComponent<Rigidbody>().useGravity = false; // make gravity false so that the target won't drop to the ground
//            GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>().SetGropper(this.transform);
//            Debug.Log(GetTarget().parent);
//            //Grabbing();
//        }
//    }

//    public void GoToCartFront()
//    {
//        if (transform.parent)
//        {
//            Transform Temp = transform.parent;
//            transform.parent = null;
//            Destroy(Temp.gameObject);
//            Grabbing();

//            Debug.Log("HIIIII");
//        }

//        //Debug.Log("HIIIII");

//        Vector3 dir = Vector3.zero;
//        Vector3 TargetPos = TargetEscape.transform.position;

//        dir = (TargetPos - this.transform.position).normalized;

//        Vector3 vel = dir * flySpeed * Time.deltaTime;

//        if ((TargetPos - this.transform.position).magnitude < 1f)
//        {
//            GoBack = false;
//            hovering = true;
//        }

//        this.transform.position += vel;
//    }

//    bool FlyToLeft_Right(bool right)
//    {
//        //this.GetTarget().transform.position = this.transform.position;

//        if (this.transform.childCount > 0)
//        {
//            this.transform.GetChild(0).position = this.transform.position;
//        }
//        if (right)
//        {

//            Vector3 TargetPos = TargetRight.transform.position;

//            if ((TargetPos - this.transform.position).magnitude < 1f)
//            {
//                GoBack = false;
//                return true;
//            }
//            Vector3 dir = Vector3.zero;


//            dir = (TargetPos - this.transform.position).normalized;

//            Vector3 vel = dir * glidingSpeed * Time.deltaTime;

//            this.transform.position += vel;


//        }
//        else
//        {

//            Vector3 TargetPos = TargetLeft.transform.position;

//            if ((TargetPos - this.transform.position).magnitude < 1f)
//            {
//                GoBack = false;
//                return true;
//            }

//            Vector3 dir = Vector3.zero;

//            dir = (TargetPos - this.transform.position).normalized;

//            Vector3 vel = dir * glidingSpeed * Time.deltaTime;


//            this.transform.position += vel;


//        }

//        //Debug.Log("Testing Sin : " + Mathf.Sin(SinCounter += Time.deltaTime));

//        this.transform.position += new Vector3(0, (Mathf.Sin(SinCounter += Time.deltaTime)) * 0.05f, 0);

//        return false;
//    }

//    public override void Dead()
//    {
//        base.Dead();
//    }

//    //public void GoBackPrevPos()
//    //{
//    //    if (this.transform.childCount < 1)
//    //        return;


//    //    if ((prevPosition - this.transform.position).magnitude > 2f) // if player got grabbed by tengu
//    //    {
//    //        //Debug.Log("Tengu grabbed player 2");
//    //        Vector3 dir = Vector3.zero;
//    //        Vector3 TargetPos = prevPosition; // for this one I choose to let the tengu go back to the position it was from, can still change

//    //        dir = (prevPosition - this.transform.position).normalized;

//    //        Vector3 vel = dir * 10 * Time.deltaTime;

//    //        this.transform.position += vel;
//    //    }

//    //    if ((prevPosition - this.transform.position).magnitude <= 2f)
//    //    {
//    //        //this.transform.position = prevPosition;
//    //        reachPrev = true;
//    //        GoBack = false;
//    //        //return;
//    //    }
//    //}

//    public override void FindNearestInList()
//    {
//        float nearest = float.MaxValue;
//        float temp_dist = 0f;
//        for (int i = 0; i < UnitsInRange.Count; ++i)
//        {
//            if (GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>().Gropper == UnitsInRange[i])
//                continue;

//            temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

//            if (Target)
//            {
//                //If the current target is not piority && the currently compared unit is piority
//                if (Target.tag != priority && (UnitsInRange[i].tag == priority))
//                {
//                    //Force Assign
//                    nearest = temp_dist;
//                    Target = UnitsInRange[i].transform;
//                    continue;
//                }
//                //If the current target is piority && the currently compared unit is not piority
//                else if (Target.tag == priority && (UnitsInRange[i].tag != priority))
//                {
//                    //Ignore and continue
//                    continue;
//                }
//            }

//            if (temp_dist < nearest)
//            {
//                nearest = temp_dist;
//                Target = UnitsInRange[i].transform;
//            }
//        }
//    }

//    Vector3 SetOriginalPosition()
//    {
//        Vector3 Temp = Vector3.zero;
//        //Temp = GameObject.Find("TenguEscapePoint").transform.position;
//        if (transform.parent)
//        {
//            if (transform.parent.GetComponent<AI_Movement>())
//            {
//                Temp.x = this.transform.parent.position.x;
//                Temp.y = transform.parent.position.y + this.GetAttackRangeStat();
//                Temp.z = this.transform.parent.position.z;
//                return Temp;
//            }
//        }
//        return Temp;
//    }

//    public override void AddState()//Put all the required states here
//    {
//        sm.AddState("attack", new Unit_Attack_State(this));
//        sm.AddState("chase", new Unit_Chase_State(this));
//        sm.AddState("chase_cart", new Unit_ChaseCart_State(this));
//        sm.AddState("dead", new Unit_Dead_State(this));
//        sm.AddState("stun", new Unit_Stun_State(this));
//        sm.AddState("grab", new Unit_TenguGrab_State(this));
//    }

//    public void Grabbing()
//    {
//        this.sm.ChangeState("grab");
//    }
//}