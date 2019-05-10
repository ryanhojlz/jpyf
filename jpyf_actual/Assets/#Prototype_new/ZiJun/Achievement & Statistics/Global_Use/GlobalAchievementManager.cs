using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAchievementManager : MonoBehaviour
{
    public List<Achievement> ListOfAchievements = new List<Achievement>();

    public static GlobalAchievementManager Instance = null;

    public Transform Canvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

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

    public void SetCanvas(Transform canvas)
    {
        Canvas = canvas;
    }
}
