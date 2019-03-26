using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeProjectile : MonoBehaviour
{
    private Transform target;
    private Transform UnitThatShoots;
    public float AnimationSpeed;
    float speed = 70f;
    float lifeTime = 0f;
    public GameObject prefabtext;

    // Start is called before the first frame update
    void Start()
    {
       
        //AttackSpeed = this.gameObject.GetComponent<Minion>().attackSpeedValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this)
            return;

        if (!UnitThatShoots)
        {
            Destroy(gameObject);
            return;
        }

        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        //if (lifeTime <= 0f && this)
        //{
        //    Debug.Log("Destroy projectile");
        //    Destroy(this.gameObject);
        //    return;//Stop updating after deleting
        //}
        //float dist_between = (UnitThatShoots.position - target.position).magnitude;

        //speed = (dist_between / AnimationSpeed);

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            // SpawnHealText(0f);
            HitTarget();
            return;
        }

        lifeTime -= Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    public void SetBase(Minion GO)
    {
        AnimationSpeed = GO.gameObject.GetComponent<Minion>().GetCountDownTimer();
        UnitThatShoots = GO.transform;
        lifeTime = AnimationSpeed;
    }

    void HitTarget()
    {
        Damage(target);
        Destroy(gameObject);
    }

    void DisplayText(float DamageAmount)
    {
        if (DamageAmount > 0f)
        {
            GameObject newText = Instantiate(prefabtext) as GameObject;
            //newText.transform.parent = this.transform;
            newText.transform.position = this.transform.position;
            newText.GetComponent<TextMesh>().text = "" + DamageAmount;
            newText.GetComponent<TextMesh>().color = Color.red;
        }
    }

    void Damage(Transform _unit)
    {
        Minion unit = _unit.GetComponent<Minion>();

        if (unit != null)
        {
            unit.TakeDamage(UnitThatShoots.gameObject.GetComponent<Minion>().attackValue);

            DisplayText(UnitThatShoots.gameObject.GetComponent<Minion>().attackValue);
        }
    }
}
