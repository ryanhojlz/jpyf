﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingTree : Object_Breaking
{
    private void Start()
    {
        m_timelimit = 5f;
        m_maxSpamPoint = 100f;
        m_powerPerHit = 5f;
    }
}
