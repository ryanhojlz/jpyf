using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonScript : MonoBehaviour
{
    public GameObject textprefab;
    public int effect_counter = 0;
    public bool hitto = false;
    

    /// Made public so can debug from inspector
    public List<GameObject> HitMarkerList;
    // Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        SpawnText();
        hitto = false;

        //if (Input.GetKeyDown(KeyCode.H))
        //    effect_counter++;


        if (Input.GetKeyDown(KeyCode.L))
        {
            hitto = true;
            Debug.Log("aaaaaaaaaaaa hitto!! " + hitto);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DonStick")
        {
            //effect_counter++;
            hitto = true;
            GameObject.Find("DonDon").GetComponent<DonMiniGame>().DonInteraction(1);
        }

        /// Pushes hittable objects into the list
        if (other.gameObject.name == "DonHitMarker")
        {
            HitMarkerList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove Respective Object that leaves
        if (other.gameObject.name == "DonHitMarker")
        {
            HitMarkerList.Remove(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    void SpawnText()
    {
        //if (effect_counter > 0)
        //{
        //    GameObject go = Instantiate(textprefab) as GameObject;
        //    var pos = transform.position;
        //    pos.z += 5;
        //    go.transform.position = pos;
        //    effect_counter--;
        //}

        if (hitto)
        {
            GameObject go = Instantiate(textprefab) as GameObject;
            var pos = transform.position;
            pos.z += 5;
            go.transform.position = pos;
        }
    }

    void RunListInteraction()
    {
        /// If the don is not smacked
        if (!hitto)
            return;
        float distance = float.MaxValue;
        for (int i = 0; i < HitMarkerList.Count; ++i)
        {
            float magnitude = (HitMarkerList[i].transform.position - this.transform.position).magnitude;
            // Find nearest distance
            if (distance > magnitude)
            {
                distance = magnitude;
            }
        }
    }
    

    

    

}
