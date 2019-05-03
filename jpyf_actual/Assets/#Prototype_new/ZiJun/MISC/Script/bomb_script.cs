using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_script : MonoBehaviour
{
    enum Bomb_state
    {
        DORMANT,
        ACTIVE,
        EXPLOSION
    }

    Bomb_state State;

    public Collider Collider = null;

    Vector3 Expending_Scale = Vector3.zero;
    public float expending_speed = 5f;

    public bool debugging = false;

    public float explosion_Range = 10f;

    GameObject Fire = null;

    // Use this for initialization
    void Start ()
    {
        //State = Bomb_state.DORMANT;
        State = Bomb_state.DORMANT;

        Expending_Scale.Set(1, 1, 1);

        if (this.transform.childCount > 0)
        {
            Fire = this.transform.GetChild(0).GetChild(1).gameObject;

            Debug.Log(Fire);

            if (Fire)
            {
                Fire.SetActive(false);
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (debugging)
        {
            if (this.transform.localScale.x > 10)
            {
                //Debug.Log("HIIIIII loooo");
                this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (this.transform.localScale.x > 10)
            {
                Destroy(this.gameObject);
            }
        }


        switch (State)
        {
            case Bomb_state.DORMANT:
                {

                }
                break;

            case Bomb_state.ACTIVE:
                {
                    if (Fire)
                    {
                        Fire.SetActive(true);
                    }
                    //this.transform.GetChild(1).gameObject.SetActive(true);
                    this.GetComponent<Rigidbody>().isKinematic = false;
                    this.GetComponent<Rigidbody>().useGravity = true;
                }
                break;

            case Bomb_state.EXPLOSION:
                {
                    if (Collider && Collider.enabled)
                    {
                        Collider.enabled = false;
                    }
                    //Debug.Log("Came here");
                    this.GetComponent<MeshRenderer>().enabled = true;
                    this.GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.localScale += Expending_Scale * expending_speed * Time.deltaTime;
                }
                break;

        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (State == Bomb_state.ACTIVE)
        {
            State = Bomb_state.EXPLOSION;

            if(this.transform.childCount > 0)
            {
                Destroy(this.transform.GetChild(0).gameObject);
                Destroy(this.transform.GetChild(1).gameObject);
            }
        }
    }

    //private void OnTriggerStay(Collision collision)
    //{
    //    if (State == Bomb_state.EXPLOSION)
    //    {
    //        Entity_Unit unit = collision.gameObject.GetComponent<Entity_Unit>();
    //        if (unit)
    //        {
    //            unit.TakeDamage(unit.GetMaxHealthStat());
    //        }
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (State == Bomb_state.EXPLOSION)
        {
            Entity_Unit unit = other.GetComponent<Entity_Unit>();
            Wall_Script wall = other.GetComponent<Wall_Script>();
            if (unit)
            {
                if (unit.GetHealthStat() > 0)
                {
                    unit.TakeDamage(unit.GetMaxHealthStat());
                }
            }
            else if (wall)
            {
                if (wall.GetHealth() > 0)
                {
                    wall.TakeDamage((int)wall.GetMaxHealth());
                }
            }
        }
    }

    public void SetBombState_Active()
    {
        State = Bomb_state.ACTIVE;
    }

    public bool IsExplosion()
    {
        if (State == Bomb_state.EXPLOSION)
        {
            return true;
        }

        return false;
    }
}
