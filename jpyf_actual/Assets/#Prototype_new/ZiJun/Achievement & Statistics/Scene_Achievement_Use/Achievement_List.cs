using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement_List : MonoBehaviour
{
    public RectTransform panel;

    GameObject Canvas = null;

    public GameObject Achi_Prefeb;

    public List<Achievement> Achievements;

    public List<RectTransform> Achievement_Panels = new List<RectTransform>();

    float offsetPercentage = 0.055f;
    float offsetBetween = 0f;

    float offsetUpPercentage = 0.1f;

    float scrollingspeed = 300f;

    Vector2 position = Vector2.zero;

	void Start ()
    {
        Achievements = GameObject.Find("AchievementManager").GetComponent<GlobalAchievementManager>().ListOfAchievements;

        Canvas = GameObject.Find("Canvas");

        offsetBetween = panel.GetComponent<RectTransform>().rect.width * offsetPercentage;

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

            position = TempAchievement.GetComponent<RectTransform>().anchoredPosition;
            position.x = TempAchievement.GetComponent<RectTransform>().rect.width * 0.5f + offsetBetween;
            position.y = TempAchievement.GetComponent<RectTransform>().rect.height * 0.5f + (offsetUpPercentage * Canvas.GetComponent<RectTransform>().rect.height);
            position.x += TempAchievement.GetComponent<RectTransform>().rect.width * i + offsetBetween * i;
            TempAchievement.GetComponent<RectTransform>().anchoredPosition = position;

            TempAchievement.transform.Find("Achi_Image").GetComponent<Image>().sprite = Achievements[i].GetComponent<Achievement>().Image;
            TempAchievement.transform.Find("Achi_Title").GetComponent<Text>().text = Achievements[i].GetComponent<Achievement>().Achievement_name;
            TempAchievement.transform.Find("Achi_Description").GetComponent<Text>().text = Achievements[i].GetComponent<Achievement>().Achievement_descriptions;

            Achievement_Panels.Add(TempAchievement.GetComponent<RectTransform>());
        }

    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            PanelMoveUp();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            PanelMoveDown();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PanelMoveRight();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            PanelMoveLeft();
        }

	}

    void PanelMoveUp()
    {
        if (Achievement_Panels.Count > 0)
        {
            if (Achievement_Panels[Achievement_Panels.Count - 1].position.y - (Achievement_Panels[Achievement_Panels.Count - 1].rect.height * 0.5f) > Canvas.GetComponent<RectTransform>().position.y - Canvas.GetComponent<RectTransform>().rect.height * 0.5f)
                return;
        }

        position = panel.GetComponent<RectTransform>().anchoredPosition;
        position.y += scrollingspeed * Time.deltaTime;
        panel.GetComponent<RectTransform>().anchoredPosition = position;
    }

    void PanelMoveDown()
    {
        if (Achievement_Panels.Count > 0)
        {
            if (Achievement_Panels[0].position.y + (Achievement_Panels[0].rect.height * 0.5f) < Canvas.GetComponent<RectTransform>().position.y + Canvas.GetComponent<RectTransform>().rect.height * 0.5f)
                return;
        }

        position = panel.GetComponent<RectTransform>().anchoredPosition;
        position.y -= scrollingspeed * Time.deltaTime;
        panel.GetComponent<RectTransform>().anchoredPosition = position;
    }

    void PanelMoveLeft()
    {
        if (Achievement_Panels.Count > 0)
        {
            if (Achievement_Panels[Achievement_Panels.Count - 1].position.x + (Achievement_Panels[Achievement_Panels.Count - 1].rect.width * 0.5f) + offsetBetween * 1.1f <= Canvas.GetComponent<RectTransform>().position.x + Canvas.GetComponent<RectTransform>().rect.width * 0.5f)
                return;
        }

        position = panel.GetComponent<RectTransform>().anchoredPosition;
        position.x -= scrollingspeed * Time.deltaTime;
        panel.GetComponent<RectTransform>().anchoredPosition = position;
    }

    void PanelMoveRight()
    {
        if (Achievement_Panels.Count > 0)
        {
            if (Achievement_Panels[0].position.x - (Achievement_Panels[0].rect.width * 0.5f) - offsetBetween * 1.1f >= Canvas.GetComponent<RectTransform>().position.x - Canvas.GetComponent<RectTransform>().rect.width * 0.5f)
                return;
        }

        position = panel.GetComponent<RectTransform>().anchoredPosition;
        position.x += scrollingspeed * Time.deltaTime;
        panel.GetComponent<RectTransform>().anchoredPosition = position;
    }
}
