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

    [SerializeField]
    GameObject ExplosionParticles = null;

    [SerializeField]
    GameObject ExplosionParticles_2 = null;

    public Collider Collider = null;

    Vector3 Expending_Scale = Vector3.zero;

    public float expending_speed = 5f;
    float audioLength = 0f;

    public bool debugging = false;

    public float explosion_Range = 100f;

    GameObject Fire = null;

    public Vector3 dir = new Vector3();
    public float speed = 10f;

    bool save = false;
    public bool canMove = true;
    bool spawnFromSpawner = false;

    Transform Explosion = null;

    Rigidbody m_rb = null;

    private AudioClip explodingSound;
    private AudioSource playExplodingSound;
    //float distanceToDespawn = 100f;

    // Use this for initialization
    void Start ()
    {
        Explosion = this.transform.Find("Bomb_Explosion");
        //State = Bomb_state.DORMANT;
        State = Bomb_state.DORMANT;

        Expending_Scale.Set(1, 1, 1);

        //if (this.transform.childCount > 0)
        //{
        //    Fire = this.transform.GetChild(0).GetChild(1).gameObject;

        //    //Debug.Log(Fire);

        //    if (Fire)
        //    {
        //        Fire.SetActive(false);
        //    }
        //}
        m_rb = this.GetComponent<Rigidbody>();
        explodingSound = GameObject.Find("AudioManager").GetComponent<AudioManager>().BombSound;
        playExplodingSound = this.GetComponent<AudioSource>();

        audioLength = explodingSound.length;
        //expending_speed = explosion_Range / audioLength * 2;

        //Debug.Log("Bomb : " + explodingSound);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (debugging)
        {
            if (this.transform.localScale.x > explosion_Range)
            {
                //Debug.Log("HIIIIII loooo");
                //this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (Explosion.localScale.x > explosion_Range && !playExplodingSound.isPlaying)
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
                    m_rb.isKinematic = false;
                    m_rb.useGravity = true;
                }
                break;

            case Bomb_state.EXPLOSION:
                {
                    
                    if (Collider && Collider.enabled)
                    {
                        Collider.enabled = false;
                    }

                    canMove = false;

                    if (ExplosionParticles && ExplosionParticles_2)
                    {
                        ExplosionParticles.SetActive(true);
                        ExplosionParticles_2.SetActive(true);
                    }
                    //Debug.Log("Came here");
                    m_rb.isKinematic = true;
                    if (Explosion.localScale.x <= explosion_Range)
                    {
                        Explosion.localScale += Expending_Scale * expending_speed * Time.deltaTime;
                    }

                    Debug.Log(Explosion.localScale.x + " : " + explosion_Range);
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

            playExplodingSound.clip = explodingSound;

            playExplodingSound.Play();

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
                    if(!unit.GetComponent<Entity_Tengu>())
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
