using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : Attack_Unit
{
    public GameObject meleeProjectile;
    public AudioSource attackSound;

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
        if (GetComponent<BasicGameOBJ>().isPossessed)
            return;
        if (minionWithinRange.Count > 0)
        {
            this.stateMachine.ChangeState(new AttackState(this, minionWithinRange, Enemy_Tag));
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(meleeProjectile, this.transform.position, this.transform.rotation);
        MeleeProjectile bullet = bulletGO.GetComponent<MeleeProjectile>();
        //attackSound.Play();

        if (bullet != null)
        {
            bullet.Seek(target.transform);
            bullet.SetBase(this);
        }
    }

    public override void SpecialAttack()
    {
        Debug.Log("SPECIAL ATTACK");
        //this.gameObject.GetComponent<Rigidbody>().AddForce(0, 20, 0);

        for (int i = 0; i < GetComponent<BasicGameOBJ>().minionWithinRange.Count; i++)
        {
            //GetComponent<BasicGameOBJ>().minionWithinRange[i].SetActive(false);
            Vector3 direction = Vector3.zero;
            direction = GetComponent<BasicGameOBJ>().minionWithinRange[i].gameObject.transform.position - this.gameObject.transform.position;
            GetComponent<BasicGameOBJ>().minionWithinRange[i].GetComponent<Rigidbody>().AddForce(direction * 100);
        }
        
    }


    public override void PlayerAutoAttack()
    {
        if (GetComponent<BasicGameOBJ>().minionWithinRange.Count <= 0)
            return;

        float distance = float.MaxValue;
        GameObject closestGO = null;
        for (int i = 0; i < GetComponent<BasicGameOBJ>().minionWithinRange.Count; i++)
        {
           float target_distance = Vector3.SqrMagnitude(GetComponent<BasicGameOBJ>().minionWithinRange[i].transform.position - this.gameObject.transform.position);
            if (distance > target_distance)
            {
                target_distance = distance;
                closestGO = GetComponent<BasicGameOBJ>().minionWithinRange[i];
            }
        }
        target = closestGO;
        GameObject bulletGO = (GameObject)Instantiate(meleeProjectile, this.transform.position, this.transform.rotation);
        MeleeProjectile bullet = bulletGO.GetComponent<MeleeProjectile>();
        if (bullet != null)
        {
            bullet.Seek(target.transform);
            bullet.SetBase(this);
        }
    }
}
