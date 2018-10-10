using UnityEngine;
using AI;
using Manager;
using Utils;

namespace Common
{
    public class AIAgent : GameEntity
    {
        protected SteerBehavior steerBh;
        protected FlockBehavior flockBh;
        protected ObstacleAvoidance avoidanceBh;

        protected Vector3 steering;
        protected Vector3 aceleration;
        protected bool isSelected = false;
        protected bool isReachedTarget = true;       
        protected AIAgent[] neighbours;
        protected Obstacle[] obstacles;

        public Rigidbody AgentRigid { get; protected set; }
        public MeshRenderer meshRenderer { get; protected set; }

        public Pointer target;
        public float separation;
        public float cohesion;
        public float alignment;
        public float maxSpeed;
        //public int index;
        [Header("Obstacle avoidance")]
        public float DetectBoxLenght;
        public float MinDetectionBoxLenght;

#if UNITY_EDITOR
        [Header("Debug")]
        public bool drawGizmos = true;
#endif

        #region Properties
        public float BoundRadius { get; protected set; }
        public float NeighbourRadius { get; protected set; }
        public override Vector3 Velocity
        {
            // using projection
            get
            {
                return Vector3.ProjectOnPlane(AgentRigid.velocity, Vector3.up);
            }
        }
        public float MaxSpeed
        {
            get { return maxSpeed; }
            protected set { maxSpeed = value; }
        }
        public bool IsSelected
        {
            get { return isSelected; }
        }
        public bool IsReachedTarget
        {
            get { return isReachedTarget; }
            protected set { isReachedTarget = value; }
        }
        #region Old Properties
        //public Vector3 Position
        //{
        //    // using projection
        //    get
        //    {
        //        return Vector3.ProjectOnPlane(transform.position, Vector3.up);                
        //    }
        //}
        //public Vector3 Heading
        //{
        //    // using projection
        //    get
        //    {
        //        return Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        //    }
        //}
        #endregion
        #endregion

        private void Awake()
        {
            gameObject.AddComponent<ClickOn>();


            StoredManager.AddAgent(this);
            AgentRigid = GetComponent<Rigidbody>();
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            target = FindObjectOfType<Pointer>();
            BoundRadius = 2;
            NeighbourRadius = 10.0f;
        }
        private void Start()
        {
            steerBh = Singleton.SteerBehavior;
            flockBh = Singleton.FlockBehavior;
            avoidanceBh = new ObstacleAvoidance();

            isSelected = true;
        }
        private void FixedUpdate()
        {
            if (IsSelected)
            {
                steering = Vector3.zero;
                aceleration = Vector3.zero;
                //if (!isReachedTarget)
                //{
                steering += steerBh.Seek(this, target.Position);
                //}
                neighbours = StoredManager.GetNeighbours(this);
                steering += flockBh.Separation(this, neighbours) * separation;
                steering += flockBh.Alignment(this, neighbours) * alignment;
                steering += flockBh.Cohesion(this, neighbours) * cohesion;


                // obstacle avoidance test
                obstacles = StoredManager.GetObstacle(this);
                DetectBoxLenght = avoidanceBh.CalculateDetectBoxLenght(this);
                steering += avoidanceBh.GetObsAvoidanceForce(this, obstacles);

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
            if (AgentRigid.velocity.sqrMagnitude > (0.1f * 0.1f))
            {
                transform.forward += AgentRigid.velocity;
            }
            else
            {
                isReachedTarget = true;
            }
        }
        private Vector3 TruncateVel(Vector3 desireVel)
        {
            return desireVel.sqrMagnitude > (maxSpeed * maxSpeed) ?
                            desireVel.normalized * maxSpeed : desireVel;
        }
        public void Select() { isSelected = true; }
        public void UnSelect() { isSelected = false; }
        public void MoveToTarget()
        {
            isReachedTarget = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
            if (drawGizmos)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, meshRenderer.bounds.extents.x);

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
