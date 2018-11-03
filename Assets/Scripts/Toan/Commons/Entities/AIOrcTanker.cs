﻿using UnityEngine;

namespace Common.Entity
{
    public class AIOrcTanker : AIAgent
    {
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Start()
        {
            base.Start();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        public override void Attack()
        {
            base.Attack();
            if (TargetEntity != null)
            {
                TargetEntity.TakeDamage(Damage);
            }
        }
    }
}