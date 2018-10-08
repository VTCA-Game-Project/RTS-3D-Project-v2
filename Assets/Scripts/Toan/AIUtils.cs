using AI;
using UnityEngine;

namespace Utils
{
    public class AIUtils 
    {
        public static readonly FlockBehavior flockBehaviorInstance = new FlockBehavior();
        public static readonly SteerBehavior steerBehaviorInstance = new SteerBehavior();

        public static Vector3 ToLocalPoint(Transform local, Vector3 point)
        {
            return local.InverseTransformDirection(point);
        }
    }
}
