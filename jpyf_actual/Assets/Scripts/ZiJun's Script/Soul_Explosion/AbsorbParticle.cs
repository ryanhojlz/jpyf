using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbParticle : MonoBehaviour
{
    public GameObject[] targetPosition;

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    //private ParticleSystem PSystem;
    private ParticleCollisionEvent[] CollisionEvents;


    // Update is called once per frame
    private void LateUpdate()
    {
        InitializeIfNeeded();

        targetPosition = GameObject.FindGameObjectsWithTag("Soul_Absorber");

       

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        ///Debug.Log("Number of Particles alife = " + numParticlesAlive);

        if (targetPosition.Length > 0)
        {
            // Change only the particles that are alive
            for (int i = 0; i < numParticlesAlive; i++)
            {
                Vector3 PosZero = new Vector3(0, 0, 0);
                Vector3 PosToOffSet = PosZero - m_Particles[i].position;

                float nearest = float.MaxValue;
                Transform TargetedPosition = null;

                //for (int j = 0; j < targetPosition.Length; j++)
                //{
                //    float distance = (m_Particles[i].position - (targetPosition[j].position - this.transform.position)).magnitude;

                //    if (distance < nearest)
                //    {
                //        nearestIndex = j;
                //        nearest = distance;
                //    }
                //}



                foreach (GameObject TargetPosition in targetPosition)
                {
                    float distance = (m_Particles[i].position - (TargetPosition.transform.position - this.transform.position)).magnitude;

                    if (distance < nearest)
                    {
                        nearest = distance;
                        TargetedPosition = TargetPosition.transform;
                    }
                }

                Vector3 Direction = (m_Particles[i].position - (TargetedPosition.position - this.transform.position)).normalized;

                if (m_Particles[i].remainingLifetime / m_Particles[i].startLifetime < 0.5)
                    m_Particles[i].position = new Vector3(m_Particles[i].position.x - Direction.x, m_Particles[i].position.y - Direction.y, m_Particles[i].position.z - Direction.z);

            }

        }

        if (numParticlesAlive <= 0)
        {
            Destroy(this.gameObject);//.SetActive(false);
        }

        // Apply the particle changes to the Particle System
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    //public void OnParticleCollision(GameObject other)
    //{
    //    CollisionEvents = new ParticleCollisionEvent[100];

    //    int collCount = m_System.GetSafeCollisionEventSize();

    //    if (collCount > CollisionEvents.Length)
    //        CollisionEvents = new ParticleCollisionEvent[collCount];

    //    //int eventCount = m_System.GetCollisionEvents(other, CollisionEvents);
    //    int eventCount = CollisionEvents.Length;

    //    Debug.Log("Removing Particles");

    //    for (int i = 0; i < eventCount; i++)
    //    {
    //        //TODO: Do your collision stuff here. 
    //        // You can access the CollisionEvent[i] to obtaion point of intersection, normals that kind of thing
    //        // You can simply use "other" GameObject to access it's rigidbody to apply force, or check if it implements a class that takes damage or whatever

    //        //other.SetActive(false);
    //        Debug.Log("Removing Particles");
    //        //Destroy(other.gameObject);
    //    }
    //}

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
    }
}
