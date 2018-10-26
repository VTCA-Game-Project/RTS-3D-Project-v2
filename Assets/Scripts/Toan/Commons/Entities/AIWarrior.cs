using UnityEngine;

namespace Common.Entity
{
    public class AIWarrior : AIAgent
    {

       public HPBar heath;
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
            heath.SetValue((float)HP / MaxHP);
            base.FixedUpdate();
        }
        public override void Attack()
        {
            base.Attack();
        }
    }
}