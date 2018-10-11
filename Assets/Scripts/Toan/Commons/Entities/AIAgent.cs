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

        public float MinDetectionBoxLenght { get; protected set; }

#if UNITY_EDITOR
        [Header("Debug")]
        public bool drawGizmos = true;
#endif
        /// <summary>
        /// <params name = "BoundRange">2 * Radius</params>
        /// </summary>
        #region Properties
        public float MaxSpeed { get; protected set; }
        public float BoundRange { get; protected set; }
        public float NeighbourRadius { get; protected set; }
        public float DetectBoxLenght { get; protected set; }

        public bool IsSelected { get; protected set; }
        public bool IsReachedTarget { get; protected set; }

        // component properties
        public Rigidbody AgentRigid { get; protected set; }
        public MeshRenderer MeshRenderer { get; protected set; }

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
            MeshRenderer = GetComponentInChildren<MeshRenderer>();

            BoundRange = MeshRenderer.bounds.extents.magnitude;
            MinDetectionBoxLenght = BoundRange;
            NeighbourRadius = 8.0f;
            IsSelected = true;
            IsReachedTarget = false;

        }
        private void Start()
        {
            steerBh = Singleton.SteerBehavior;
            flockBh = Singleton.FlockBehavior;
            avoidanceBh = Singleton.ObstacleAvoidance;

            MaxSpeed = 12;
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
                    steering += avoidanceBh.GetObsAvoidanceForce(this, obstacles) * 1.5f;
                }
                aceleration = steering * Mathf.Pow(AgentRigid.mass, -1);
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
                transform.forward += AgentRigid.velocity * Mathf.Pow(AgentRigid.mass, -1);
                Debug.Log(AgentRigid.velocity.sqrMagnitude);
                // transform.forward = Vector3.RotateTowards(Heading, Velocity, 0.2f, 0.2f);
            }
            else
            {
                IsReachedTarget = true;
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
            if (MeshRenderer == null) MeshRenderer = GetComponentInChildren<MeshRenderer>();
            if (drawGizmos)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, BoundRange);

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
