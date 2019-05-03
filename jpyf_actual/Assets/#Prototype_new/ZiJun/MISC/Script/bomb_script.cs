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

    Vector3 Expending_Scale = Vector3.zero;
    public float expending_speed = 5f;

    public bool debugging = false;

    public float explosion_Range = 10f;

    // Use this for initialization
    void Start ()
    {
        //State = Bomb_state.DORMANT;
        State = Bomb_state.DORMANT;

        Expending_Scale.Set(1, 1, 1);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (debugging)
        {
            if (this.transform.localScale.x > 10)
            {
                Debug.Log("HIIIIII loooo");
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
                    this.GetComponent<Rigidbody>().isKinematic = false;
                    this.GetComponent<Rigidbody>().useGravity = true;
                }
                break;

            case Bomb_state.EXPLOSION:
                {
                    Debug.Log("Came here");
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
            if (unit)
            {
                if (unit.GetHealthStat() > 0)
                {
                    unit.TakeDamage(unit.GetMaxHealthStat());
                }
            }
        }
    }

    public void SetBombState_Active()
    {
        State = Bomb_state.ACTIVE;
    }
}
