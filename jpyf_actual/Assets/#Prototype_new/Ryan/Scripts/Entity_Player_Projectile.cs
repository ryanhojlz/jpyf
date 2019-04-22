using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Player_Projectile : Entity_Projectile
{
    float damageMultiplier = 1.5f;//Percentage
    float stunDuration = 2f;//Seconds

    public override void HitCart(Collider other)
    {

    }

    public override void HitPlayer(Collider other)
    {

    }
}
