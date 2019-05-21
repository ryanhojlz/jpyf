using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public GameObject AchievementManager = null;
    // Use this for initialization
    void Awake()
    {
        if (GameObject.Find("AchievementManager") && GameObject.Find("AchievementManager").activeSelf)
        {
            Destroy(this.gameObject);
        }
        else
        {
            AchievementManager.SetActive(true);
        }
    }
}
