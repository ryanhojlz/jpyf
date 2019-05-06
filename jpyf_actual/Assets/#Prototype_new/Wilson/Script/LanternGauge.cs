using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanternGauge : MonoBehaviour
{
    public Image blackImage;
    Color variable;
    Color Original;
    bool reverse = false;

    public float speed = 3f;
    float offset = 0.1f;
    // Use this for initialization

    Stats_ResourceScript ForPlayer = null;

    Vector3 scale = Vector3.zero;

    void Start()
    {
        Original = blackImage.color;
        Original.a = 0.5f;
        variable = blackImage.color;
        variable.a = 0f;

        ForPlayer = Stats_ResourceScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (((float)ForPlayer.m_LanternHp / (float)ForPlayer.m_LanternHpCap <= 0.25f) && ((float)ForPlayer.m_LanternHp / (float)ForPlayer.m_LanternHpCap >= 0.21f))
        {
            Fade();
            scale.Set(15, 15, 1);
            blackImage.rectTransform.localScale = scale;
            Debug.Log(blackImage.rectTransform.localScale);
        }
        else if (((float)ForPlayer.m_LanternHp / (float)ForPlayer.m_LanternHpCap <= 0.20f) && ((float)ForPlayer.m_LanternHp / (float)ForPlayer.m_LanternHpCap >= 0.11f))
        {
            Fade();
            scale.Set(13, 13, 1);
            blackImage.rectTransform.localScale = scale;
            Debug.Log(blackImage.rectTransform.localScale);
        }
        else if (((float)ForPlayer.m_LanternHp / (float)ForPlayer.m_LanternHpCap <= 0.10f) && ((float)ForPlayer.m_LanternHp / (float)ForPlayer.m_LanternHpCap >= 0f))
        {
            Fade();
            scale.Set(10, 10, 1);
            blackImage.rectTransform.localScale = scale;
            Debug.Log(blackImage.rectTransform.localScale);
        }
        else
        {
            blackImage.color = variable;
        }
    }

    void Fade()
    {
        if (!reverse)
        {
            blackImage.color = Color.Lerp(blackImage.color, variable, Time.deltaTime * speed);

            if (blackImage.color.a <= variable.a + offset)
            {
                Debug.Log("Got come in");
                reverse = true;
            }
        }
        else
        {
            blackImage.color = Color.Lerp(blackImage.color, Original, Time.deltaTime * speed);

            if (blackImage.color.a >= Original.a - offset)
            {
                Debug.Log("Got come in");
                reverse = false;
            }
        }
    }
}
