using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMedic : Healer_Unit
{
    public GameObject[] healTargetList;
    // Start is called before the first frame update
    void Start()
    {

        healthValue = 100;
        startHealthvalue = healthValue;
        attackValue = 30;

        isActive = true;
    }

    //public override void Healing()
    //{
    //    if (target == null)
    //        return;

    //    //Debug.Log("Who is attacking? : " + this.name);
    //    if (CountDownTimer <= 0)
    //    {
    //        CountDownTimer = OriginalTimer;
    //        Shoot();
    //        //target.GetComponent<Minion>().TakeDamage(attackValue);
    //    }
    //    else
    //    {
    //        CountDownTimer -= Time.deltaTime;
    //    }
    //}

    //public override void FindAllyToHeal()
    //{
    //    //base.FindAllyToHeal();
    //    healTargetList = GameObject.FindGameObjectsWithTag("Ally_Unit");

    //    foreach (GameObject GO in healTargetList)
    //    {
    //        //how to search through the list and find the unit with the lowest hp?
    //        if (healthValue < 30)//only find units with health value less than 20?
    //        {
    //            Healing(20f);
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        //FindAllyToHeal();
    }

    void Shoot()
    {
        //GameObject bulletGO = (GameObject)Instantiate(rangeProjectile, this.transform.position, this.transform.rotation);
        //HealingProjectile bullet = bulletGO.GetComponent<HealingProjectile>();

        //if (bullet != null)
        //{
        //    bullet.Seek(target.transform);
        //    bullet.SetBase(this);
        //}
    }
}
