using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BasicGameOBJ : MonoBehaviour
{
    public enum OBJType
    {
        BUILDING,
        MINION
    }

    public OBJType OBJ_TYPE;

    //base attributes of the enemies
    public float startHealthvalue;
    public float healthValue;
    public float attackValue;
    private float originalAtkValue = 0;
    public float defenceValue;
    public float attackSpeedValue;
    public float moveSpeedValue;
    public float rangeValue;
    public float possesionTier = 0;
    
    public bool isActive;
    public GameObject[] targetList; //enemy or ally also can
    public Image healthBar;


    public List<GameObject> minionWithinRange;//Keep track of which unit is within range

    public SphereCollider TriggerRange;
    //public CylinderCollider TriggerRange;

    protected float CountDownTimer;
    protected float OriginalTimer;
    public GameObject target;

    protected StateMachine stateMachine = new StateMachine();
    //public StateMachine stateMachine = new StateMachine();

    public GameObject ParticleExplosion;

    public string Enemy_Tag;
    public string Ally_Tag;

    public bool isPossessed = false;

    
    // Start is called before the first frame update
    void Awake()
    {
        originalAtkValue = attackValue;
        if (this.tag == "Ally_Unit")
        {
            Enemy_Tag = "Enemy_Unit";
            Ally_Tag = "Ally_Unit";
            //Physics.IgnoreLayerCollision(0, 10);
        }
        else if (this.tag == "Enemy_Unit")
        {
            Enemy_Tag = "Ally_Unit";
            Ally_Tag = "Enemy_Unit";
            //Physics.IgnoreLayerCollision(0, 10);
        }

        isActive = true;
        startHealthvalue = healthValue;
        //TriggerRange.radius = rangeValue;

        if (attackSpeedValue > 0)
        {
            CountDownTimer = 1 / attackSpeedValue;
            //Debug.Log("CountDownTimer : " + CountDownTimer);
        }
        else
        {
            //If is less then 0, it should never attack
            CountDownTimer = float.MaxValue;
        }

        OriginalTimer = CountDownTimer;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateHealth();
        UpdateCheckList();//Checking for unit in the list. If it is not active, remove it
        CheckTargetActive();//Check if your target is active. If not active target becomes null

        stateMachine.ExecuteStateUpdate();
        ClassUpdate();

        if (healthValue <= 0)
        {
            target = null;
            this.stateMachine.ChangeState(new DeadState(this));//state machine
        }

    }
    //Taking Damage
    public void TakeDamage(float dmgAmount)
    {

        if (this.gameObject.GetComponent<Immunity>())
            return;

        dmgAmount -= defenceValue;
        if (dmgAmount < 1)
        {
            dmgAmount = 1;
        }

        healthValue -= dmgAmount;

        //Have to multiply by defence value to reduce the damage taken
        if (healthValue <= 0)
        {
            SetIsActive(false);
        }
    }
    public void Die()
    {
        //Can add timer and other stuff
        if (isActive == false)
        {
            //Debug.Log("Destroying");
            //Destroy(gameObject);
            GameObject thisnew = Instantiate(ParticleExplosion) as GameObject;
            thisnew.transform.position = this.transform.position;
            this.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    //Updates
    protected virtual void ClassUpdate()
    {

    }



    protected void UpdateCheckList()
    {
        for (int i = 0; i < minionWithinRange.Count; ++i)
        {
            if (!minionWithinRange[i])//If is null remove
            {

                if (target == minionWithinRange[i])
                {
                    //Debug.Log("Removed");
                    target = null;
                }

                RemoveUnitFromList(minionWithinRange[i]);
                //RemoveUnitFromList(minionWithinRange[i].GetComponent<Minion>());
                continue;
            }

            if (!minionWithinRange[i].GetComponent<BasicGameOBJ>().GetIsActive())//If it is not active remove
            {
                if (target == minionWithinRange[i])
                {
                    target = null;
                }

                RemoveUnitFromList(minionWithinRange[i]);
                continue;
            }

            // if object is false
            if (minionWithinRange[i].activeSelf == false)
            {
                if (target == minionWithinRange[i])
                {
                    target = null;
                }
                minionWithinRange.Remove(minionWithinRange[i]);
            }

        }
        //Debug.Log(this.name + " : " + minionWithinRange.Count);
    }
    protected void UpdateHealth()
    {
        healthBar.fillAmount = healthValue / startHealthvalue;
        //Debug.Log("Update Health : " + healthValue + " / " + startHealthvalue);
    }

    //Checks
    protected void CheckTargetActive()
    {
        if (!target)
            return;

        if (!target.GetComponent<BasicGameOBJ>().GetIsActive())
        {
            target = null;
        }
    }
    public void RemoveUnitFromList(GameObject unit)
    {
        minionWithinRange.Remove(unit);
    }
    public bool CheckWithinRange(Transform unit)
    {
        if (this.gameObject == unit)//Unlikely happen but just in-case it detect itself in list somehow
            return false;

        Ray ThisToGround = new Ray(this.gameObject.transform.position, Vector3.down);
        Ray TargetToGround = new Ray(unit.position, Vector3.down);

        RaycastHit ThisPos;
        RaycastHit TargetPos;

        Physics.Raycast(ThisToGround, out ThisPos);
        Physics.Raycast(TargetToGround, out TargetPos);

        if ((ThisPos.point - TargetPos.point).magnitude <= rangeValue)
        {
            return true;
        }

        return false;
    }
    public float CheckDist(Transform unit)//Does not matter which is first
    {
        Vector3 pos1 = this.gameObject.transform.position;
        Vector3 pos2 = unit.transform.position;

        Ray ThisToGround = new Ray(pos1, Vector3.down);
        Ray TargetToGround = new Ray(pos2, Vector3.down);

        RaycastHit ThisPos;
        RaycastHit TargetPos;

        Physics.Raycast(ThisToGround, out ThisPos);
        Physics.Raycast(TargetToGround, out TargetPos);

        return (ThisPos.point - TargetPos.point).magnitude;
    }

    //Get & Setter
    public bool GetIsActive()
    {
        return isActive;
    }
    public void SetIsActive(bool toggle)
    {
        isActive = toggle;
    }
    public float GetCountDownTimer()
    {
        return CountDownTimer;
    }
    public void SetTarget(GameObject _target)
    {
        target = _target;
    }
    public GameObject GetTarget()
    {
        return target;
    }

    //Trigger Colliders
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>())
            return;

        if (other.gameObject.GetComponent<SphereCollider>() && this.gameObject.GetComponent<SphereCollider>())
            Physics.IgnoreCollision(other.gameObject.GetComponent<SphereCollider>(), this.gameObject.GetComponent<SphereCollider>());

        ////For Making sure if the object is already added, don't add again
        //for (int i = 0; i < minionWithinRange.Count; ++i)
        //{
        //    if (other.gameObject == minionWithinRange[i])
        //    {
        //        return;
        //    }
        //}

        if (other.tag == "Ally_Unit" || other.tag == "Enemy_Unit")
        {
            minionWithinRange.Add(other.gameObject);
        }
    }
    //void OnTriggerStay(Collider other)
    //{
    //    if (!other.gameObject.GetComponent<Rigidbody>())
    //        return;

    //    if (other.gameObject.GetComponent<SphereCollider>() && this.gameObject.GetComponent<SphereCollider>())
    //        Physics.IgnoreCollision(other.gameObject.GetComponent<SphereCollider>(), this.gameObject.GetComponent<SphereCollider>());

    //    //For Making sure if the object is already added, don't add again
    //    for (int i = 0; i < minionWithinRange.Count; ++i)
    //    {
    //        if (other.gameObject == minionWithinRange[i])
    //        {
    //            return;
    //        }
    //    }

    //    if (other.tag == "Ally_Unit" || other.tag == "Enemy_Unit")
    //    {
    //        minionWithinRange.Add(other.gameObject);
    //    }
    //}
    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.GetComponent<CHyperBeam>())
        //{
        //    Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        //}
    }
    private void OnCollisionStay(Collision other)
    {
        //if (other.gameObject.GetComponent<CHyperBeam>())
        //{
        //    Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        //}
    }
    void OnTriggerExit(Collider other)
    {
        //for (int i = 0; i < minionWithinRange.Count; ++i)
        //{
        //    if (minionWithinRange[i] == other.gameObject)
                minionWithinRange.Remove(other.gameObject);
        //}
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeValue);
    }

    public void SetStateMachine(IState state)
    {
        this.stateMachine.ChangeState(state);
    }


}
