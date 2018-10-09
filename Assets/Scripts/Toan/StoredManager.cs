using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Manager
{
    public class StoredManager
    {
        public static List<AIAgent> Agents = new List<AIAgent>();
        public static List<Construct> Constructs = new List<Construct>();
        public static List<Obstacle> Obstacles = new List<Obstacle>();

        // agent
        public static void AddAgent(AIAgent agent)
        {
            Agents.Add(agent);
        }
        public static void RemoveAgent(AIAgent agent)
        {
            Agents.RemoveAt(Agents.IndexOf(agent));
        }
        // AI uitls
        public static AIAgent[] GetNeighbours(AIAgent agent)
        {
            List<AIAgent> result = new List<AIAgent>();
            float sqrBoundRadius = agent.NeighbourRadius * agent.NeighbourRadius;
            for (int i = 0; i < Agents.Count; i++)
            {
                if (Agents[i] != agent && Vector3.SqrMagnitude(agent.Position - Agents[i].Position) <= sqrBoundRadius)
                {
                    result.Add(Agents[i]);
                }
            }
            return result.ToArray();
        }
        // constructs
        public static void AddConstruct(Construct construct)
        {
            Constructs.Add(construct);
            GlobalGameStatus.NewConstructBuilded(construct);
#if UNITY_EDITOR
            Debug.Log(construct.Id + " added");
#endif
        }
        public static void RemoveConstruct(Construct construct)
        {
            Constructs.RemoveAt(Constructs.IndexOf(construct));
            GlobalGameStatus.ConstructDestroyed(construct);
#if UNITY_EDITOR
            Debug.Log(construct.Id + " destroyed");
#endif
        }

        public static void PowerLow()
        {
            for(int i = 0; i < Constructs.Count; i++)
            {
                if (Constructs[i].IsUsePower) Constructs[i].IsActive = false;
            }
        }
        public static void PowerHight()
        {
            for (int i = 0; i < Constructs.Count; i++)
            {
                if (Constructs[i].IsUsePower) Constructs[i].IsActive = true;
            }
        }
        // obstacle
        public static void AddObstacle(Obstacle obs)
        {
            Obstacles.Add(obs);
        }
        public static void RemoveObstacle(Obstacle obs)
        {
            Obstacles.RemoveAt(Obstacles.IndexOf(obs));
        }
        public static Obstacle[] GetObstacle(AIAgent agent)
        {
            List<Obstacle> result = new List<Obstacle>();
            for (int i = 0; i < Obstacles.Count; i++)
            {
                if(Vector3.Distance(agent.Position,Obstacles[i].Position) <= agent.detectBoxLenght)
                {
                    result.Add(Obstacles[i]);
                }
            }
            return result.ToArray();
        }

        public static void WhiteAll()
        {
            for (int i = 0; i < Obstacles.Count; i++)
            {
                Obstacles[i].White();
            }
        }
    }
}
