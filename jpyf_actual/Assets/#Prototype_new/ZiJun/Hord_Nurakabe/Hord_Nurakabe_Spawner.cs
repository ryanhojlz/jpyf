using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hord_Nurakabe_Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject Nurakabe_Hord = null;

    [SerializeField]
    Transform m_Max = null;

    [SerializeField]
    Transform m_Min = null;
    // Use this for initialization

    public float m_speed_min = 10f;
    public float m_speed_max = 10f;

    float m_speed = 10f;

    public float m_TimeToSpawn_min = 1f;
    public float m_TimeToSpawn_max = 1f;

    float m_TimeToSpawn = 0f;

    public bool m_canSpawn;

    float m_countDown = 1f;

    Vector3 m_dir = Vector3.zero;

    bool m_Towards_m_Max = true;

    

    float offsetDistance = 0.5f;

    

    void Start()
    {
        //m_Min = transform.GetChild(0);
        //m_Max = transform.GetChild(1);

        m_countDown = Random.Range(m_TimeToSpawn_min, m_TimeToSpawn_max);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Towards_m_Max)
        {
            m_dir = m_Max.position - this.transform.position;
            if (m_dir != Vector3.zero)
            {
                m_dir.Normalize();
            }
            
        }
        else
        {
            m_dir = m_Min.position - this.transform.position;
            if (m_dir != Vector3.zero)
            {
                m_dir.Normalize();
            }
        }

        this.transform.position += m_dir * m_speed * Time.deltaTime;

        if (m_Towards_m_Max)
        {
            if ((m_Max.position - this.transform.position).magnitude <= offsetDistance)
            {
                m_Towards_m_Max = false;
            }
        }
        else
        {
            if ((m_Min.position - this.transform.position).magnitude <= offsetDistance)
            {
                m_Towards_m_Max = true;
            }
        }

        if (m_canSpawn)
        {
            m_countDown -= Time.deltaTime;

            if (m_countDown < 0)
            {
                m_TimeToSpawn = Random.Range(m_TimeToSpawn_min, m_TimeToSpawn_max);
                m_speed = Random.Range(m_speed_min, m_speed_max);
                m_countDown = m_TimeToSpawn;

                Instantiate<GameObject>(Nurakabe_Hord, this.transform.position, Nurakabe_Hord.transform.rotation);

            }
        }
    }
}
