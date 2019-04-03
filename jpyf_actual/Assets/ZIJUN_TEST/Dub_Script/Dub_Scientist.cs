using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dub_Scientist : Healer_Unit
{
    public GameObject rangeProjectile;
    float immunityTimer = 30f;

    public override void Healing()
    {
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
            this.stateMachine.ChangeState(new HealState(this, minionWithinRange, Ally_Tag));
        }

       GetComponent<NavMeshAgent>().baseOffset = 5;

        if(Input.GetKeyDown(KeyCode.M))
        {
            SpecialHealing();
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(rangeProjectile, this.transform.position, this.transform.rotation);
        HealingProjectile bullet = bulletGO.GetComponent<HealingProjectile>();

        if (bullet != null)
        {
            bullet.Seek(target.transform);
            bullet.SetBase(this);
        }
    }

    void SpecialShoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(rangeProjectile, this.transform.position, this.transform.rotation);
        FullHealProjectile bullet = bulletGO.GetComponent<FullHealProjectile>();

        if (bullet != null)
        {
            bullet.Seek(target.transform);
            bullet.SetBase(this);
        }
    }

    public override void SpecialHealing()
    {
        //Heal the selected target to full health and let the full health persists for 30 seconds
        if (CountDownTimer <= 0)
        {
            CountDownTimer = OriginalTimer;
            SpecialShoot();
        }
        else
        {
            CountDownTimer -= Time.deltaTime;
        }

        if(healthValue == startHealthvalue)
        {
            immunityTimer -= Time.deltaTime;

        }

    }
}
