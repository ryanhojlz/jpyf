using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBuffMiniGame : MonoBehaviour
{
    GameObject findplayer;
    public GameObject HitMarkerPrefab;
    public float minigame_length;   
    bool startminigame = false;
    bool updatePositionOnce = false;

    public List<float> spawnTriggers;
    public List<bool> boolTriggers;
    Vector3 referencePosition;
    
    // Use this for initialization
	void Start ()
    {
        findplayer = GameObject.Find("Camera_player");

        for (int i = 0; i < spawnTriggers.Count; i++)
        {
            bool trigger = false;
            boolTriggers.Add(trigger);
        }

        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            startminigame = true;
            //Debug.Log("Ran ran ruuuu");
            //SpawnObject(new Vector3(0.15f,0,0.3f));   
            //SpawnObject(new Vector3(-0.15f,0,0.3f));
        }
        if (startminigame)
        {
            if (!updatePositionOnce)
            {
                referencePosition = findplayer.transform.position;
                updatePositionOnce = true;
            }
            //    startminigame = false;
            //    SpawnObject(new Vector3(0.15f, 0, 0.3f));
            //    SpawnObject(new Vector3(-0.15f, 0, 0.3f));
            for (int i = 0; i < boolTriggers.Count; i++)
            {
                // if the trigger is false go ahead with the check
                if (boolTriggers[i] == false)
                {
                    if (minigame_length <= spawnTriggers[i])
                    {
                        // if its the current time
                        boolTriggers[i] = true;
                        // do the spawing here
                        SpawningSwitchCase(i);
                        
                    }
                }
            }


            minigame_length -= 1 * Time.deltaTime;
            if (minigame_length < 0)
            {
                startminigame = false;
                updatePositionOnce = false;
            }
        }

        // Reinit
        if (!startminigame)
        {
            minigame_length = 5;
            for (int i = 0; i < boolTriggers.Count; i++)
            {
                boolTriggers[i] = false;
            }
        }
    }

    void SpawnObject(Vector3 in_pos)
    {
        GameObject go = Instantiate(HitMarkerPrefab) as GameObject;
        //go.transform.SetParent(referencePosition);
        go.transform.localPosition = Vector3.zero;
        go.transform.localPosition = referencePosition + in_pos;
        //go.transform.parent = null;
        
        //Vector3 _pos = go.transform.position;
        //go.transform.SetParent(null);
    }

    public void SetMiniGame(bool boolean)
    {
        startminigame = boolean;
    
    }

    void SpawningSwitchCase(int i)
    {
        float position_offsetz = 0.45f;
        float position_offsety = 0.27f;
        switch (i)
        {
            case 0:
                SpawnObject(new Vector3(0.2f, position_offsety, position_offsetz));
                SpawnObject(new Vector3(-0.2f, -position_offsety, position_offsetz));
                break;
            case 1:
                SpawnObject(new Vector3(0.2f, -position_offsety, position_offsetz));
                SpawnObject(new Vector3(-0.2f, position_offsety, position_offsetz));
                break;
            case 2:
                SpawnObject(new Vector3(0.35f, -(position_offsety + 0.05f), position_offsetz));
                SpawnObject(new Vector3(-0.35f, (position_offsety + 0.05f), position_offsetz));
                break;
            case 3:
                SpawnObject(new Vector3(0.35f, (position_offsety + 0.05f), position_offsetz));
                SpawnObject(new Vector3(-0.35f, -(position_offsety + 0.05f), position_offsetz));
                break;
        }
    }

    public void TurnOnMiniGame()
    {
        startminigame = true;
        referencePosition = findplayer.transform.position;
    }

}
