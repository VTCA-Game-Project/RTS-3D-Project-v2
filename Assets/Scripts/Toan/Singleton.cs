using AI;
using UnityEngine;

namespace Utils
{
    /// <summary>
    ///  AI behabior singleton
    /// </summary>
    public class Singleton
    {        
        public static readonly FlockBehavior    FlockBehavior = FlockBehavior.Instance;
        public static readonly SteerBehavior    SteerBehavior = SteerBehavior.Instance;
        public static readonly MathUtils        MathUtils = MathUtils.Instance;
        public static readonly AssetUtils       AssetUtils = AssetUtils.Instance;

        public static readonly ObstacleAvoidance ObstacleAvoidance = new ObstacleAvoidance();    
    }
}
