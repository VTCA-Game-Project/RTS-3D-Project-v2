using AI;
using UnityEngine;

namespace Utils
{
    public class AIUtils
    {
        public static readonly FlockBehavior flockBehaviorInstance = new FlockBehavior();
        public static readonly SteerBehavior steerBehaviorInstance = new SteerBehavior();
        public static readonly ObstacleAvoidance obstacleAvoidance = new ObstacleAvoidance();
      

    }
}
