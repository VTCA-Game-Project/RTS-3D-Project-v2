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
            Rigidbody copyBall = Instantiate(FireBall, LauncherPoint.position, Quaternion.identity);
            copyBall.gameObject.SetActive(true);
            base.Attack();
        }
    }

}