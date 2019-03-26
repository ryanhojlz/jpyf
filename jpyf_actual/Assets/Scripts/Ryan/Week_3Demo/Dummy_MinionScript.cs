using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_MinionScript : MonoBehaviour
{
    public GameObject prefab_fordeathanimation;
    public enum faction
    {
        A,
        B
    };
    public faction _faction;
	
    // Use this for initialization
	void Start ()
    {
        this.gameObject.name = "DemoDummy";	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float speed = 10;
        var _pos = this.gameObject.transform.position;
        if (_faction == faction.A)
        {
            _pos.z += speed * Time.deltaTime;
        }
        else
        {
            _pos.z -= speed * Time.deltaTime;
        }
        this.gameObject.transform.position = _pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name == "demo_ground")
        //    return;

        if (collision.gameObject.name == "DemoDummy")
        {
            GameObject go = Instantiate(prefab_fordeathanimation) as GameObject;
            go.transform.position = transform.position;
            if (_faction == faction.A)
            {
                if (collision.gameObject.GetComponent<Dummy_MinionScript>()._faction == faction.B)
                {
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                }
            }
            else if (_faction == faction.B)
            {
                if (collision.gameObject.GetComponent<Dummy_MinionScript>()._faction == faction.A)
                {
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);

                }
            }
        }
    }
}
