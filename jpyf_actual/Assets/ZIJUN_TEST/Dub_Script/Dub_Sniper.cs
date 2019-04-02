using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dub_Sniper : Attack_Unit
{
    public GameObject bulletPrefab;
    public GameObject laserPrefeb;
    Dub_Lazer_Projectile laser;
    bool isLazer;
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

        if (Input.GetKeyDown(KeyCode.M) || isLazer)
        {
            //ShootFront();
            isLazer = true;
            SpecialAttack();
        }
    }

    public override void SpecialAttack()
    {
        //Debug.Log("SPECIAL ATTACK");
        //this.gameObject.GetComponent<Rigidbody>().AddForce(0, 20, 0);

        //for (int i = 0; i < GetComponent<BasicGameOBJ>().minionWithinRange.Count; i++)
        //{
        //    //GetComponent<BasicGameOBJ>().minionWithinRange[i].SetActive(false);
        //    Vector3 direction = Vector3.zero;
        //    direction = GetComponent<BasicGameOBJ>().minionWithinRange[i].gameObject.transform.position - this.gameObject.transform.position;
        //    GetComponent<BasicGameOBJ>().minionWithinRange[i].GetComponent<Rigidbody>().AddForce(direction * 100);
        //}

        //HYPERBEAM!!!

        //for (int i = 0; i < minionWithinRange.Count; i++)
        //{
        //    if (!minionWithinRange[i].GetComponent<Minion>())//If is not a minion type, Proceed to the next one
        //        continue;
        //    if (minionWithinRange[i].tag == this.tag)//If is an ally, proceeds to the next one
        //        continue;

        //    minionWithinRange[i].GetComponent<BasicGameOBJ>().SetTarget(this.gameObject);//Get aggroed switching target to caster
        //    minionWithinRange[i].GetComponent<BasicGameOBJ>().TakeDamage(this.attackValue);//Dealing damage

        //}
        if (!laser)
        {
            
            GameObject bulletGO = (GameObject)Instantiate(laserPrefeb) as GameObject;
            laser = bulletGO.GetComponent<Dub_Lazer_Projectile>();
        }
        else
        {
            if (!laser.gameObject.activeSelf)
            {
                laser.gameObject.SetActive(true);
            }
            laser.SeekUser(this.transform);//Follow the user
        }

    }

    public void SetLaser(bool toLaser)
    {
        isLazer = toLaser;
        if (laser && !isLazer)
        {
            Destroy(laser);
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        CProjectile bullet = bulletGO.GetComponent<CProjectile>();
        //attackSound.Play();

        //Debug.Log("Sniper_Shoots");

        if (bullet != null)
        {
            //Debug.Log(CountDownTimer);
            bullet.Seek(target.transform);
            bullet.SetBase(this);
        }
    }
}
