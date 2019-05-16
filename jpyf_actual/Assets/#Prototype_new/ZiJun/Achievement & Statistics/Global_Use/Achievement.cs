using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    protected bool hasDone;//checking if this achievement is completed
    public string Achievement_name;//name of achievement
    public string Achievement_descriptions;//description of achievement

    //public GameObject Panel;

    //public RawImage Image;

    public Sprite Image;

    //public GameObject Name;

    //public GameObject Description;

    public GameObject Achievement_Panel;

    public int index = 0;

    bool Added = false;

    Transform Canvas = null;

    //public static Achievement Instance = null;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Use this for initialization
    void Start ()
    {
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

    public void SetCanvas(Transform canvas)
    {
        Canvas = canvas;
    }

    protected IEnumerator TriggerAchievement()
    {
        hasDone = true;
        SetCanvas(GlobalAchievementManager.Instance.Canvas);
        if (Canvas)
        {

            Debug.Log("Got come here");
            
            GameObject TempAchievement = Instantiate(Achievement_Panel);
            TempAchievement.transform.SetParent(Canvas, false);
            TempAchievement.transform.Find("Achievement_Image").GetComponent<Image>().sprite = Image;
            TempAchievement.transform.Find("Achievement_Title").GetComponent<Text>().text = Achievement_name;
            TempAchievement.transform.Find("Achievement_Description").GetComponent<Text>().text = Achievement_descriptions;
            SaveAchievement();

            float time = 0;
            if (TempAchievement.GetComponent<Achievement_Slider>())
            {
                time = TempAchievement.GetComponent<Achievement_Slider>().LifeTime;
            }
            yield return new WaitForSeconds(time);
            Destroy(TempAchievement);
        }

    }

    private void OnDestroy()
    {
        Debug.Log("Delete Here after wanting to save");
        ResetAchievement();
    }
}
