﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Nurikabe_Projectile : Entity_Projectile
{
    float damageMultiplier = 1.5f;//Percentage
    float stunDuration = 2f;//Seconds

    public override void HitCart(Collider other)
    {
        GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>().Cart_TakeDmg((int)(m_dmg * damageMultiplier));
        //Add stun Effects here (Once have) (Make speed 0)
    }

    public override void HitPlayer(Collider other)
    {
        other.GetComponent<PS4_PlayerHitboxScript>().TakeDamage((int)m_dmg);
        //Add stun Effects here (Once have) (Make speed 0)
    }
}