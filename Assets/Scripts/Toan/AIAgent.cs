using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using Manager;

namespace Agent
{
    public class AIAgent : MonoBehaviour
    {
        private SteerBehavior steerBh;
        private FlockBehavior flockBh;

        private Vector3 steering;
        private Vector3 aceleration;

        private bool isSelected = false;
        private bool isReachedTarget = true;
        public Pointer target;
        public Rigidbody rigid;
        public float BoundRadius;
        public float NeighbourRadius;

        public float separation;
        public float cohesion;
        public float alignment;
        public float maxSpeed;
        public int index;
        public AIAgent[] neighbours;
        [Header("Debug")]
        public bool drawGizmos = true;

        #region Properties
        public Vector3 Velocity
        {
            // using projection
            get
            {
                return Vector3.ProjectOnPlane(rigid.velocity, Vector3.up);
            }
        }
        public float MaxSpeed
        {
            get { return maxSpeed; }
            protected set { maxSpeed = value; }
        }
        public Vector3 Position
        {
            // using projection
            get
            {
                return Vector3.ProjectOnPlane(transform.position, Vector3.up);                
            }
        }
        public Vector3 Heading
        {
            // using projection
            get
            {
                return Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            }
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
        #endregion

        private void Awake()
        {
            gameObject.AddComponent<ClickOn>();
            StoredManager.AddAgent(this);

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
                if (!isReachedTarget)
                {
                    steering += steerBh.Seek(this, target.Position);
                }
                    neighbours = StoredManager.GetNeighbours(this);                    
                    steering += flockBh.Separation(this, neighbours) * separation;
                    steering += flockBh.Alignment(this, neighbours) * alignment;
                    steering += flockBh.Cohesion(this, neighbours) * cohesion;

                    aceleration = steering / rigid.mass;
                    rigid.velocity = Truncate(rigid.velocity + aceleration);
                    RotateAgent();
                
            }
#if UNITY_EDITOR
            // Debug.Log("steer: " + steering + " velocity: " + rigid.velocity + " max speed: " + maxSpeed * rigid.velocity.normalized);
#endif
        }

        private void RotateAgent()
        {
            if (rigid.velocity.magnitude > 0.1f)
            {
                transform.forward += rigid.velocity;
            }
            else
            {
                isReachedTarget = true;
            }
        }
        private Vector3 Truncate(Vector3 desireVel)
        {
            return desireVel.magnitude > maxSpeed ? desireVel.normalized * maxSpeed : desireVel;
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
            if (drawGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, NeighbourRadius);
            }
        }
#endif
    }
}
