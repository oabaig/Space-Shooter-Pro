﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : laser
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        _isEnemyLaser = true;
    }
}
