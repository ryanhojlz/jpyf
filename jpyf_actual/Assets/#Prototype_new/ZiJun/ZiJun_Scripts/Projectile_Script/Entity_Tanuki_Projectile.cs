using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Tanuki_Projectile : Entity_Projectile
{
    float damageReduction = 0.5f;//Percentage
    float slowDuration = 5f;//Seconds
    float slowAmount = 0.2f;//Percentage slow

    public override void HitCart(Collider other)
    {
        GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>().Cart_TakeDmg((int)(m_dmg * damageReduction));
    }

    public override void HitPlayer(Collider other)
    {
        //other.GetComponent<PS4_PlayerHitboxScript>().TakeDamage((int)0);
        //Add Slow Effects here (Once have)
        GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>().SetDebuff(slowAmount, slowDuration);
    }
}
