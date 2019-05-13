using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackScript : MonoBehaviour
{
    public GameObject playerProjectile = null;
    public int Ammo = 0;
    public int AmmoCap = 0;

    private float fireRate = 0.5f;
    private float fireRateTimer = 0.5f;
    private bool canFire = true;
	// Use this for initialization
	void Start ()
    {
        Ammo = 10;
        AmmoCap = Ammo;
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
        else if (Ammo > AmmoCap)
        {
            Ammo = 10;
        }

        if (Ammo <= 0)
        {
            if (!transform.GetChild(0).gameObject.activeInHierarchy)
                transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            if (transform.GetChild(0).gameObject.activeInHierarchy)
                transform.GetChild(0).gameObject.SetActive(false);
        }

        if (!canFire)
        {
            fireRateTimer -= 1 * Time.deltaTime;
            if (fireRateTimer <= 0)
            {
                fireRateTimer = fireRate;
                canFire = true;
            }
        }
    }

    public void SpawnBullet()
    {
        if (Ammo <= 0)
            return;
        if (!canFire)
            return;

        --Ammo;
        GameObject p_projectile = Instantiate(playerProjectile,this.transform.position,this.transform.rotation) as GameObject;
        p_projectile.GetComponent<Entity_Player_Projectile>().SetDirection(this.transform.position + this.transform.forward, this.transform.position);
        canFire = false;
    }



}
