using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProjectile : MonoBehaviour {

    private Vector3 TargetPosition;
    private Vector3 UnitThatShootsPosition;
    private Transform UnitThatShoots;

    float entered = 0;

    public enum PT
    {
        MELEE,
        RANGE,
        //HEALING
    }

    public PT projectile_type;

    public float AnimationSpeed;
    float speed = 0f;
    float lifeTime = 0f;
    public bool moving = false;

    bool isHit = false;

    public GameObject prefabtext;

    // Use this for initialization
    void Start ()
    {
        //Debug.Log(this);
       
        this.transform.position = new Vector3(
            this.transform.position.x + (UnitThatShoots.transform.forward.x * (UnitThatShoots.transform.localScale.x + this.transform.localScale.x)) * (this.GetComponent<SphereCollider>().radius),
            this.transform.position.y + (UnitThatShoots.transform.forward.y * (UnitThatShoots.transform.localScale.y + this.transform.localScale.y)) * (this.GetComponent<SphereCollider>().radius),
            this.transform.position.z + (UnitThatShoots.transform.forward.z * (UnitThatShoots.transform.localScale.z + this.transform.localScale.z)) * (this.GetComponent<SphereCollider>().radius)
            );

        //for (int i = 0; i < UnitThatShoots.GetComponent<Minion>().minionWithinRange.Count; ++i)
        //{
        //    Physics.IgnoreCollision(UnitThatShoots.GetComponent<Minion>().minionWithinRange[i].GetComponent<SphereCollider>(), this.GetComponent<Collider>());
        //}
        
        GameObject[] newObject = GameObject.FindGameObjectsWithTag(UnitThatShoots.GetComponent<BasicGameOBJ>().Enemy_Tag);
        GameObject[] newObject_2 = GameObject.FindGameObjectsWithTag(UnitThatShoots.GetComponent<BasicGameOBJ>().Ally_Tag);
        //for(int i = 0)

        foreach (GameObject GO in newObject)
        {
            if (!GO.GetComponent<BasicGameOBJ>())
                continue;
            if (GO.GetComponent<BasicGameOBJ>().GetComponent<SphereCollider>())
                Physics.IgnoreCollision(GO.GetComponent<SphereCollider>(), this.GetComponent<Collider>());
        }

        foreach (GameObject GO in newObject_2)
        {
            if (!GO.GetComponent<BasicGameOBJ>())
                continue;
            if (GO.GetComponent<BasicGameOBJ>().GetComponent<SphereCollider>())
                Physics.IgnoreCollision(GO.GetComponent<SphereCollider>(), this.GetComponent<Collider>());
        }

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (!UnitThatShoots)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    if (projectile_type == PT.MELEE)
    //    {
    //        float dist_between = (UnitThatShootsPosition - TargetPosition).magnitude;
    //        speed = (dist_between / AnimationSpeed);
    //    }
    //    else if (projectile_type == PT.RANGE)
    //    {
    //        speed = (UnitThatShoots.GetComponent<BasicGameOBJ>().rangeValue / AnimationSpeed);
    //        //Debug.Log("cAME HERE");
    //    }
    //    //else if (projectile_type == PT.HEALING)
    //    //{
    //    //    speed = 1;// (UnitThatShoots.GetComponent<BasicGameOBJ>().rangeValue / AnimationSpeed);
    //    //}
    //    Vector3 dir = TargetPosition - transform.position;
    //    float distanceThisFrame = speed * Time.deltaTime;


    //    lifeTime -= Time.deltaTime;

    //    this.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * speed;

    //    if (lifeTime < 0f)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    void Update()
    {
        if (!UnitThatShoots)
        {
            Destroy(gameObject);
            return;
        }

        if (projectile_type == PT.MELEE)
        {
            float dist_between = (UnitThatShootsPosition - TargetPosition).magnitude;
            speed = (dist_between / AnimationSpeed);
        }
        else if (projectile_type == PT.RANGE)
        {
            speed = (UnitThatShoots.GetComponent<BasicGameOBJ>().rangeValue / AnimationSpeed);
            //Debug.Log("cAME HERE");
        }
        //else if (projectile_type == PT.HEALING)
        //{
        //    speed = 1;// (UnitThatShoots.GetComponent<BasicGameOBJ>().rangeValue / AnimationSpeed);
        //}
        Vector3 dir = TargetPosition - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;


        lifeTime -= Time.deltaTime;

        //this.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * speed;

        transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

        if (lifeTime < 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Seek(Transform _target)
    {
        TargetPosition = _target.position;
    }

    public void SetBase(Minion GO)
    {
        AnimationSpeed = GO.gameObject.GetComponent<Minion>().GetCountDownTimer();
        UnitThatShootsPosition = GO.transform.position;
        UnitThatShoots = GO.transform;
        lifeTime = AnimationSpeed;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (!UnitThatShoots)
    //    //    return;
    //    //For non ally hiting projectiles
    //    //if (other.gameObject.GetComponent<CProjectile>() || other.gameObject.GetComponent<Dub_Lazer_Projectile>() || other.gameObject.tag == UnitThatShoots.gameObject.tag)//Uses this line of if statement to ignore collision between ally && other projectiles
    //    //{
    //    //    Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
    //    //}

    //    //if (other.gameObject.GetComponent<SphereCollider>() && this.gameObject.GetComponent<SphereCollider>())
    //    //{
    //    //    Debug.Log("HAHA");
    //    //    Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), other.gameObject.GetComponent<SphereCollider>());
    //    //    Physics.IgnoreCollision(other.gameObject.GetComponent<SphereCollider>(), this.gameObject.GetComponent<SphereCollider>());
    //    //}

    //    //if (other.gameObject.GetComponent<BasicGameOBJ>())
    //    //{
    //    //    if (UnitThatShoots.GetComponent<BasicGameOBJ>().Enemy_Tag == other.gameObject.GetComponent<BasicGameOBJ>().tag)
    //    //    {
    //    //        //Debug.Log(other.gameObject.name);
    //    //        //Debug.Log(lifeTime + "HIT");
    //    //        Damage(UnitThatShoots, other.transform);
    //    //        Destroy(this.gameObject);
    //    //    }
    //    //}
    //    if (other.gameObject.GetComponent<SphereCollider>() && this.gameObject.GetComponent<SphereCollider>())
    //    {
    //        //if (!isHit)
    //        //{
    //        //    Physics.IgnoreCollision(other.gameObject.GetComponent<SphereCollider>(), this.gameObject.GetComponent<SphereCollider>());
    //        //    isHit = true;
    //        //    return;
    //        //}
    //        Physics.IgnoreCollision(other.gameObject.GetComponent<SphereCollider>(), this.gameObject.GetComponent<SphereCollider>());
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.GetComponent<SphereCollider>() && this.gameObject.GetComponent<SphereCollider>())
        //{
        //    Physics.IgnoreCollision(other.gameObject.GetComponent<SphereCollider>(), this.gameObject.GetComponent<SphereCollider>(), true);
        //}
        //if (other.gameObject.GetComponent<BasicGameOBJ>())
        //{
        //    if (UnitThatShoots.GetComponent<BasicGameOBJ>().Enemy_Tag == other.gameObject.GetComponent<BasicGameOBJ>().tag)
        //    {
        //        Damage(UnitThatShoots, other.transform);
        //        //Debug.Break();
        //        Destroy(this.gameObject);
        //    }
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<BasicGameOBJ>())
        {
            if (UnitThatShoots.GetComponent<BasicGameOBJ>().Enemy_Tag == other.gameObject.GetComponent<BasicGameOBJ>().tag)
            {
                Damage(UnitThatShoots, other.transform);
                //Debug.Break();
                Destroy(this.gameObject);
            }
        }
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    //if (!UnitThatShoots)
    //    //    return;

    //    Debug.Log(other);

    //    //if (other.gameObject.GetComponent<SphereCollider>() && this.gameObject.GetComponent<SphereCollider>())
    //    //{
    //    //    Physics.IgnoreCollision(other.gameObject.GetComponent<SphereCollider>(), this.gameObject.GetComponent<SphereCollider>(),true);
    //    //    //entered += 1;
    //    //}

    //    if (other.gameObject.GetComponent<BasicGameOBJ>())
    //    {
    //        if (UnitThatShoots.GetComponent<BasicGameOBJ>().Enemy_Tag == other.gameObject.GetComponent<BasicGameOBJ>().tag)
    //        {
    //            Damage(UnitThatShoots, other.transform);
    //            //Debug.Break();
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}

    #region Collider checks
    //private void OnCollisionEnter(Collision other)
    //{
    //    //if (this.projectile_type == PT.HEALING)
    //    //{

    //    //    //if (other.gameObject.GetComponent<CProjectile>() || other.gameObject.tag == UnitThatShoots.gameObject.tag)//Uses this line of if statement to ignore collision between ally && other projectiles
    //    //    //{
    //    //    //Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
    //    //    //}

    //    //    //if (other.gameObject.GetComponent<BasicGameOBJ>())
    //    //    //{
    //    //    //    if (UnitThatShoots.GetComponent<BasicGameOBJ>().Ally_Tag == other.gameObject.GetComponent<BasicGameOBJ>().tag)
    //    //    //    {
    //    //    //        //Debug.Log(other.gameObject.name);
    //    //    //        //Debug.Log(lifeTime + "HIT");
    //    //    //        if (other.gameObject == UnitThatShoots.gameObject)
    //    //    //        {
    //    //    //            Damage(UnitThatShoots, other.transform);
    //    //    //            Destroy(this.gameObject);
    //    //    //        }
    //    //    //    }
    //    //    //}
    //    //}
    //    //else
    //    //{
    //    if (!UnitThatShoots)
    //        return;
    //    //For non ally hiting projectiles
    //    if (other.gameObject.GetComponent<CProjectile>() || other.gameObject.GetComponent<Dub_Lazer_Projectile>() || other.gameObject.tag == UnitThatShoots.gameObject.tag)//Uses this line of if statement to ignore collision between ally && other projectiles
    //    {
    //        Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
    //    }

    //    if (other.gameObject.GetComponent<BasicGameOBJ>())
    //    {
    //        if (UnitThatShoots.GetComponent<BasicGameOBJ>().Enemy_Tag == other.gameObject.GetComponent<BasicGameOBJ>().tag)
    //        {
    //            //Debug.Log(other.gameObject.name);
    //            //Debug.Log(lifeTime + "HIT");
    //            Damage(UnitThatShoots, other.transform);
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}

    //private void OnCollisionStay(Collision other)
    //{
    //    if (other.gameObject.GetComponent<CHyperBeam>())
    //    {
    //        Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
    //    }

    //}
    #endregion

    void DisplayText(float DamageAmount)
    {

        GameObject newText = Instantiate(prefabtext) as GameObject;
        //newText.transform.parent = this.transform;
        newText.transform.position = this.transform.position;
        newText.GetComponent<TextMesh>().text = "" + DamageAmount;
        newText.GetComponent<TextMesh>().color = Color.red;
    }

    void Damage(Transform _unitThatShoot, Transform Target)
    {
        if (_unitThatShoot != null && Target != null)
        {
            Target.GetComponent<BasicGameOBJ>().TakeDamage(_unitThatShoot.GetComponent<BasicGameOBJ>().attackValue);
            DisplayText(_unitThatShoot.GetComponent<BasicGameOBJ>().attackValue);
        }
    }
}
