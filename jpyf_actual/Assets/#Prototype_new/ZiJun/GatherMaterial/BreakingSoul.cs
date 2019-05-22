﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingSoul : Object_Breaking
{
    private void Awake()
    {
        m_timelimit = 5f;
        m_maxSpamPoint = 100f;
        m_powerPerHit = 5f;
    }

    protected override void SelfStart()
    {
        objectAudio = AudioManager.Instance.BreakingLantern;
    }
}
