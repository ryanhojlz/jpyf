using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderModi : MonoBehaviour
{
    public GameObject attackSlider;
    public GameObject hitArea;
    public GameObject arrow;
    public GameObject SuccessImage;
    public GameObject FailureImage;
    Vector3 arrowPosition;
    Vector3 hitAreaPosition;
    float travelDistance;

    //Used Variables
    Vector3 attackSliderPos;
    float ScaleX_attackSlider;
    float StartX;
    float EndX;

    Vector3 hitAreaPos;
    float ScaleX_hitArea;
    float StartX_hitArea;
    float EndX_hitArea;

    Vector3 arrowPos;
    float ScaleX_arrow;
    float StartX_arrow;
    float EndX_arrow;

    float ArrowSpeed = 200f;
    float speedMultiplier = 1f;

    int QTEsuccessCounter;

    bool Started = true;
    bool SuccessTrue = false;
    bool FailureTrue = false;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        //Zi Jun Test
        attackSliderPos = attackSlider.GetComponent<RectTransform>().localPosition;
        ScaleX_attackSlider = attackSlider.transform.localScale.x * 0.5f;

        ScaleX_hitArea = hitArea.transform.localScale.x * 0.5f;
        ScaleX_arrow = arrow.transform.localScale.x * 0.5f;

        StartX = attackSliderPos.x - ScaleX_attackSlider;
        EndX = attackSliderPos.x + ScaleX_attackSlider;

        arrowPos = arrow.transform.localPosition;
    }

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SuccessImage.SetActive(false);
        FailureImage.SetActive(false);
        //Arrow scale & HitArea need to be in update. Might change during runtime
        ScaleX_hitArea = hitArea.transform.localScale.x * 0.5f;
        ScaleX_arrow = arrow.transform.localScale.x * 0.5f;

        //Getting position of Arrow & hitArea
        arrowPos = arrow.transform.localPosition;
        hitAreaPos = hitArea.transform.localPosition;

        //Setting the start and end position if Arrow n HitArea
        StartX_arrow = arrowPos.x - ScaleX_arrow;
        EndX_arrow = arrowPos.x + ScaleX_arrow;

        StartX_hitArea = hitAreaPos.x - ScaleX_hitArea;
        EndX_hitArea = hitAreaPos.x + ScaleX_hitArea;

        if (CheckWithin() && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Success Hit");
            speedMultiplier += 0.5f;
            SuccessTrue = true;
            QTEsuccessCounter++;
            FullReset();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Fail Hit");
            QTEfailure();
            this.gameObject.SetActive(false);
            FailureTrue = true;
        }

        if (Input.GetKeyDown(KeyCode.R) || Started)
        {
            FullReset();
        }

        if (arrow.GetComponent<RectTransform>().localPosition.x + ScaleX_arrow > EndX)
        {
            QTEfailure();
            this.gameObject.SetActive(false);
            //FullReset();
        }
        else
        {
            arrow.GetComponent<RectTransform>().localPosition = new Vector3(arrow.GetComponent<RectTransform>().localPosition.x + ArrowSpeed * speedMultiplier * Time.deltaTime, arrow.GetComponent<RectTransform>().localPosition.y, arrow.GetComponent<RectTransform>().localPosition.z);
        }
        if (SuccessTrue)
        {
            SuccessImage.SetActive(true);
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                Debug.Log("success enter");
                SuccessTrue = false;
                SuccessImage.SetActive(false);
                timer = 0;
            }
        }
        if (FailureTrue)
        {
            FailureImage.SetActive(true);
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                Debug.Log("failure enter");
                FailureTrue = false;
                FailureImage.SetActive(false);
                timer = 0;
            }
        }

        if (QTEsuccessCounter == 3)
        {
            //ArrowSpeed = 0;
            this.gameObject.SetActive(false);
            QTEsuccess();
        }
    }

    void RandSpawnhitArea()
    {
        //float DistanceBTW = Mathf.Sqrt((StartX - EndX) * (StartX - EndX));//This will be the length(use this if things starts getting weird)
        float DistanceBTW = attackSlider.transform.localScale.x;//This will be the length
        float offsetValue = 90f;
        hitArea.GetComponent<RectTransform>().localPosition = new Vector3(StartX + offsetValue + Random.Range(ScaleX_hitArea, (DistanceBTW - ScaleX_hitArea - offsetValue)), attackSliderPos.y, attackSliderPos.z);
    }

    void ResetArrow()
    {
        arrow.GetComponent<RectTransform>().localPosition = new Vector3(StartX + arrow.transform.localScale.x * 0.5f, arrow.GetComponent<RectTransform>().localPosition.y, arrow.GetComponent<RectTransform>().localPosition.z);
    }

    bool CheckWithin()
    {
        if ((StartX_arrow > StartX_hitArea && StartX_arrow < EndX_hitArea)
            || (EndX_arrow > StartX_hitArea && EndX_arrow < EndX_hitArea))
        {
            return true;
        }
        return false;
    }

    void SetStart(bool start)
    {
        Started = start;
    }

    bool GetStart()
    {
        return Started;
    }

    void FullReset()
    {
        RandSpawnhitArea();
        ResetArrow();
        Started = false;
    }

    void ValueToReset()
    {
        speedMultiplier = 1f;
        QTEsuccessCounter = 0;
    }

    public void QTEStart()
    {
        this.gameObject.SetActive(true);
        FullReset();
        ValueToReset();
    }

    void QTEsuccess()
    {
        Debug.Log("QTE success");
    }

    void QTEfailure()
    {
        Debug.Log("QTE failure");
    }
}
