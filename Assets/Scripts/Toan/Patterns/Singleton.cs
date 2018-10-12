﻿using AI;
using Manager;
using Utils;

namespace Pattern
{
    /// <summary>
    ///  Singleton collection
    /// </summary>
    public class Singleton
    {
        // AI Singleton
        public static readonly ObstacleAvoidance    ObstacleAvoidance = ObstacleAvoidance.Instance;
        public static readonly FlockBehavior        FlockBehavior = FlockBehavior.Instance;
        public static readonly SteerBehavior        SteerBehavior = SteerBehavior.Instance;

        // utils singleton
        public static readonly MathUtils            MathUtils = MathUtils.Instance;
        public static readonly AssetUtils           AssetUtils = AssetUtils.Instance;


        // manager
        public static readonly StoredManager        StoredManager = StoredManager.Instance;
           
    }
}