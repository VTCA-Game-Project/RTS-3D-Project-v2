﻿using UnityEngine;
using AI;
using Manager;
using Pattern;
using InterfaceCollection;
using EnumCollection;
using RTS_ScriptableObject;
using Animation;
using System.Collections.Generic;

namespace Common.Entity
{
    public abstract class AIAgent : GameEntity, ISelectable,IAttackable,IDetectEnemy
    {
        protected bool isReachedTarget;        
        protected Vector3 target;
        protected Vector3 steering;
        protected Vector3 aceleration;
        
        protected AIAgent[] neighbours;
        protected Obstacle[] obstacles;
        protected SteerBehavior steerBh;
        protected FlockBehavior flockBh;
        protected ObstacleAvoidance avoidanceBh;
        protected BaseAnimation anims;
        protected Pointer pointer;

        public int Damage;
        public Player Owner;/*{ get; set; }*/
        public GameEntity TargetEntity { get; protected set; }
        public bool OnObsAvoidance      { get; set; }
        public float AttackRange        { get; protected set; }
        public float MinVelocity        { get; protected set; }
        public float Separation         { get; protected set; }
        public float Cohesion           { get; protected set; }
        public float Alignment          { get; protected set; }
        public float SeekingWeight      { get; protected set; }
        public float AvoidanceWeight    { get; protected set; }
        public TargetType TargetType    { get; protected set; }
        public Group PlayerGroup        { get; protected set; }

        public AgentOffset offset;
#if UNITY_EDITOR
        [Header("Debug")]
        public bool drawGizmos = true;
#endif

        #region Properties
        public int HP { get; protected set; }
        public float MaxSpeed { get; protected set; }
        public float Radius { get; protected set; }
        public float NeighbourRadius { get; protected set; }
        public float DetectBoxLenght { get; protected set; }
        public float MinDetectionBoxLenght { get; protected set; }

        public override bool IsDead
        {
            get;
            protected set;
        }
        public bool IsSelected  { get; protected set; }
        public bool IsReachedTarget { get; protected set; }
        // component properties
        public Rigidbody AgentRigid { get; protected set; }
        public SkinnedMeshRenderer SkinMeshRenderer { get; protected set; }

        // override interface
        public override Vector3 Velocity
        {
            // using projection
            get { return Vector3.ProjectOnPlane(AgentRigid.velocity, Vector3.up); }
        }
        #endregion

        protected virtual void Awake()
        {
            gameObject.AddComponent<ClickOn>();
            pointer = FindObjectOfType<Pointer>();
            anims = GetComponent<BaseAnimation>();
            AgentRigid = GetComponent<Rigidbody>();
            SkinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
        protected virtual void Start()
        {
            Damage = 2;
            HP = 2;
            Owner.AddAgent(this);
            PlayerGroup = Owner.Group;

            steerBh = Singleton.SteerBehavior;
            flockBh = Singleton.FlockBehavior;
            avoidanceBh = Singleton.ObstacleAvoidance;
            InitOffset();
        }
        protected virtual void FixedUpdate()
        {
            if (IsDead) return;
            DetectEnemy();
            steering = Vector3.zero;
            aceleration = Vector3.zero;
            if (!IsReachedTarget)
            {
                steering += steerBh.Seek(this, target) * SeekingWeight;
                if (OnObsAvoidance)
                {
                    neighbours = Owner.GetNeighbours(this);
                    steering += flockBh.Separation(this, neighbours) * Separation;
                    steering += flockBh.Alignment(this, neighbours) * Alignment;
                    steering += flockBh.Cohesion(this, neighbours) * Cohesion;
                }
            }
            // obstacle avoidance test
            if (AgentRigid.velocity.sqrMagnitude > MinVelocity)
            {
                DetectBoxLenght = avoidanceBh.CalculateDetectBoxLenght(this);
                obstacles = StoredManager.GetObstacle(this);
                steering += avoidanceBh.GetObsAvoidanceForce(this, obstacles) * AvoidanceWeight;
            }
            aceleration = steering / AgentRigid.mass;
            AgentRigid.velocity = TruncateVel(AgentRigid.velocity + aceleration);

            RotateAgent();            
            if (!IsReachedTarget)
            {
                IsReachedTarget = CheckReachedTarget();
                if (IsReachedTarget)
                {
                    AgentRigid.velocity = Vector3.zero;
                }
            }


#if UNITY_EDITOR
            // Debug.Log("steer: " + steering + " velocity: " + rigid.velocity + " max speed: " + maxSpeed * rigid.velocity.normalized);
#endif
        }
        protected void MoveToTarget()
        {
            if (IsSelected)
            {
                IsReachedTarget = false;
                target = pointer.Position;
                TargetType = pointer.TargetType;
                TargetEntity = pointer.TargetEntity;
            }
        }
        protected Vector3 TruncateVel(Vector3 desireVel)
        {
            return desireVel.sqrMagnitude > (MaxSpeed * MaxSpeed) ?
                            desireVel.normalized * MaxSpeed : desireVel;
        }
        protected void RotateAgent()
        {
            if (AgentRigid.velocity.sqrMagnitude > MinVelocity)
            {
                transform.forward += (AgentRigid.velocity / AgentRigid.mass);
            }
            else if (TargetType == TargetType.NPC)
            {
                transform.LookAt(target);
            }
        }
        protected bool CheckReachedTarget()
        {
            //targetType = pointer.TargetType;
            switch (TargetType)
            {
                case TargetType.Place:
                    if (AgentRigid.velocity.sqrMagnitude <= MinVelocity)
                        return true;
                    break;
                case TargetType.Construct:
                    return true;
                case TargetType.NPC:
                    return (Vector3.Distance(Position, target) <= AttackRange);
                default:
                    return true;
            }
            return false;
        }

        public void SetTarget(TargetType type,Vector3 position)
        {
            isReachedTarget = false;
            TargetType = type;
            target = position;
        }
        public void OffObsAvoidance() { OnObsAvoidance = false; }
        public override void Dead()
        {
            IsDead = true;
            Destroy(gameObject, 2);
        }
        public override void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
                Dead();
            }
        }
        protected virtual void InitOffset()
        {
            NeighbourRadius = offset.NeighboursRadius;
            Separation = offset.Separation;
            Cohesion = offset.Cohesion;
            Alignment = offset.Alignment;
            SeekingWeight = offset.Seeking;
            AvoidanceWeight = offset.ObstacleAvoidance;
            MaxSpeed = offset.MaxSpeed;
            AttackRange = offset.AttackRadius;

            IsDead = false;
            IsSelected = false;
            IsReachedTarget = true;
            OnObsAvoidance = true;
            MinDetectionBoxLenght = Radius;
            Radius = SkinMeshRenderer.bounds.extents.x;
        }
        // INTERFACE
        public void Select() { IsSelected = true; }
        public void UnSelect() { IsSelected = false; }
        public virtual void Action() { MoveToTarget(); }
        public virtual void Attack()
        {
            if (TargetEntity != null)
            {
                TargetEntity.TakeDamage(Damage);
                if (TargetEntity.IsDead)
                {
                    TargetEntity = null;
                    TargetType = TargetType.None;
                }
            }
        }
        public void DetectEnemy()
        {
            if (TargetEntity != null) return;
            List<Player> players = UpdateGameStatus.Instance.Players;
            List<AIAgent> enemies;
            for (int i = 0; i < players.Count; i++)
            {
                if(players[i].Group != PlayerGroup)
                {
                    enemies = players[i].Agents;
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        if(enemies[j] != null && !enemies[j].IsDead && Vector3.Distance(enemies[j].Position,Position) <= AttackRange)
                        {
                            TargetEntity = enemies[j];
                            TargetType = TargetType.NPC;
                            target = enemies[j].Position;
                            break;
                        }
                    }
                }
                if (TargetEntity != null) break;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (SkinMeshRenderer == null) SkinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            if (drawGizmos)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, Radius);

                if (AgentRigid != null)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawRay(transform.position, transform.forward * DetectBoxLenght);
                }
            }
        }
#endif
    }
}
