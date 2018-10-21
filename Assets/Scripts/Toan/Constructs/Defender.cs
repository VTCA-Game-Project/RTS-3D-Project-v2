using Common.Entity;
using EnumCollection;
using InterfaceCollection;
using UnityEngine;

namespace Common.Building
{
    public class Defender : Construct, IDetectEnemy, IAttackable
    {
        public GameObject Arrow;
        public AIAgent target;
        private bool isDetectedEnemy;

        public int Damage;
        public float AttackRange;
        protected override void Start()
        {
            Id = ConstructId.Defender;
           // base.Start();
        }
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                Attack();
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
                Fire();
            }
            else
            {
                target = null;
                isDetectedEnemy = false;
            }
        }

        private void Fire()
        {
            GameObject arrow = Instantiate(Arrow, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            arrow.transform.LookAt(target.transform);

            arrow.SetActive(true);
            Rigidbody rigid = arrow.GetComponent<Rigidbody>();
            Vector3 dir = target.transform.position - arrow.transform.position;
            rigid.AddForce(dir * 20);
            Destroy(arrow, 2);
        }
    }
}
