using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement_List : MonoBehaviour
{

    public GameObject Achi_Prefeb;

    public List<Achievement> Achievements;

	// Use this for initialization
	void Start ()
    {
        Achievements = GameObject.Find("AchievementManager").GetComponent<GlobalAchievementManager>().ListOfAchievements;

        GameObject Canvas = GameObject.Find("Canvas");

        for (int i = 1; i < Achievements.Count; ++i)
        {
            for (int j = 0; j < (Achievements.Count - i); ++j)
            {
                if (Achievements[j].index > Achievements[j + 1].index)
                {
                    Achievement temp = Achievements[j];
                    Achievements[j] = Achievements[j + 1];
                    Achievements[j + 1] = temp;
                }
            }
        }

        for (int i = 0; i < Achievements.Count; ++i)
        {
            GameObject TempAchievement = Instantiate(Achi_Prefeb);

            if (Achievements[i].GetComponent<Achievement>().GetCompletion())
            {
                TempAchievement.GetComponent<Image>().color = UnityEngine.Color.yellow;
            }

            TempAchievement.transform.SetParent(GameObject.Find("Panel").transform, false);
            TempAchievement.transform.position = Canvas.transform.position + new Vector3(0, (Canvas.GetComponent<RectTransform>().rect.height * 0.5f * Canvas.transform.lossyScale.y) - (TempAchievement.GetComponent<RectTransform>().rect.height * 0.5f * TempAchievement.transform.lossyScale.y), 0);
            //TempAchievement.transform.Find("Achi_Image").GetComponent<RawImage>().texture = Achievements[i].GetComponent<Achievement>().Image.texture;
            TempAchievement.transform.Find("Achi_Title").GetComponent<Text>().text = Achievements[i].GetComponent<Achievement>().Achievement_name;
            TempAchievement.transform.Find("Achi_Description").GetComponent<Text>().text = Achievements[i].GetComponent<Achievement>().Achievement_descriptions;

            TempAchievement.transform.position -= new Vector3(0, (TempAchievement.GetComponent<RectTransform>().rect.height * TempAchievement.transform.lossyScale.y) * i, 0);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
