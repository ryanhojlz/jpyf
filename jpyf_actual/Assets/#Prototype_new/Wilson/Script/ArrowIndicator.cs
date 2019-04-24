using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicator : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Vector3 pointing;
    [SerializeField]
    Transform Arrow;
    //[SerializeField] private Camera uiCamera;
    //[SerializeField] private Sprite arrowSprite;
    //[SerializeField] private Sprite crossSprite;
    //private Vector3 targetPosition;
    //private RectTransform pointerRectTransform;
    //private Image pointerImage;

    // Use this for initialization
    void Start()
    {
        //targetPosition = new Vector3(200, 45);
        //Vector3 toPosition = targetPosition;
    }
    //private void Awake()
    //{
    //    targetPosition = new Vector3(200, 45);
    //    pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    //}

    // Update is called once per frame
    void Update()
    {
        Arrows();
        //    Vector3 toPosition = targetPosition;
        //    Vector3 fromPosition = Camera.main.transform.position;
        //    fromPosition.z = 0f;
        //    Vector3 dir = (toPosition - fromPosition).normalized;
        //    float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        //    pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        //    float borderSize = 100f;
        //    Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        //    bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height - borderSize;

        //    if(isOffScreen)
        //    {
        //        Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
        //        if (cappedTargetScreenPosition.x <= 0)
        //            cappedTargetScreenPosition.x = 0f;
        //        if (cappedTargetScreenPosition.x >= Screen.width)
        //            cappedTargetScreenPosition.x = Screen.width;
        //        if (cappedTargetScreenPosition.y <= 0)
        //            cappedTargetScreenPosition.y = 0f;
        //        if (cappedTargetScreenPosition.x >= Screen.height)
        //            cappedTargetScreenPosition.y = Screen.height;

        //        Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
        //        pointerRectTransform.position = pointerWorldPosition;
        //        pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        //    }
        //    else
        //    {
        //        Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
        //        pointerRectTransform.position = pointerWorldPosition;
        //        pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        //        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
        //    }
        //}

        //private void RotatePointerTowardsTargetPosition()
        //{
        //    Vector3 toPosition = targetPosition;
        //    Vector3 fromPosition = Camera.main.transform.position;
        //    fromPosition.z = 0f;
        //    Vector3 dir = (toPosition - fromPosition).normalized;
        //    float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        //    pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
        //}

        //public void Hide()
        //{
        //    gameObject.SetActive(false);
        //}

        //public void Show(Vector3 targetPosition)
        //{
        //    gameObject.SetActive(true);
        //    this.targetPosition = targetPosition;
        //}
    }
    void Arrows()
    {
        Vector3 dir = Camera.main.WorldToScreenPoint(target.transform.position);
        pointing.z = Mathf.Atan2((Arrow.transform.position.y - dir.y), (Arrow.transform.position.x - dir.x)) * Mathf.Rad2Deg - 90;
        pointing += new Vector3(0, 0, 180);
        Arrow.transform.rotation = Quaternion.Euler(pointing);
    }
}
