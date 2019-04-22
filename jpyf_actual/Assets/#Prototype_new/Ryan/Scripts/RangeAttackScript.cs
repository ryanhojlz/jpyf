using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackScript : MonoBehaviour
{
    public GameObject playerProjectile = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        Debug.Log("Beep");
        GameObject p_projectile = Instantiate(playerProjectile,this.transform.position,this.transform.rotation) as GameObject;
        p_projectile.GetComponent<Entity_Player_Projectile>().SetDirection(this.transform.position + this.transform.forward, this.transform.position);
      
    }



}
