using Common.Entity;
using EnumCollection;
using InterfaceCollection;

namespace Common.Building
{
    public class Defender : Construct, IDetectEnemy, IAttackable
    {

        private AIAgent target;
        private bool isDetectedEnemy;

        public int Damage;
        public float AttackRange;
        protected override void Start()
        {
            Id = ConstructId.Defender;
            base.Start();
        }
        protected override void Update()
        {
            if (isDetectedEnemy)
            {
                Attack();
            }
        }
        public void DetectEnemy()
        {
            if (target != null)
            {
                if (!target.IsDead) return;
                target = null;
                // find agent inside attack range 
            }
        }

        public void Attack()
        {
            // attack target
            // before attack check either enemy out of attack range or not
            if ((target.Position - Position).sqrMagnitude < AttackRange * AttackRange)
            {
                // fire
                target.TakeDamage(Damage);
            }
            else
            {
                target = null;
                isDetectedEnemy = false;
            }
        }
    }
}
