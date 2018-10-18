using Manager;
using UnityEngine;

namespace Common.Entity
{
    public class Obstacle : GameEntity
    {
        private MeshRenderer meshRenderer;

#if UNITY_EDITOR
        public bool Debug;
#endif
        public float BoundRadius { get; set; }
        public int Index;
        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            float sizeX = meshRenderer.bounds.size.x;
            float sizeZ = meshRenderer.bounds.size.z;
            BoundRadius = sizeX > sizeZ ? sizeX : sizeZ;
            StoredManager.AddObstacle(this);
        }

        private void OnDestroy()
        {
            StoredManager.RemoveObstacle(this);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Debug)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, BoundRadius);
            }
        }
#endif
    }

}
