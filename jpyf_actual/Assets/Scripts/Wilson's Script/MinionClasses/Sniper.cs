using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Attack_Unit
{
    public GameObject bulletPrefab;
    //public AudioSource attackSound;

    public override void Attack()
    {
        //target = FindNearestUnit(transform.position);
        //base.Attack();
        if (target == null)
            return;

        //Debug.Log("Who is attacking? : " + this.name);

        if (CountDownTimer <= 0)
        {
           
            CountDownTimer = OriginalTimer;
            Shoot();
            //target.GetComponent<Minion>().TakeDamage(attackValue);
        }
        else
        {
            CountDownTimer -= Time.deltaTime;
        }
    }

    public override void Unit_Self_Update()
    {
        if (minionWithinRange.Count > 0)
        {
            this.stateMachine.ChangeState(new AttackState(this, minionWithinRange, Enemy_Tag));
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        RangeProjectile bullet = bulletGO.GetComponent<RangeProjectile>();
        //attackSound.Play();

        if (bullet != null)
        {
            //Debug.Log(CountDownTimer);
            bullet.Seek(target.transform);
            bullet.SetBase(this);
        }
    }
}
