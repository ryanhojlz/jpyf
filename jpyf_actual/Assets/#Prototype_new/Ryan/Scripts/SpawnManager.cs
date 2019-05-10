using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Its week 10 why didnt i think of this
    public static SpawnManager Instance = null;

    public List<GameObject> SpawnList;
    // idk if local variables are cheaper
    GameObject reuseable;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    // Use this for initialization
 //   void Start ()
 //   {
		
	//}
	
	// Update is called once per frame
	//void Update ()
 //   {
		
	//}

    public GameObject SpawnObject(int id)
    {
        return Instantiate(SpawnList[id]);
    }

    public GameObject SpawnObject(int id, Vector3 pos, Quaternion rot, Transform parent)
    {
        return Instantiate(SpawnList[id], pos, rot, parent);

    }

    public GameObject SpawnObject(int id, bool idle)
    {
        reuseable = Instantiate(SpawnList[id]);
        return reuseable;
    }

    public GameObject SpawnObject(int id, Vector3 pos, Quaternion rot, Transform parent, bool idle)
    {
        reuseable = Instantiate(SpawnList[id], pos, rot, parent);
        return reuseable;
    }
}
