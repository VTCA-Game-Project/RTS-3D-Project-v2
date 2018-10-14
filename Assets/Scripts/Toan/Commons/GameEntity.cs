using InterfaceCollection;
using UnityEngine;

namespace Common
{
    public abstract class GameEntity : MonoBehaviour, IGameEntiy
    {
        public virtual Vector3 Heading
        {
            get { return Vector3.ProjectOnPlane(transform.forward, Vector3.up); }
        }
        public virtual Vector3 Position
        {
            get { return Vector3.ProjectOnPlane(transform.position, Vector3.up); }
        }
        public virtual Vector3 Velocity
        {
            get { return Vector3.zero; }
        }

        public virtual void Dead() { }
        public virtual void ReceiveDamage(int damage) { }
    }
}
