using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image redImage;
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
        Original = redImage.color;
        Original.a = 0.35f;
        variable = redImage.color;
        variable.a = 0f;

        ForPlayer = Stats_ResourceScript.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (((float)ForPlayer.m_P2_hp / (float)ForPlayer.m_P2_hpCap <= 0.25f) && ((float)ForPlayer.m_P2_hp / (float)ForPlayer.m_P2_hpCap >= 0.21f))
        {
            Fade();
            scale.Set(15, 15, 1);
            redImage.rectTransform.localScale = scale;
            Debug.Log(redImage.rectTransform.localScale);
        }
        else if (((float)ForPlayer.m_P2_hp / (float)ForPlayer.m_P2_hpCap <= 0.20f) && ((float)ForPlayer.m_P2_hp / (float)ForPlayer.m_P2_hpCap >= 0.11f))
        {
            Fade();
            scale.Set(13, 13, 1);
            redImage.rectTransform.localScale = scale;
            Debug.Log(redImage.rectTransform.localScale);
        }
        else if (((float)ForPlayer.m_P2_hp / (float)ForPlayer.m_P2_hpCap <= 0.10f) && ((float)ForPlayer.m_P2_hp / (float)ForPlayer.m_P2_hpCap >= 0f))
        {
            Fade();
            scale.Set(10, 10, 1);
            redImage.rectTransform.localScale = scale;
            Debug.Log(redImage.rectTransform.localScale);
        }
        else
        {
            redImage.color = variable;
        }
    }

    void Fade()
    {
        if (!reverse)
        {
            redImage.color = Color.Lerp(redImage.color, variable, Time.deltaTime * speed);

            if (redImage.color.a <= variable.a + offset)
            {
                Debug.Log("Got come in");
                reverse = true;
            }
        }
        else
        {

            redImage.color = Color.Lerp(redImage.color, Original, Time.deltaTime * speed);

            if (redImage.color.a >= Original.a - offset)
            {
                Debug.Log("Got come in");
                reverse = false;
            }
        }
    }
}
