﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    protected bool hasDone;//checking if this achievement is completed
    public string Achievement_name;//name of achievement
    public string Achievement_descriptions;//description of achievement

    //public GameObject Panel;

    public RawImage Image;

    //public GameObject Name;

    //public GameObject Description;

    public GameObject Achievement_Panel;

    public int index = 0;

    bool Added = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Use this for initialization
    void Start ()
    {
        //this.transform.parent.GetComponent<GlobalAchievementManager>().AddAchievement(this);
        this.transform.parent.GetComponent<GlobalAchievementManager>().AddAchievement(this);
        LoadAchievement();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void SaveAchievement()
    {
        PlayerPrefs.SetInt(Achievement_name, hasDone ? 1 : 0);
    }

    public void LoadAchievement()
    {
        //Loading Achievement

        hasDone = PlayerPrefs.GetInt(Achievement_name) == 1 ? true : false;
    }

    public void ResetAchievement()
    {
        PlayerPrefs.SetInt(Achievement_name, 0);
    }

    public bool GetCompletion()
    {
        return hasDone;
    }

    protected IEnumerator TriggerAchievement()
    {
        hasDone = true;
        GameObject TempAchievement = Instantiate(Achievement_Panel);
        TempAchievement.transform.SetParent(GameObject.Find("UI").transform.Find("Canvas").transform, false);
        TempAchievement.transform.Find("Achievement_Image");
        TempAchievement.transform.Find("Achievement_Title").GetComponent<Text>().text = Achievement_name;
        TempAchievement.transform.Find("Achievement_Description").GetComponent<Text>().text = Achievement_descriptions;
        SaveAchievement();
        //PlayerPrefs.SetInt(Achievement_name, index);
        //Image.SetActive(true);
        //Name.GetComponent<Text>().text = Achievement_name;
        //Description.GetComponent<Text>().text = Achievement_descriptions;
        //Panel.SetActive(true);
        float time = 0;
        if (TempAchievement.GetComponent<Achievement_Slider>())
        {
            time = TempAchievement.GetComponent<Achievement_Slider>().LifeTime;
        }
        yield return new WaitForSeconds(time);
        Destroy(TempAchievement);
        
        //Image.SetActive(false);
        //Name.GetComponent<Text>().text = "";
        //Description.GetComponent<Text>().text = "";
        //Panel.SetActive(false);

    }

    private void OnDestroy()
    {
        Debug.Log("Delete Here after wanting to save");
        ResetAchievement();
    }
}
