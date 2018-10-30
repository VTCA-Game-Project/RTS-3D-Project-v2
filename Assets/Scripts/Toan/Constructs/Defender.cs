using Common.Entity;
using EnumCollection;
using InterfaceCollection;
using Manager;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Building
{
    public class Defender : Construct, IDetectEnemy, IAttackable
    {
        public float DelayAttack;
        public float ShootForce;
        public Transform LauncherPoint;
        public Rigidbody Arrow;
        public GameEntity TargetEntity { get; set; }
        public Group PlayerGroup { get; set; }

        private bool isDetectedEnemy;

        public int Damage;
        public float AttackRange;

        private float attackCounter;

        protected override void Start()
        {
            Id = ConstructId.Defender;
            base.Start();
            // test
            Build();
        }
        protected override void Update()
        {
            if (IsDead) return;
            if (TargetEntity == null)
            {
                DetectEnemy();
            }
            if (isDetectedEnemy)
            {
                attackCounter += Time.deltaTime;
                if (attackCounter >= DelayAttack)
                {
                    Attack();
                    attackCounter = 0;
                }
            }
        }
        public void DetectEnemy()
        {
            // find agent inside attack range 

            if (TargetEntity != null && TargetEntity.IsDead)
            {
                TargetEntity = null;
                return;
            }
            if (TargetEntity != null) return;

            List<Player> players = UpdateGameStatus.Instance.Players;
            List<AIAgent> enemies;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Group != PlayerGroup)
                {
                    enemies = players[i].Agents;
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        if (enemies[j] != null && !enemies[j].IsDead && Vector3.Distance(enemies[j].Position, Position) <= AttackRange)
                        {
                            TargetEntity = enemies[j];
                            isDetectedEnemy = true;
                            break;
                        }
                    }
                }
                if (TargetEntity != null) break;
            }
        }

        public void Attack()
        {
            // attack target
            // before attack check either enemy out of attack range or not
            if ((TargetEntity.Position - Position).sqrMagnitude < AttackRange * AttackRange)
            {
                // fire
                Fire();
            }
            else
            {
                TargetEntity = null;
                isDetectedEnemy = false;
            }
        }

        private void Fire()
        {
            if (TargetEntity != null && !TargetEntity.IsDead)
            {
                Rigidbody copyArrow = Instantiate(Arrow, LauncherPoint.position, transform.rotation);
                copyArrow.gameObject.SetActive(true);
                copyArrow.transform.LookAt(TargetEntity.transform);
                copyArrow.transform.Rotate(-8, 0, 0);
                copyArrow.AddForce(copyArrow.transform.forward * ShootForce);
                copyArrow.GetComponent<AIArrow>().Init(TargetEntity, Damage);
            }
        }
 
    }
}
