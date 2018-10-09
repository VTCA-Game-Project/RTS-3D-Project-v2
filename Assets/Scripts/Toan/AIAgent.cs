using UnityEngine;
using AI;
using Manager;
using Utils;

namespace Common
{
    public class AIAgent : GameEntity
    {
        private SteerBehavior steerBh;
        private FlockBehavior flockBh;

        private Vector3 steering;
        private Vector3 aceleration;

        private bool isSelected = false;
        private bool isReachedTarget = true;
        private Rigidbody agentRigid;
        private MeshRenderer meshRenderer;
        private AIAgent[] neighbours;
        private Obstacle[] obstacles;

        public Pointer target;
        public float separation;
        public float cohesion;
        public float alignment;
        public float maxSpeed;
        //public int index;
        [Header("Obstacle avoidance")]
        public float detectBoxLenght;
        public float minDetectionBoxLenght;

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
                return Vector3.ProjectOnPlane(agentRigid.velocity, Vector3.up);
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
            agentRigid = GetComponent<Rigidbody>();
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            target = FindObjectOfType<Pointer>();
            BoundRadius = 2;
        }
        private void Start()
        {
            steerBh = AIUtils.steerBehaviorInstance;
            flockBh = AIUtils.flockBehaviorInstance;
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
                steering += ObstacleAvoidance();

                aceleration = steering / agentRigid.mass;
                agentRigid.velocity = TruncateVel(agentRigid.velocity + aceleration);
                RotateAgent();

            }
#if UNITY_EDITOR
            // Debug.Log("steer: " + steering + " velocity: " + rigid.velocity + " max speed: " + maxSpeed * rigid.velocity.normalized);
#endif
        }

        private void RotateAgent()
        {
            if (agentRigid.velocity.sqrMagnitude > (0.1f * 0.1f))
            {
                transform.forward += agentRigid.velocity;
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

        Obstacle closestObs = null;

        private Vector3 ObstacleAvoidance()
        {
            detectBoxLenght = minDetectionBoxLenght + (agentRigid.velocity.sqrMagnitude / (float)(maxSpeed * maxSpeed)) * maxSpeed;
            obstacles = StoredManager.GetObstacle(this);
            StoredManager.WhiteAll();
            // find losed obstacle

            Vector3 localPosObstacle = Vector3.negativeInfinity;

            float distToClosest = (closestObs != null) ? Vector3.Distance(closestObs.Position, Position) : float.MaxValue;
            float dist = 0.0f;

            for (int i = 0; i < obstacles.Length; i++)
            {
                dist = Vector3.Distance(obstacles[i].Position, Position);
                if (dist < distToClosest)
                {
                    distToClosest = dist;
                    closestObs = obstacles[i];
                }
                localPosObstacle = MathUtils.ToLocalPoint(transform, obstacles[i].Position);
                //Debug.Log("Index: " + obstacles[i].Index + " World point: " + obstacles[i].Position +
                //       " | Local point: " + localPosObstacle);


            }

            if (closestObs)
            {
                float x1, x2;
                Vector3 closestLocal = MathUtils.ToLocalPoint(transform, closestObs.Position);
                closestObs.Red();
                if (closestLocal.z > 0 && MathUtils.CalculateQuadraticBetweenCircleAndXAxis(
                       new Vector2(closestLocal.z, closestLocal.x),
                       closestObs.BoundRadius + BoundRadius/2.0f, out x1, out x2))
                {
#if UNITY_EDITOR
                    Debug.Log("Index : " + closestObs.Index + " Local instersection: x1 = " + x1 + " x2 = " + x2 + " local pos: " +
                        MathUtils.ToLocalPoint(transform, closestObs.Position));
                    float closestIZ = Mathf.Min(x1, x2);
                    float multiplier = 1.0f + (detectBoxLenght - closestIZ) / detectBoxLenght;

                    float xF = (5 - closestLocal.x) * multiplier;
                    float zF = (5 - closestIZ);

                    return transform.TransformVector(new Vector3(xF, 0, zF));
#endif  
                }
            }
            return Vector3.zero;
            //if (closestObs != null)
            //    Debug.Log("Closest index: " + closestObs.Index);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
            if (drawGizmos)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, meshRenderer.bounds.extents.x);

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(transform.position, transform.forward * detectBoxLenght);
            }
        }
#endif
    }
}
