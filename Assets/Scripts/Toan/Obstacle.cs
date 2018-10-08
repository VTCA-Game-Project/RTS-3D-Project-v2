using UnityEngine;

namespace Common
{
    public class Obstacle : GameEntity
    {
        public float BoundRadius { get; set; }
        private void Awake()
        {

        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, BoundRadius);
        }
#endif
    }

}
