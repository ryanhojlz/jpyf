using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackScript : MonoBehaviour
{
    public Transform playerProjectile = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpawnBullet()
    {
        Entity_Projectile p_projectile = Instantiate(playerProjectile).GetComponent<Entity_Projectile>();
        p_projectile.SetDirection(this.transform.position + this.transform.forward, this.transform.position);
        p_projectile.SetDamage(100);
    }



}
