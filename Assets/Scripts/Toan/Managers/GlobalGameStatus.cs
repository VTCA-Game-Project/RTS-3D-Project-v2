using EnumCollection;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Building;
using Common.Entity;

namespace Manager
{
    public class GlobalGameStatus
    {
        public float Gold                               { get; private set; }
        public List<AIAgent> Agents                     { get; private set; }
        public List<Construct> Constructs               { get; private set; }
        public List<ConstructId> ConstructsCantBuild    { get; private set; }
        
        public GlobalGameStatus()
        {
            Gold = 0;            
            Agents = new List<AIAgent>();
            Constructs = new List<Construct>();
            ConstructsCantBuild = new List<ConstructId>();
        }                 

        public void NewConstructBuilt(Construct construct)
        {
            ConstructId[] unlock = construct.Owned;
            for (int i = 0; i < unlock.Length; i++)
            {
                ConstructsCantBuild.Add(unlock[i]);
#if UNITY_EDITOR
                Debug.Log(unlock[i] + " add cant build list");
#endif
            }
        }

        public void ConstructDestroyed(Construct construct)
        {
            ConstructId[] unlock = construct.Owned;
            for (int i = 0; i < unlock.Length; i++)
            {
                ConstructsCantBuild.RemoveAt(ConstructsCantBuild.IndexOf((unlock[i])));
#if UNITY_EDITOR
                Debug.Log(unlock[i] + " remove cant build list");
#endif
            }
        }
        public void TakeGold(float gold)
        {
            Gold += gold;
        }

        public PayGoldStatus PayGold(float requireGold, out float debt)
        {
            debt = requireGold;
            if (Gold <= 0) return PayGoldStatus.Terminal;
            if (Gold < requireGold)
            {
                debt = requireGold - Gold;
                return PayGoldStatus.Pause;
            }
            debt = 0;
            return PayGoldStatus.Success;
        }

        // agent
        public void AddAgent(AIAgent agent)
        {
            Agents.Add(agent);
        }
        public void RemoveAgent(AIAgent agent)
        {
            Agents.RemoveAt(Agents.IndexOf(agent));
        }
        // AI uitls
        public AIAgent[] GetNeighbours(AIAgent agent)
        {
            List<AIAgent> result = new List<AIAgent>();
            float sqrBoundRadius = agent.NeighbourRadius * agent.NeighbourRadius;
            for (int i = 0; i < Agents.Count; i++)
            {
                if (Agents[i] != agent &&
                    Vector3.SqrMagnitude(agent.Position - Agents[i].Position) <= sqrBoundRadius)
                {
                    result.Add(Agents[i]);
                }
            }
            return result.ToArray();
        }
        // constructs
        public void AddConstruct(Construct construct)
        {
            Constructs.Add(construct);
            NewConstructBuilt(construct);
#if UNITY_EDITOR
            Debug.Log(construct.Id + " added");
#endif
        }
        public void RemoveConstruct(Construct construct)
        {
            Constructs.RemoveAt(Constructs.IndexOf(construct));
            ConstructDestroyed(construct);
#if UNITY_EDITOR
            Debug.Log(construct.Id + " destroyed");
#endif
        }
    }
}
