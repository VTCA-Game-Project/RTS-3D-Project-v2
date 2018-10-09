using Common;
using EnumCollection;
using Manager;
using UnityEngine;

namespace Building
{
    public class Refinery : Construct
    {
        protected override void Start()
        {
            Id = ConstructId.Refinery;
            ConsumePower = 0;
            IsUsePower = false;
            IsActive = true;
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }

        public void ReceiveGold(float gold)
        {
            GlobalGameStatus.ReceiveGold(gold);
        }

        public override void Produce()
        {
            return;
        }
    }
}