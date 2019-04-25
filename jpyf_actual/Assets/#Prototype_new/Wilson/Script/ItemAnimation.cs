using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    public AnimationCurve normalCurve;
    float bounceTimer;
    //public AnimationCurve lighterCurve;
    Vector3 Bounce;
    Vector3 lighterBounce;

    void Awake()
    {
      
    }

    void Update()
    {
        bounceTimer += Time.deltaTime;
        Bounce.x = transform.position.x;
        Bounce.y = normalCurve.Evaluate((Time.time % normalCurve.length));
        Bounce.z = transform.position.z;
        //lighterBounce.x = transform.position.x;
        //lighterBounce.y = lighterCurve.Evaluate((Time.time % lighterCurve.length));
        //lighterBounce.z = transform.position.z;
        this.gameObject.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        //cube2.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
        this.gameObject.transform.position = Bounce;
        //if(bounceTimer > 5)
        //    this.gameObject.transform.position = lighterBounce;
        Debug.Log(this.gameObject.transform.position);
    }
}
