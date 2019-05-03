using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Script : MonoBehaviour {

    [SerializeField]
    float m_Health;

    float m_Health_max;
    // Use this for initialization
    void Start ()
    {
        m_Health_max = m_Health;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_Health <= 0f)
        {
            Destroy(this.gameObject);
        }
	}

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
    }

    public float GetHealth()
    {
        return m_Health;
    }

    public float GetMaxHealth()
    {
        return m_Health_max;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.GetComponent<bomb_script>())
    //    {
    //        if (other.GetComponent<bomb_script>().IsExplosion())
    //        {
    //            Destroy(this.gameObject);
    //        }

    //    }
    //}
}
