using UnityEngine;

namespace Common.Entity
{
    public class AIFireBall : GameEntity
    {
        [SerializeField]
        private AnimationCurve heightSampler;
        private Vector3 origin;
        private const float MAX_HEIGHT = 4.5f;

        public float Speed;
        public int Damage           { get; set; }
        public GameEntity Target    { get; set; }
        public override bool IsDead { get; protected set; }

        private void Awake()
        {
            origin = Position;
        }
        private void Update()
        {
            if (IsDead) return;
            if (transform.position.y <= 0)
            {
                Dead();
            }

            float sampler = (Position - origin).magnitude / (origin - Target.Position).magnitude;
            Vector3 position = Position;
            position = Vector3.MoveTowards(position, Target.Position, Time.deltaTime * Speed);
            position.y = heightSampler.Evaluate(sampler) * MAX_HEIGHT;
            transform.position = position;
        }


        public void Init(GameEntity target, int damage)
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