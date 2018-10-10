using Common.Entity;
using Manager;
using UnityEngine;
using Utils;

namespace AI
{
    public class ObstacleAvoidance
    {
        private static readonly ObstacleAvoidance instance = new ObstacleAvoidance();

        private float agentRadius;
        private float detectBoxLenght;
        private Obstacle closestObs = null;
        private Vector3 localPosObstacle;

        private ObstacleAvoidance() { }
        public static ObstacleAvoidance Instance { get { return instance; } }

        public float CalculateDetectBoxLenght(AIAgent agent)
        {
            return agent.MinDetectionBoxLenght
                + (agent.AgentRigid.velocity.sqrMagnitude / (agent.MaxSpeed * agent.MaxSpeed))
                * agent.MaxSpeed;
        }
        public Vector3 GetObsAvoidanceForce(AIAgent agent, Obstacle[] obstacles)
        {
            closestObs      = null;
            detectBoxLenght = agent.DetectBoxLenght;
            agentRadius     = agent.BoundRadius;           

            StoredManager.WhiteAll();

            // find losed obstacle
            localPosObstacle = Vector3.negativeInfinity;
            float distToClosest = (closestObs != null) ? Vector3.Distance(closestObs.Position, agent.Position) : float.MaxValue;
            float dist = 0.0f;

            for (int i = 0; i < obstacles.Length; i++)
            {
                dist = Vector3.Distance(obstacles[i].Position, agent.Position);
                if (dist < distToClosest)
                {
                    distToClosest = dist;
                    closestObs = obstacles[i];
                }
                localPosObstacle = MathUtils.ToLocalPoint(agent.transform, obstacles[i].Position);
            }
            if (closestObs)
            {
                closestObs.Red();

                float x1, x2;
                Vector3 closestLocal = MathUtils.ToLocalPoint(agent.transform, closestObs.Position);                
                if (closestLocal.z > 0 && 
                    MathUtils.CalculateQuadraticBetweenCircleAndXAxis(
                       new Vector2(closestLocal.z, closestLocal.x),
                       closestObs.BoundRadius + agentRadius, out x1, out x2))
                {
                    Debug.Log("Index : " + closestObs.Index + " Local instersection: x1 = " + x1 + " x2 = " + x2 + " local pos: " +
                        MathUtils.ToLocalPoint(agent.transform, closestObs.Position));
                    float closestIZ = Mathf.Min(x1, x2);
                    float multiplier = 1.0f + (detectBoxLenght - closestIZ) / detectBoxLenght;

                    float xF = (5 - closestLocal.x) * multiplier;
                    float zF = (5 - closestIZ);

                    return MathUtils.ToWorldVector(agent.transform, new Vector3(xF, 0, zF));
                }
            }
            return Vector3.zero;
        }
    }
}
