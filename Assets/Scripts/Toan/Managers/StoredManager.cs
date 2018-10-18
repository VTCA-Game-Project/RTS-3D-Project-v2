using System.Collections.Generic;
using UnityEngine;
using Common.Entity;
using Common;

namespace Manager
{
    public class StoredManager
    {
        private static readonly StoredManager instance = new StoredManager();

        private static List<AIAgent>    agents = new List<AIAgent>();
        private static List<Construct>  constructs = new List<Construct>();
        private static List<Obstacle>   obstacles = new List<Obstacle>();

        private StoredManager() { }
        #region Properties
        public List<AIAgent> Agents
        {
            get { return agents; }
            protected set { agents = value; }
        }
        public List<Construct> Constructs
        {
            get { return constructs; }
            protected set { constructs = value; }
        }
        public List<Obstacle> Obstacles
        {
            get { return obstacles; }
            protected set { obstacles = value; }
        }
        public static StoredManager Instance { get { return instance; } }
        #endregion
        // agent
        public static void AddAgent(AIAgent agent)
        {
            agents.Add(agent);
        }
        public static void RemoveAgent(AIAgent agent)
        {
            agents.RemoveAt(agents.IndexOf(agent));
        }
        // AI uitls
        public static AIAgent[] GetNeighbours(AIAgent agent)
        {
            List<AIAgent> result = new List<AIAgent>();
            float sqrBoundRadius = agent.NeighbourRadius * agent.NeighbourRadius;
            for (int i = 0; i < agents.Count; i++)
            {
                if (agents[i] != agent && 
                    Vector3.SqrMagnitude(agent.Position - agents[i].Position) <= sqrBoundRadius)
                {
                    result.Add(agents[i]);
                }
            }
            return result.ToArray();
        }
        // constructs
        public static void AddConstruct(Construct construct)
        {
            constructs.Add(construct);
            GlobalGameStatus.Instance.NewConstructBuilded(construct);
#if UNITY_EDITOR
            Debug.Log(construct.Id + " added");
#endif
        }
        public static void RemoveConstruct(Construct construct)
        {
            int index = constructs.IndexOf(construct);
            if (index >= 0)
            {
                constructs.RemoveAt(index);
                GlobalGameStatus.Instance.ConstructDestroyed(construct);
            }
#if UNITY_EDITOR
            Debug.Log(construct.Id + " destroyed");
#endif
        }

        public static void PowerLow()
        {
            for(int i = 0; i < constructs.Count; i++)
            {
                if (constructs[i].IsUsePower) constructs[i].IsActive = false;
            }
        }
        public static void PowerHight()
        {
            for (int i = 0; i < constructs.Count; i++)
            {
                if (constructs[i].IsUsePower) constructs[i].IsActive = true;
            }
        }
        // obstacle
        public static void AddObstacle(Obstacle obs)
        {
            obstacles.Add(obs);
        }
        public static void RemoveObstacle(Obstacle obs)
        {
            int index = obstacles.IndexOf(obs);
            if (index >= 0)
            {
                obstacles.RemoveAt(index);
            }
        }
        public static Obstacle[] GetObstacle(AIAgent agent)
        {
            List<Obstacle> result = new List<Obstacle>();
            for (int i = 0; i < obstacles.Count; i++)
            {
                if(Vector3.Distance(agent.Position,obstacles[i].Position) <= agent.DetectBoxLenght)
                {
                    result.Add(obstacles[i]);
                }
            }
            return result.ToArray();
        }

        public static void WhiteAll()
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].White();
            }
        }
    }
}
