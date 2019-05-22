using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Tanuki : Entity_Unit
{
    Transform BulletShootFrom = null;
    public override void SelfStart()
    {
        AudioManager AudioInstance = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AttackSound = AudioInstance.TNK_attack;
        TakeDamageSound = AudioInstance.TNK_TakeDamage;
        DieSound = AudioInstance.TNK_Die;
        SpawnSound = AudioInstance.TNK_spawn;
        UnitThatProduceSound = this.GetComponent<AudioSource>();
        //UnitThatProduceSound.clip = AttackSound;

        BulletShootFrom = this.transform.Find("Bullet_Spawn_Location");
        UnitThatProduceSound.clip = SpawnSound;
        UnitThatProduceSound.Play();
    }

    public override void SelfUpdate()
    {
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
        
        if (!BulletShootFrom)
            return;

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
