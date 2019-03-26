using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBoi : GrabbableObject {

    public override void OnGrab(MoveController currentController)
    {
        base.OnGrab(currentController);
        //Debug.Log("GRabbed boi");
    }

}
