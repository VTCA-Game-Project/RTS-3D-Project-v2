﻿using UnityEngine;

namespace InterfaceCollection
{
    // interface for properties
    interface IGameEntiy
    {
        Vector3 Position { get; }
        Vector3 Heading { get; }
        Vector3 Velocity { get; }
    }

    // interface for behavior
    interface IAttackable
    {
        void Attack();
    }
    interface IDetectEnemy
    {
        void DetectEnemy();
    }
    interface IProduce
    {
        GameObject Produce(System.Enum type);
    }
    // 
}
