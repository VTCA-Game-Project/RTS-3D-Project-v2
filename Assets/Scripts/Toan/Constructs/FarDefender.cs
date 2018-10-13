using EnumCollection;
using InterfaceCollection;

namespace Common.Building
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

        // interface implementation
        public void Attack()
        {

        }

        public void DetectEnemy()
        {

        }
    }
}
