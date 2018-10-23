using UnityEngine;

namespace Common.Entity
{
    public class AIFireBall : MonoBehaviour
    {
        public GameEntity Target { get; set; }
        public float Angular;
        public float Radius;
        private void FixedUpdate()
        {
            if (transform.position.y <= 0) Destroy(this.gameObject);
        }
    }
}