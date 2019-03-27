using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProjectile : MonoBehaviour {

    private Vector3 TargetPosition;
    private Vector3 UnitThatShootsPosition;
    private Transform UnitThatShoots;

    public float AnimationSpeed;
    float speed = 0f;
    float lifeTime = 0f;

    public GameObject prefabtext;

    // Use this for initialization
    void Start ()
    {
        this.transform.position = new Vector3(
            this.transform.position.x + (UnitThatShoots.transform.forward.x + UnitThatShoots.transform.localScale.x) * (this.GetComponent<SphereCollider>().radius),
            this.transform.position.y + (UnitThatShoots.transform.forward.y + UnitThatShoots.transform.localScale.y) * (this.GetComponent<SphereCollider>().radius),
            this.transform.position.z + (UnitThatShoots.transform.forward.z + UnitThatShoots.transform.localScale.z) * (this.GetComponent<SphereCollider>().radius)
            );	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!UnitThatShoots)
        {
            Destroy(gameObject);
            return;
        }

        float dist_between = (UnitThatShootsPosition - TargetPosition).magnitude;

        speed = (dist_between / AnimationSpeed);

        Vector3 dir = TargetPosition - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        //If life time becomes 0, Delete it
        //Debug.Log(lifeTime + "HIT");

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

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<BasicGameOBJ>())
        {
            if (UnitThatShoots.GetComponent<BasicGameOBJ>().Enemy_Tag == other.gameObject.GetComponent<BasicGameOBJ>().tag)
            {
                Debug.Log(lifeTime + "HIT");
                Damage(UnitThatShoots, other.transform);
                Destroy(this.gameObject);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if ((this.transform.position - other.transform.position).magnitude >= this.GetComponent<SphereCollider>().radius)
    //        return;

    //    if (!other.attachedRigidbody)
    //        return;

    //    if (other.GetComponent<BasicGameOBJ>())
    //    {
    //        if (UnitThatShoots.GetComponent<BasicGameOBJ>().Enemy_Tag == other.GetComponent<BasicGameOBJ>().tag)
    //        {
    //            Debug.Log(lifeTime + "HIT");
    //            Damage(UnitThatShoots, other.transform);
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}

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
        //var unit = _unit.GetComponent<BasicGameOBJ>();

        if (_unitThatShoot != null && Target != null)
        {
            Target.GetComponent<BasicGameOBJ>().TakeDamage(_unitThatShoot.GetComponent<BasicGameOBJ>().attackValue);
            DisplayText(_unitThatShoot.GetComponent<BasicGameOBJ>().attackValue);
        }
    }
}
