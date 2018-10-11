﻿using Common.Entity;
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
        private Vector3 closestLocalPosition;

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
            closestObs = null;
            detectBoxLenght = agent.DetectBoxLenght;
            agentRadius = agent.BoundRange;
            closestLocalPosition = Vector3.negativeInfinity;

            StoredManager.WhiteAll();

            // find losed obstacle
            localPosObstacle = Vector3.negativeInfinity;
            float distToClosest = float.MaxValue;
            float dist = 0.0f;

            for (int i = 0; i < obstacles.Length; i++)
            {
                localPosObstacle = MathUtils.ToLocalPoint(agent.transform, obstacles[i].Position);
                if (localPosObstacle.z > 0)
                {
                    float expandRadius = (obstacles[i].BoundRadius + agentRadius) * 0.5f;
                    if (localPosObstacle.x < expandRadius)
                    {
                        float x1, x2;
                        MathUtils.CalculateQuadraticBetweenCircleAndXAxis(
                               new Vector2(closestLocalPosition.z, closestLocalPosition.x),
                               expandRadius, out x1, out x2);
                        dist = x1;
                        if (dist <= 0)
                        {
                            dist = x2;
                        }
                        if (dist < distToClosest)
                        {
                            distToClosest = dist;
                            closestObs = obstacles[i];
                            closestLocalPosition = localPosObstacle;
                        }
                    }

                    //dist = obstacles[i].Position.sqrMagnitude;
                    //if (dist < distToClosest)
                    //{
                    //    distToClosest = dist;
                    //    closestObs = obstacles[i];
                    //    closestPosition = localPosObstacle;
                    //}
                }
            }
            if (closestObs)
            {
                float multiplier = 1 + (dist / detectBoxLenght);

                float xF = (closestObs.BoundRadius - closestLocalPosition.x) * multiplier;
                float zF = (closestObs.BoundRadius - closestLocalPosition.z) * 0.2f;
#if UNITY_EDITOR
                //Debug.Log("Index : " + closestObs.Index + " local pos: " +
                //    MathUtils.ToLocalPoint(agent.transform, closestObs.Position));
                //Debug.Log("xF: " + xF + "zF: " + zF);
#endif
                return MathUtils.ToWorldVector(agent.transform, new Vector3(xF, 0, zF));
            }
            #region Old
            //            if (closestObs && Mathf.Floor(closestPosition.x) < closestObs.BoundRadius + agentRadius * 0.5f)
            //            {
            //                closestObs.Red();

            //                float x1, x2;
            //                if (MathUtils.CalculateQuadraticBetweenCircleAndXAxis(
            //                       new Vector2(closestPosition.z, closestPosition.x),
            //                       closestObs.BoundRadius + agentRadius, out x1, out x2))
            //                {

            //                    float closestIZ = Mathf.Min(x1, x2);
            //                    float multiplier = 1.0f + (detectBoxLenght - closestIZ) / detectBoxLenght;

            //                    float xF = (closestObs.BoundRadius - closestPosition.x);
            //                    float zF = (closestObs.BoundRadius - closestPosition.z);
            //#if UNITY_EDITOR
            //                    Debug.Log("Index : " + closestObs.Index + " Local instersection: x1 = " + x1 + " x2 = " + x2 + " local pos: " +
            //                        MathUtils.ToLocalPoint(agent.transform, closestObs.Position));
            //                    Debug.Log("xF: " + xF + "zF: " + zF);
            //#endif
            //                    return MathUtils.ToWorldVector(agent.transform, new Vector3(xF, 0, zF)) * 3.0f;
            //                }
            //            }
            #endregion
            return Vector3.zero;
        }
    }
}
