﻿using Common;
using EnumCollection;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : Construct
{
    public int PowerVolume { get; protected set; }

    protected override void Start()
    {
        Id = ConstructId.Power;
        PowerVolume = 10;
        ConsumePower = 0;
        IsActive = true;
        IsUsePower = false;
        base.Start();
    }

    public override void Build()
    {
        GlobalGameStatus.PowerBuilded(this);
        base.Build();
    }

    public override void DestroyConstruct()
    {
        GlobalGameStatus.PowerBuildDestroyed(this);
        base.DestroyConstruct();
    }

    public override void Produce()
    {
        return;
    }
}
