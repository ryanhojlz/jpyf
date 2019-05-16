using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurikabe : Entity_Unit
{
    GameObject bulletGO = null;

    public override void SelfStart()
    {
        AttackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().NRKB_attack;
        UnitThatProduceSound = this.GetComponent<AudioSource>();
        UnitThatProduceSound.clip = AttackSound;
    }

    public override void SelfUpdate()
    {
        if (GetStillAttacking())//if it is still attacking
            if (bulletGO == null)//Bullet no longer exist
                SetStillAttacking(false);//State it is no longer attacking


        if (GetisIdle())
        {
            m_animation.SetAnim(0);
        }
        else
        {
            m_animation.SetAnim(1);
        }
    }
    public override void Attack()
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
                bulletGO = (GameObject)Instantiate(Projectile_Prefeb, this.transform.position, this.transform.rotation);

                Entity_Projectile Projectile = bulletGO.GetComponent<Entity_Projectile>();

                if (AttackSound)
                {
                    //UnitThatProduceSound.clip = AttackSound;
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f)
                    {
                        UnitThatProduceSound.Play();

                    }
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
                Projectile.SetSpeed(6);
                //Projectile.SetSpeed(GetAttackRangeStat() / lifeTime);
                Projectile.SetLifeTime(lifeTime);
                Projectile.SetDamage(GetAttackStat());
                //SetStillAttacking(true);
                Projectile.SetProjectileTag(this.tag);

            }

            countdown = atkcooldown;
        }
    }

    //public override void FindNearestInList()
    //{
    //    float nearest = float.MaxValue;
    //    float temp_dist = 0f;
    //    for (int i = 0; i < UnitsInRange.Count; ++i)
    //    {
    //        temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

    //        //If the current target is not piority && the currently compared unit is piority
    //        if (Target)
    //        {
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
}
