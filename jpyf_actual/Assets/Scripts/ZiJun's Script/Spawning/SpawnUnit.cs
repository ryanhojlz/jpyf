using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    public GameObject UnitToSpawn;
    public enum SPAWNTYPE
    {
        MELEE,
        RANGE,
        HEAL
    }

    public SPAWNTYPE Unit_Type;

    //Uncomment this if using minion cards to spawn

    //bool hasSpawn = false;

    ////static bool unitspawned = false;
   
    
    //// Update is called once per frame
    //void Update()
    //{
    //    RaycastHit hit;
    //    Ray landingRay = new Ray(transform.position, Vector3.down);

    //    if (Physics.Raycast(landingRay, out hit, 1f))
    //    {
    //        if (!hasSpawn)
    //        {
    //            hasSpawn = true;
    //            SpawningUnit();

    //            Destroy(this.gameObject);
    //        }
    //    }
    //}

    //void SpawningUnit()
    //{
    //    GameObject thisnew = Instantiate(UnitToSpawn, this.transform);
    //    thisnew.transform.parent = null;
    //    thisnew.transform.position = this.transform.position;
    //    //Destroy(this.gameObject);
    //}

   
}
