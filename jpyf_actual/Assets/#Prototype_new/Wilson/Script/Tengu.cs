using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tengu : Entity_Unit
{

    public override void SelfStart()
    {
        AttackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().NRKB_attack;
        UnitThatProduceSound = this.GetComponent<AudioSource>();
        UnitThatProduceSound.clip = AttackSound;
    }

    public override void Attack()
    {


        //this.gameObject.transform.position = new Vector3(Target.position.x, Target.position.y, Target.position.z);
        //if (Target.position.x > this.gameObject.transform.position.x)//target is on GO's right
        //{
        //    this.gameObject.transform.position += new Vector3(speed, 0, 0);
        //}
        //if (Target.position.x < this.gameObject.transform.position.x)//target is on GO's left
        //{
        //    this.gameObject.transform.position -= new Vector3(speed, 0, 0);
        //}
        //if (Target.position.y > this.gameObject.transform.position.y)//target is on GO's right
        //{
        //    this.gameObject.transform.position += new Vector3(0, speed, 0);
        //}
        //if (Target.position.y < this.gameObject.transform.position.y)//target is on GO's left
        //{
        //    this.gameObject.transform.position -= new Vector3(0, speed, 0);
        //}
        //if (Target.position.z > this.gameObject.transform.position.z)//target is on GO's right
        //{
        //    this.gameObject.transform.position += new Vector3(0, 0, speed);
        //}
        //if (Target.position.z < this.gameObject.transform.position.z)//target is on GO's left
        //{
        //    this.gameObject.transform.position -= new Vector3(0, 0, speed);
        //}

        if ((this.GetTarget().position - this.transform.position).magnitude > 2 && this.transform.childCount == 0)
        {

            Vector3 dir = Vector3.zero;

            Vector3 TargetPos = this.GetTarget().position;

            TargetPos.y += this.GetTarget().lossyScale.y;

            dir = (this.GetTarget().position - this.transform.position).normalized;

            Vector3 vel = dir * 10 * Time.deltaTime;

            this.transform.position += vel;
        }
        else if (this.transform.childCount == 0)
        {
            this.GetTarget().parent = this.transform;
            this.GetTarget().GetComponent<Rigidbody>().useGravity = false;
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
