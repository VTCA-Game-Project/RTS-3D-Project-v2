
using EnumCollection;
using Common;
using InterfaceCollection;

namespace Building
{
    public class FarDefender : Construct, IAttackable, IDetectEnemy
    {
        protected override void Start()
        {
            Id = ConstructId.FarDefender;
            IsUsePower = true;
            IsActive = true;
            ConsumePower = 10;
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }
        public override void Produce()
        {
            if (IsActive)
            {
                DetectEnemy();
            }
        }


        public void Attack()
        {

        }

        public void DetectEnemy()
        {

        }
    }
}
