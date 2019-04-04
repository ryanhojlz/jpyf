using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TimerHolder
{
    public GameObject OBJ;
    public float timer;

    public void SetTimer(float time)
    {
        timer = time;
    }
}

public class Dub_Lazer_Projectile : MonoBehaviour {

    Transform UnitThatShoots;
    public float AttackperSecond;
    public float DamageperSecond;
    public float LifeTime = 10;
    float CountDownTimer = 0;
    float AttackInterval;

    public List<TimerHolder> InDamageRange;

    public GameObject prefabtext;

    // Use this for initialization
    void Start ()
    {
        //this.transform.position = new Vector3(
        //   this.transform.position.x + (UnitThatShoots.transform.forward.x * (UnitThatShoots.transform.localScale.x + this.transform.localScale.x)) * (this.GetComponent<CapsuleCollider>().height),
        //   this.transform.position.y + (UnitThatShoots.transform.forward.y * (UnitThatShoots.transform.localScale.y + this.transform.localScale.y)) * (this.GetComponent<CapsuleCollider>().height),
        //   this.transform.position.z + (UnitThatShoots.transform.forward.z * (UnitThatShoots.transform.localScale.z + this.transform.localScale.z)) * (this.GetComponent<CapsuleCollider>().height)
        //   );

        this.transform.position = UnitThatShoots.position + UnitThatShoots.forward * ((UnitThatShoots.localScale.z + (this.transform.localScale.z) * 0.5f));
        this.transform.localRotation = UnitThatShoots.localRotation;

        this.tag = UnitThatShoots.tag;
       // this.gameObject.layer = UnitThatShoots.gameObject.layer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        AttackInterval = 1 / AttackperSecond;

        LifeTime -= Time.deltaTime;
        CountDownTimer -= Time.deltaTime;

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

        //for (int i = 0; i < InDamageRange.Count; ++i)
        //{
        //    if (InDamageRange[i].timer < 0)
        //    {
        //        InDamageRange.Remove(InDamageRange[i]);
        //    }
        //}

        //this.transform.position = UnitThatShoots.position + (UnitThatShoots.transform.forward * (this.GetComponent<CapsuleCollider>().height * (this.GetComponent<CapsuleCollider>().transform.localScale.y * 0.5f)) + UnitThatShoots.transform.forward * this.transform.localScale.x);


        this.transform.position = UnitThatShoots.position + UnitThatShoots.forward * ((UnitThatShoots.localScale.z + (this.transform.localScale.z) * 0.5f));
        this.transform.localRotation = UnitThatShoots.localRotation;
        //this.transform.LookAt(UnitThatShoots.transform.up + this.transform.position);
    }

    public void SeekUser(Transform user)
    {
        UnitThatShoots = user;

        //this.transform.parent = UnitThatShoots;

        //this.transform.localPosition = Vector3.zero;

        //this.transform.localPosition = UnitThatShoots.transform.forward * this.transform.localScale.y;

        //this.transform.position = UnitThatShoots.position + (UnitThatShoots.transform.forward *
        //   (this.GetComponent<CapsuleCollider>().height *
        //   (this.GetComponent<CapsuleCollider>().transform.localScale.y * 0.5f)) + UnitThatShoots.transform.forward * this.transform.localScale.x);
    }

    void DisplayText(float DamageAmount, Vector3 position)
    {

        GameObject newText = Instantiate(prefabtext) as GameObject;
        //newText.transform.parent = this.transform;
        newText.transform.position = position;
        newText.GetComponent<TextMesh>().text = "" + DamageAmount;
        newText.GetComponent<TextMesh>().color = Color.red;
    }

    private void OnTriggerStay(Collider other)
    {

        //REGISTER ALL INCOMING OTHER PARAMS
        //IF HAVE DUPLICATE, DONT REGISTER
        //ONCE REGISTERED ONLY THEN START DAMAGE (WRITE IN SEPERATE FUNCTION)

        #region Collider checks
        if (!other.gameObject || !UnitThatShoots.gameObject)
            return;

        if (UnitThatShoots.tag == other.tag)//If ally dont attack
            return;

        if (other.gameObject.GetComponent<SphereCollider>() && this.gameObject.GetComponent<CapsuleCollider>())
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<SphereCollider>(), this.gameObject.GetComponent<CapsuleCollider>());
        }
        #endregion

        //TimerHolder Temp;

        //if (InDamageRange.Contains(other.gameObject))

        //if (AddToList)//If not in list, Add to list
        //{
        //    TimerHolder temp = new TimerHolder();
        //    temp.OBJ = other.gameObject;
        //    temp.timer = AttackInterval;
        //    InDamageRange.Add(temp);
        //}

        //for (int i = 0; i < InDamageRange.Count; ++i)
        //{
        //    float TempTime = InDamageRange[i].timer;
        //    TempTime -= Time.deltaTime;
        //    InDamageRange[i].SetTimer(TempTime);

        //    if (TempTime < 0)
        //    {
        //        //Debug.Log("Its came here");
        //        //if (InDamageRange[i].OBJ.GetComponent<BasicGameOBJ>())
        //        //{
        //        //    InDamageRange[i].OBJ.GetComponent<BasicGameOBJ>().TakeDamage(DamageperSecond);
        //        //    InDamageRange.Remove(InDamageRange[i]);
        //        //    continue;
        //        //}
        //        ////DisplayText(DamageperSecond, other.GetComponent<Collision>().contacts[0].point);
        //        //CountDownTimer = AttackInterval;
        //    }

        //}

        //Debug.Log(other.name);

        if (other.gameObject.GetComponent<BasicGameOBJ>())
        {
            //if (CountDownTimer < 0)
            //{
           

                other.gameObject.GetComponent<BasicGameOBJ>().TakeDamage((int)DamageperSecond * Time.deltaTime);
                DisplayText(DamageperSecond, other.transform.position);
                CountDownTimer = AttackInterval;
            //}
        }

    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.GetComponent<Dub_Lazer_Projectile>())
    //    {
    //        Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>());
    //    }
    //}

    //private void OnCollisionStay(Collision other)
    //{
    //    if (other.gameObject.GetComponent<BasicGameOBJ>())
    //    {
    //        if (CountDownTimer < 0)
    //        {
    //            other.gameObject.GetComponent<BasicGameOBJ>().TakeDamage(DamageperSecond);
    //            DisplayText(DamageperSecond, other.contacts[0].point);
    //            CountDownTimer = AttackInterval;
    //        }
    //    }

    //}
}
