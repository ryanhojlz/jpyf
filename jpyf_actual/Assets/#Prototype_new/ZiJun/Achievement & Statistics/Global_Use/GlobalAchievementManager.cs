using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAchievementManager : MonoBehaviour
{
    public List<Achievement> ListOfAchievements = new List<Achievement>();
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update ()
    {
        //Checking for achievements that are completed
        
	}

    public void AddAchievement(Achievement achi)
    {
        ListOfAchievements.Add(achi);
    }
}
