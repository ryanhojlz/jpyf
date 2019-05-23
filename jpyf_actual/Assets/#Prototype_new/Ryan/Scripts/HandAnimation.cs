using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    MoveController m_parent = null;
    bool b_anim_setGrab = false;
    Animator animobj = null;
	// Use this for initialization
	void Start ()
    {
        m_parent = transform.parent.GetComponent<MoveController>();
        animobj = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_parent.ReturnIsGrabbing())
        {
            b_anim_setGrab = true;
        }
        else if (!m_parent.ReturnIsGrabbing())
        {
            b_anim_setGrab = false;
        }

        animobj.SetBool("Anim_Grabbing", b_anim_setGrab);
	}
}
