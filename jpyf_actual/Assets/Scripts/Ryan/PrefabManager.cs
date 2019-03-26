using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    List<GameObject> PrefabList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public GameObject GetPrefab(string prefabname)
    {
        GameObject _return = null;
        for (int i = 0; i < PrefabList.Count; ++i)
        {
            if (PrefabList[i].gameObject.name == prefabname)
            {
                _return = PrefabList[i].gameObject;
            }
        }
        if (_return == null)
        {
            _return = new GameObject("noprefabfound");
        }
        return _return;
    }
}
