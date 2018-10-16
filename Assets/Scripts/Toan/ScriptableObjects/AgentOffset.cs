using UnityEngine;

namespace RTS_ScriptableObject
{
    [CreateAssetMenu(fileName = "AI Agent Offset", menuName = @"RTS Game/Agent Offset", order = 1)]
    public class AgentOffset : ScriptableObject
    {
        public float Seeking;
        public float MaxSpeed;
        public float Cohesion;
        public float Separation;
        public float Alignment;        
        public float AttackRadius;
        public float NeighboursRadius;
        public float ObstacleAvoidance;
    }
}