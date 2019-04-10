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
        //Debug.Log("Parent : " + name);

        //for (int i = 0; i < this.transform.childCount; ++i)
        //{
        //    Transform child = this.transform.GetChild(i);
        //    if (child.GetComponent<Achievement>())//Checking if it has an achievement class
        //    {
        //        ListOfAchievements.Add(child.GetComponent<Achievement>());
        //    }
        //}
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
