using Common.Entity;
using UnityEngine;

namespace Common.Entity
{
    public class AIArcher : AIAgent
    {
        public Rigidbody Arrow;
        public Transform LauncherPoint;
        public float ShootForce;
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
            Rigidbody copyArrow = Instantiate(Arrow, LauncherPoint.position,transform.rotation);
            copyArrow.gameObject.SetActive(true);
            copyArrow.AddForce(transform.forward * ShootForce);
            Destroy(copyArrow.gameObject, ShootForce / AttackRange);
            base.Attack();
           
        }
    }
}