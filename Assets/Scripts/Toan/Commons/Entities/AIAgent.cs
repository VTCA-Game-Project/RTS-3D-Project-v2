using UnityEngine;
using AI;
using Manager;
using Pattern;

namespace Common.Entity
{
    public class AIAgent : GameEntity
    {
        protected Vector3 steering;
        protected Vector3 aceleration;

        protected AIAgent[] neighbours;
        protected Obstacle[] obstacles;
        protected SteerBehavior steerBh;
        protected FlockBehavior flockBh;
        protected ObstacleAvoidance avoidanceBh;


        public Pointer target;
        public float separation;
        public float cohesion;
        public float alignment;
        public float seekingWeight;
        public float avoidanceWeight;

        public float MinDetectionBoxLenght { get; protected set; }


        protected AnimationStateCtrl anims;
#if UNITY_EDITOR
        [Header("Debug")]
        public bool drawGizmos = true;
#endif



        #region Properties
        public float MaxSpeed { get; protected set; }
        public float Radius { get; protected set; }
        public float NeighbourRadius { get; protected set; }
        public float DetectBoxLenght { get; protected set; }

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
            // gameObject.AddComponent<ClickOn>();
            target = FindObjectOfType<Pointer>();

            StoredManager.AddAgent(this);
            AgentRigid = GetComponent<Rigidbody>();
            SkinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

            Radius = SkinMeshRenderer.bounds.extents.x;
            MinDetectionBoxLenght = Radius;
            NeighbourRadius = 5.0f;
            IsSelected = true;
            IsReachedTarget = false;

            anims = GetComponent<AnimationStateCtrl>();
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
            if (IsSelected)
            {
                steering = Vector3.zero;
                aceleration = Vector3.zero;
                //if (!isReachedTarget)
                //{
                steering += steerBh.Seek(this, target.Position) * seekingWeight;
                //}
                neighbours = StoredManager.GetNeighbours(this);
                steering += flockBh.Separation(this, neighbours) * separation;
                steering += flockBh.Alignment(this, neighbours) * alignment;
                steering += flockBh.Cohesion(this, neighbours) * cohesion;


                // obstacle avoidance test
                if (AgentRigid.velocity.sqrMagnitude > 0.01f)
                {
                    DetectBoxLenght = avoidanceBh.CalculateDetectBoxLenght(this);
                    obstacles = StoredManager.GetObstacle(this);
                    steering += avoidanceBh.GetObsAvoidanceForce(this, obstacles) * avoidanceWeight;
                }
                aceleration = steering / AgentRigid.mass;
                AgentRigid.velocity = TruncateVel(AgentRigid.velocity + aceleration);
                RotateAgent();

            }
#if UNITY_EDITOR
            // Debug.Log("steer: " + steering + " velocity: " + rigid.velocity + " max speed: " + maxSpeed * rigid.velocity.normalized);
#endif
        }

        private void RotateAgent()
        {
            if (AgentRigid.velocity.sqrMagnitude > (1.0f))
            {
                transform.forward += AgentRigid.velocity / AgentRigid.mass;
                anims.Play("Run");
            }
            else
            {
                IsReachedTarget = true;
                anims.Play("Idle");
            }
        }
        private Vector3 TruncateVel(Vector3 desireVel)
        {
            return desireVel.sqrMagnitude > (MaxSpeed * MaxSpeed) ?
                            desireVel.normalized * MaxSpeed : desireVel;
        }
        public void Select() { IsSelected = true; }
        public void UnSelect() { IsSelected = false; }
        public void MoveToTarget()
        {
            IsReachedTarget = false;
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
