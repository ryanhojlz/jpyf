using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Tanuki : Entity_Unit
{
    Transform BulletShootFrom = null;
    public override void SelfStart()
    {
        AttackSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().TNG_attack;
        UnitThatProduceSound = this.GetComponent<AudioSource>();
        //UnitThatProduceSound.clip = AttackSound;

        BulletShootFrom = this.transform.Find("Bullet_Spawn_Location");
    }

    //public override void FindNearestInList()
    //{
    //    float nearest = float.MaxValue;
    //    float temp_dist = 0f;
    //    for (int i = 0; i < UnitsInRange.Count; ++i)
    //    {
    //        temp_dist = (UnitsInRange[i].transform.position - this.transform.position).magnitude;

    //        if (Target)
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

    public override void Attack()
    {
        
        if (!BulletShootFrom)
            return;

        Debug.Log("Got come here");

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
                        GameObject bulletGO = (GameObject)Instantiate(Projectile_Prefeb, BulletShootFrom.position, this.transform.rotation);
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
                            Projectile.SetDirection(BulletShootFrom.position + BulletShootFrom.forward, BulletShootFrom.position);
                        }
                        else
                        {
                            Projectile.SetDirection(Target.position, BulletShootFrom.position);
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
}
