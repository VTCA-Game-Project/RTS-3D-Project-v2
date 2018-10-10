
using EnumCollection;
using Common;
using InterfaceCollection;

namespace Common.Building
{
    public class NearDefender : Construct, IAttackable, IDetectEnemy
    {
        protected override void Start()
        {
            Id = ConstructId.NearDefender;
            IsUsePower = true;
            IsActive = true;
            ConsumePower = 0;
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }

        // inferface implementation
        public void Attack()
        {

        }

        public void DetectEnemy()
        {

        }
    }
}
