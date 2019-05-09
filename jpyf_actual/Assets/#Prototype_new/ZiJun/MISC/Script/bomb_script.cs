using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_script : MonoBehaviour
{

    //Decide on how to despawn in the future

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

    public Vector3 dir = new Vector3();
    public float speed = 10f;

    bool save = false;
    public bool canMove = true;
    bool spawnFromSpawner = false;

    Transform Explosion = null;

    //float distanceToDespawn = 100f;

    // Use this for initialization
    void Start ()
    {
        Explosion = this.transform.Find("Bomb_Explosion");
        //State = Bomb_state.DORMANT;
        State = Bomb_state.DORMANT;

        Expending_Scale.Set(1, 1, 1);

        if (this.transform.childCount > 0)
        {
            Fire = this.transform.GetChild(0).GetChild(1).gameObject;

            //Debug.Log(Fire);

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
            if (Explosion.localScale.x > 10)
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

                    canMove = false;

                    //Debug.Log("Came here");
                    this.GetComponent<Rigidbody>().isKinematic = true;
                    Explosion.localScale += Expending_Scale * expending_speed * Time.deltaTime;
                }
                break;

        }

        Movement(dir, speed, canMove);

        //this.transform.position += new Vector3(0, 0, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (State == Bomb_state.ACTIVE)
        {
            State = Bomb_state.EXPLOSION;

            //if(this.transform.childCount > 0)
            //{
            //    //Destroy(transform.Get);
            //    //Destroy(this.transform.GetChild(1).gameObject);

            //}

            foreach (Transform child in this.transform)
            {
                if (child.name == "Bomb_Explosion")
                    continue;

                Destroy(child.gameObject);
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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
            return;

        if (State == Bomb_state.EXPLOSION)
        {
            if (other.GetComponent<Force_Field>() && other.GetComponent<Force_Field>().GetIsActive())
            {
                other.GetComponent<Force_Field>().SetForceField(false);
                return;
            }

            Entity_Unit unit = other.GetComponent<Entity_Unit>();
            Wall_Script wall = other.GetComponent<Wall_Script>();
            if (unit)
            {
                if (unit.GetHealthStat() > 0)
                {
                    //unit.TakeDamage(unit.GetMaxHealthStat());
                    unit.ChangeState("stun");
                }
            }
            else if (wall)
            {
                if (wall.GetHealth() > 0)
                {
                    wall.TakeDamage((int)wall.GetMaxHealth());
                }
            }
            //If it hits payload
            else if (other.tag == "Payload" && !save)
            {
                Stats_ResourceScript.Instance.Cart_TakeDmg(2);
            }
        }
        else if (other.tag == "Payload" || other.tag == "BombStopper")
        {
            State = Bomb_state.ACTIVE;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (State == Bomb_state.EXPLOSION)
    //    {
    //        Entity_Unit unit = other.GetComponent<Entity_Unit>();
    //        Wall_Script wall = other.GetComponent<Wall_Script>();
    //        if (unit)
    //        {
    //            if (unit.GetHealthStat() > 0)
    //            {
    //                unit.TakeDamage(unit.GetMaxHealthStat());
    //            }
    //        }
    //        else if (wall)
    //        {
    //            if (wall.GetHealth() > 0)
    //            {
    //                wall.TakeDamage((int)wall.GetMaxHealth());
    //            }
    //        }
    //        //If it hits payload
    //        else if (other.tag == "Payload" && !save)
    //        {
    //            Stats_ResourceScript.Instance.Cart_TakeDmg(2);
    //        }
    //    }
    //    else if (other.tag == "Payload" || other.tag == "BombStopper")
    //    {
    //        State = Bomb_state.ACTIVE;
    //    }
    //}

    public void SetBombState_Active()
    {
        State = Bomb_state.ACTIVE;
        save = true;
        canMove = false;
    }

    public void SetPickUp()
    {
        canMove = false;
    }

    public bool IsExplosion()
    {
        if (State == Bomb_state.EXPLOSION)
        {
            return true;
        }

        return false;
    }

    public void Movement(Vector3 Direction, float speed, bool canMove)
    {
        //Debug.Log(Direction * speed * Time.deltaTime);
        //Debug.Log(canMove);
        if(canMove)
            this.transform.localPosition = this.transform.localPosition + (Direction * speed * Time.deltaTime);

        //this.transform.position += new Vector3(0, 0, 1);
    }

    public void SpawnerIniting()
    {
        spawnFromSpawner = true;
    }
}
