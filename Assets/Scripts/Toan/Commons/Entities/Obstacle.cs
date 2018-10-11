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
            BoundRadius = meshRenderer.bounds.extents.magnitude;
#if UNITY_EDITOR
            White();
#endif
            StoredManager.AddObstacle(this);
        }

#if UNITY_EDITOR
        public void Red()
        {
            meshRenderer.material.color = Color.red;
        }
        public void White()
        {
            meshRenderer.material.color = Color.white;
        }
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
