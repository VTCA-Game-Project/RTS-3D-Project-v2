﻿using EnumCollection;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameStatus : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GlobalGameStatus.Instance.Status != GameStatus.Playing) return;
    }
}
