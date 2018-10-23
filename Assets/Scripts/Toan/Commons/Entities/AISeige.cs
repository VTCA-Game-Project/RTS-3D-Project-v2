using UnityEngine;

namespace Common.Entity
{
    public class AISeige : AIAgent
    {
        public Rigidbody FireBall;
        public Transform LauncherPoint;

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
            Rigidbody copyBall = Instantiate(FireBall, LauncherPoint.position, LauncherPoint.rotation);
            copyBall.gameObject.SetActive(true);
            copyBall.AddRelativeForce(0, 0, 300);

            base.Attack();
        }

        public override void Dead()
        {
            base.Dead();
        }
    }

}