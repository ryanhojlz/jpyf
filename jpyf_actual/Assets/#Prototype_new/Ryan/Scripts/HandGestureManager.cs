using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandGestureManager : MonoBehaviour
{
    // Current script is not done
    // Game handler
    public Stats_ResourceScript handler = null;

    // Object Reference
    public Transform left_hand = null;
    public Transform right_hand = null;
    public Transform player_2 = null;

    // Mini game interactions
    private bool m_start_laserbeam = false;
    // Hitboxs the player have to hit
    public List<Transform> hitboxmarkers;
    


    // Use this for initialization
	void Start ()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();



	}
	
	// Update is called once per frame
	void Update ()
    {
        LazerBeamGame();
	}

    // I hate using this tbh
    private void FixedUpdate()
    {
        
    }

    public void SetLazerBeam(bool laserBeam) { m_start_laserbeam = laserBeam; }

    public bool GetLazerBeam() { return m_start_laserbeam; }

    public void StartLazerBeamInteraction()
    {
        if (!m_start_laserbeam)
        {
            m_start_laserbeam = true;
        }
    }

    private void LazerBeamGame()
    {
        if (m_start_laserbeam)
        {

        }
        else if (!m_start_laserbeam)
        {

        }
    }



}
