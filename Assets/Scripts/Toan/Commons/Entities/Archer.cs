using Common.Entity;
using UnityEngine;

namespace Common.Entity
{
    public class Archer : AIAgent
    {
        public Rigidbody Arrow;
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
            
        }
    }
}