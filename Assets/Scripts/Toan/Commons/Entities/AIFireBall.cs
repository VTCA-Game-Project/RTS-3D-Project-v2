using UnityEngine;

namespace Common.Entity
{
    public class AIFireBall : GameEntity
    {
        [SerializeField]
        private AnimationCurve heightSampler;
        public int Damage { get; set; }
        public GameEntity Target { get; set; }

        public override bool IsDead { get; protected set; }

        private void FixedUpdate()
        {
            if (IsDead) return;
            if (transform.position.y <= 0)
            {
                Dead();
            }

            Vector3 pos = Position;
            Debug.Log(Position.magnitude / Target.Position.magnitude);
            float hieght = heightSampler.Evaluate((Position.magnitude/Target.Position.magnitude));
            pos = Vector3.MoveTowards(pos, Target.Position, Time.deltaTime *1);

            pos.y = hieght;
            transform.position = pos;
        }


        public void Init(GameEntity target,int damage)
        {
            Target = target;
            Damage = damage;
        }

        public override void Dead()
        {
            IsDead = true;
            if (Target != null && !Target.IsDead)
            {
                Target.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
}