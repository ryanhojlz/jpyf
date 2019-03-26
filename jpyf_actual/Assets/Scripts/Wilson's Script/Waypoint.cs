using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
    protected float DrawRadius = 1.0f;

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DrawRadius);
    }
}