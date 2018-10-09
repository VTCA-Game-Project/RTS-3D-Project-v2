using Manager;
using UnityEngine;

namespace Common
{
    public class Obstacle : GameEntity
    {
        private MeshRenderer meshRenderer;
        public float BoundRadius { get; set; }
        public int Index;
        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            BoundRadius = meshRenderer.bounds.extents.x;
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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, BoundRadius + 1);
        }
#endif
    }

}
