using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dub_Lazer_Projectile : MonoBehaviour {

    Transform UnitThatShoots;
    public float AttackperSecond;
    public float DamageperSecond;
    public float LifeTime = 10;
    float CountDownTimer = 0;
    float AttackInterval;



    public GameObject prefabtext;

    // Use this for initialization
    void Start ()
    {
        //this.transform.position = new Vector3(
        //   this.transform.position.x + (UnitThatShoots.transform.forward.x * (UnitThatShoots.transform.localScale.x + this.transform.localScale.x)) * (this.GetComponent<CapsuleCollider>().height),
        //   this.transform.position.y + (UnitThatShoots.transform.forward.y * (UnitThatShoots.transform.localScale.y + this.transform.localScale.y)) * (this.GetComponent<CapsuleCollider>().height),
        //   this.transform.position.z + (UnitThatShoots.transform.forward.z * (UnitThatShoots.transform.localScale.z + this.transform.localScale.z)) * (this.GetComponent<CapsuleCollider>().height)
        //   );
    }
	
	// Update is called once per frame
	void Update ()
    {
        AttackInterval = 1 / AttackperSecond;

        CountDownTimer -= Time.deltaTime;
        LifeTime -= Time.deltaTime;

        if (!UnitThatShoots)
        {
            Destroy(this.gameObject);
            return;
        }

        //Debug.Log("LifeTime = " + LifeTime);

        if (LifeTime < 0)
        {
            
            Destroy(this.gameObject);
            UnitThatShoots.GetComponent<Dub_Sniper>().SetLaser(false);
            return;
        }

        //this.transform.position = new Vector3(
        //  UnitThatShoots.transform.position.x + (UnitThatShoots.transform.forward.x * (UnitThatShoots.transform.localScale.x + this.transform.localScale.y)) * ((this.GetComponent<CapsuleCollider>().height)),
        //  UnitThatShoots.transform.position.y + (UnitThatShoots.transform.forward.y * (UnitThatShoots.transform.localScale.y + this.transform.localScale.z)) * (this.GetComponent<CapsuleCollider>().radius),
        //  UnitThatShoots.transform.position.z + (UnitThatShoots.transform.forward.z * (UnitThatShoots.transform.localScale.z + this.transform.localScale.x)) * (this.GetComponent<CapsuleCollider>().radius)
        //  );

        this.transform.position = UnitThatShoots.position + (UnitThatShoots.transform.forward * (this.GetComponent<CapsuleCollider>().height * (this.GetComponent<CapsuleCollider>().transform.localScale.y *  0.5f)) + UnitThatShoots.transform.forward * this.transform.localScale.x);

        this.transform.LookAt(UnitThatShoots.transform.up + this.transform.position);
    }

    public void SeekUser(Transform user)
    {
        UnitThatShoots = user;
    }

    void DisplayText(float DamageAmount, Vector3 position)
    {

        GameObject newText = Instantiate(prefabtext) as GameObject;
        //newText.transform.parent = this.transform;
        newText.transform.position = position;
        newText.GetComponent<TextMesh>().text = "" + DamageAmount;
        newText.GetComponent<TextMesh>().color = Color.red;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Dub_Lazer_Projectile>())
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<BasicGameOBJ>())
        {
            if (CountDownTimer < 0)
            {
                other.gameObject.GetComponent<BasicGameOBJ>().TakeDamage(DamageperSecond);
                DisplayText(DamageperSecond, other.contacts[0].point);
                CountDownTimer = AttackInterval;
            }
        }

    }
}
