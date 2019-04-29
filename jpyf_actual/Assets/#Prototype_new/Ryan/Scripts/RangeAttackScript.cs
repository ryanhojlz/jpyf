using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackScript : MonoBehaviour
{
    public GameObject playerProjectile = null;
    public int Ammo = 0;
	// Use this for initialization
	void Start ()
    {
        Ammo = 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    if (Ammo > 0)
        //        SpawnBullet();
        //}

        if (Ammo < 0)
        {
            Ammo = 0;
        }
        else if (Ammo > 3)
        {
            Ammo = 3;
        }
    }

    public void SpawnBullet()
    {
        if (Ammo <= 0)
            return;
        --Ammo;
        GameObject p_projectile = Instantiate(playerProjectile,this.transform.position,this.transform.rotation) as GameObject;
        p_projectile.GetComponent<Entity_Player_Projectile>().SetDirection(this.transform.position + this.transform.forward, this.transform.position);
    }



}
