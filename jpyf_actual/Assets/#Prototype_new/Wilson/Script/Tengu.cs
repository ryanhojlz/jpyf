using System.Collections.Generic;
using UnityEngine;

public class Tengu : Entity_Unit
{
    private Vector3 prevPosition;
    private bool startAttack;

    public override void SelfStart()
    {
        startAttack = true;
        AttackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().TNG_attack;
        UnitThatProduceSound = this.GetComponent<AudioSource>();
        UnitThatProduceSound.clip = AttackSound;
    }

    public override void SelfUpdate()
    {
        //Debug.Log(priority);
        //Debug.Log("child count: " + this.transform.childCount);
        //Debug.Log("health: " + GetHealthStat());
        //Debug.Log(gameObject);
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetHealthStat(0);
        }

        if (GetHealthStat() == 0) // if tengu dies
        {
            this.GetTarget().parent = null; // Unparent
            this.GetTarget().GetComponent<Rigidbody>().useGravity = true; // make gravity true so that the target drop to the ground after tengu dies
            Debug.Log("child count: " + this.transform.childCount);
            //Dead(); 
            Destroy(gameObject); // destroy tengu gameobject
        }
    }

    public override void Attack()
    {
        if (startAttack)
        {
            prevPosition = this.gameObject.transform.position;
        }
        //this.gameObject.transform.position = new Vector3(Target.position.x, Target.position.y, Target.position.z);

        if ((this.GetTarget().position - this.transform.position).magnitude > 2 && this.transform.childCount == 0) // Distance less than 2 and tengu not grabbing the target
        {
            //Debug.Log("Tengu found player");
            // Go down to the target
            Vector3 dir = Vector3.zero; // get direction
            Vector3 TargetPos = this.GetTarget().position; // get the target position

            if (startAttack)
            {
                startAttack = false;
            }

            //TargetPos.y += this.GetTarget().lossyScale.y;

            dir = (this.GetTarget().position - this.transform.position).normalized; // direction equals to distance between the two normalised

            Vector3 vel = dir * 10 * Time.deltaTime; // velocity

            this.transform.position += vel; // Making the tengu move
        }
        else if (this.transform.childCount == 0) // tengu not grabbing the target
        {
            this.GetTarget().parent = this.transform; // parent the target to the tengu as the tengu grabs the target
            this.GetTarget().GetComponent<Rigidbody>().useGravity = false; // make gravity false so that the target won't drop to the ground
            Debug.Log(GetTarget().parent);
        }

        if (this.transform.childCount == 1) // if player got grabbed by tengu
        {
            //Debug.Log("Tengu grabbed player 2");
            Vector3 dir = Vector3.zero;
            Vector3 TargetPos = prevPosition; // for this one I choose to let the tengu go back to the position it was from, can still change

            dir = (prevPosition - this.transform.position).normalized;

            Vector3 vel = dir * 10 * Time.deltaTime;

            this.transform.position += vel;
        }
    }

    public override void FindNearestInList()
    {
        float nearest = float.MaxValue;
        float temp_dist = 0f;
        for (int i = 0; i < UnitsInRange.Count; ++i)
        {
            temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

            if (Target)
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
}