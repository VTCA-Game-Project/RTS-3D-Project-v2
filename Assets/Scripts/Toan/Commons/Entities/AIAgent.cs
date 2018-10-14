using UnityEngine;
using AI;
using Manager;
using Pattern;
using InterfaceCollection;
using EnumCollection;

namespace Common.Entity
{
    public class AIAgent : GameEntity, ISelectable
    {
        protected AnimState nextState = AnimState.Idle;

        protected Vector3 target;
        protected Vector3 steering;
        protected Vector3 aceleration;

        protected AIAgent[] neighbours;
        protected Obstacle[] obstacles;
        protected SteerBehavior steerBh;
        protected FlockBehavior flockBh;
        protected ObstacleAvoidance avoidanceBh;

        protected Pointer pointer;

        public float MinVelocity;
        public float separation;
        public float cohesion;
        public float alignment;
        public float seekingWeight;
        public float avoidanceWeight;

#if UNITY_EDITOR
        [Header("Debug")]
        public bool drawGizmos = true;
#endif

        #region Properties
        public Group Group { get; set; }
        public int HP { get; protected set; }        
        public float MaxSpeed { get; protected set; }
        public float Radius { get; protected set; }
        public float NeighbourRadius { get; protected set; }
        public float DetectBoxLenght { get; protected set; }
        public float MinDetectionBoxLenght { get; protected set; }

        public bool IsDead { get; protected set; }
        public bool IsSelected { get; protected set; }
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

        private void Awake()
        {
            gameObject.AddComponent<ClickOn>();
            pointer = FindObjectOfType<Pointer>();

            StoredManager.AddAgent(this);
            AgentRigid = GetComponent<Rigidbody>();
            SkinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

            Radius = SkinMeshRenderer.bounds.extents.x;
            MinDetectionBoxLenght = Radius;
            NeighbourRadius = 5.0f;
            IsSelected = false;
            IsReachedTarget = true;
            IsDead = false;

        }
        private void Start()
        {
            steerBh = Singleton.SteerBehavior;
            flockBh = Singleton.FlockBehavior;
            avoidanceBh = Singleton.ObstacleAvoidance;

            MaxSpeed = 5;
        }
        private void FixedUpdate()
        {
            if (IsDead) return;

            steering = Vector3.zero;
            aceleration = Vector3.zero;
            if (!IsReachedTarget)
            {
                steering += steerBh.Seek(this, target) * seekingWeight;

                neighbours = StoredManager.GetNeighbours(this);
                steering += flockBh.Separation(this, neighbours) * separation;
                steering += flockBh.Alignment(this, neighbours) * alignment;
                steering += flockBh.Cohesion(this, neighbours) * cohesion;

            }
            // obstacle avoidance test
            if (AgentRigid.velocity.sqrMagnitude > MinVelocity)
            {
                DetectBoxLenght = avoidanceBh.CalculateDetectBoxLenght(this);
                obstacles = StoredManager.GetObstacle(this);
                steering += avoidanceBh.GetObsAvoidanceForce(this, obstacles) * avoidanceWeight;
            }
            aceleration = steering / AgentRigid.mass;
            AgentRigid.velocity = TruncateVel(AgentRigid.velocity + aceleration);
            RotateAgent();


#if UNITY_EDITOR
            // Debug.Log("steer: " + steering + " velocity: " + rigid.velocity + " max speed: " + maxSpeed * rigid.velocity.normalized);
#endif
        }

        private void RotateAgent()
        {
            if (AgentRigid.velocity.sqrMagnitude > MinVelocity)
            {
                transform.forward += AgentRigid.velocity / AgentRigid.mass;
            }
            else
            {
                IsReachedTarget = true;
                AgentRigid.velocity = Vector3.zero;
            }
        }
        private Vector3 TruncateVel(Vector3 desireVel)
        {
            return desireVel.sqrMagnitude > (MaxSpeed * MaxSpeed) ?
                            desireVel.normalized * MaxSpeed : desireVel;
        }
        public void Select() { IsSelected = true; }
        public void UnSelect() { IsSelected = false; }
        public void Action() { MoveToTarget(); }
        public void MoveToTarget()
        {
            if (IsSelected)
            {
                IsReachedTarget = false;
                target = pointer.Position;
            }
        }
        public override void Dead()
        {
            IsDead = true;
            Destroy(gameObject,2);
        }
        public override void ReceiveDamage(int damage)
        {
            HP -= damage;
            if(HP <= 0)
            {
                HP = 0;
                Dead();
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
