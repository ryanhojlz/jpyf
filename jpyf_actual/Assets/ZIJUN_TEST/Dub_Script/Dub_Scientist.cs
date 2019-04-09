using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dub_Scientist : Healer_Unit
{
    public GameObject rangeProjectile;
    public GameObject specialProjectile;
    float immunityTimer = 30f;

    private void Start()
    {
        GetComponent<NavMeshAgent>().baseOffset = 5;
    }

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

        if (Input.GetKeyDown(KeyCode.M))
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
        GameObject bulletGO = (GameObject)Instantiate(specialProjectile, this.transform.position, this.transform.rotation);
        FullHealProjectile bullet = bulletGO.GetComponent<FullHealProjectile>();

        if (bullet != null)
        {
            bullet.Seek(target.transform);
            bullet.SetBase(this);
        }
    }

    public override void SpecialHealing()
    {
        Debug.Log("Special healing enter");
        //Heal the selected target to full health and let the full health persists for 30 seconds
        if (target == null)
            return;

        SpecialShoot();

        if (this.GetTarget().GetComponent<BasicGameOBJ>().healthValue == this.GetTarget().GetComponent<BasicGameOBJ>().startHealthvalue)
        {
            immunityTimer -= Time.deltaTime;
            this.GetTarget().GetComponent<BasicGameOBJ>().TakeDamage(0);
            if (immunityTimer < 0)
            {
                this.GetTarget().GetComponent<BasicGameOBJ>().TakeDamage(this.attackValue);
            }
        }
    }
}
