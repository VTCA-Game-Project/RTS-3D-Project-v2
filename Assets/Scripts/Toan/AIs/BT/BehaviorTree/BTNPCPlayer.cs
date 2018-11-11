using AIs.BT.Commoms;
using Common;
using Common.Building;
using Common.Entity;
using EnumCollection;
using Manager;
using System.Collections.Generic;
using UnityEngine;

namespace AIs.BT.BehaviorTree
{
    public class BTNPCPlayer
    {

        private readonly Dictionary<ConstructId, float> ConstructBuyDelay = new Dictionary<ConstructId, float>()
        {
            { ConstructId.Yard      , 2.0f },
            { ConstructId.Refinery  , 2.0f },
            { ConstructId.Barrack   , 2.0f },
            { ConstructId.Defender  , 2.0f },
            { ConstructId.Radar     , 2.0f },
        };

        private readonly Dictionary<Soldier, float> AgentBuyDelay = new Dictionary<Soldier, float>()
        {
            { Soldier.Archer        ,2.0f},
            { Soldier.HumanWarrior  ,2.0f},
            { Soldier.WoodHorse     ,2.0f},
            { Soldier.Warrior       ,2.0f},
            { Soldier.OrcTanker     ,2.0f},
            { Soldier.Magic         ,2.0f},
        };

        private NPCPlayer npc;
        private MainPlayer mainPlayer;

        private Dictionary<ConstructId, float> constructCountQuery;
        private Dictionary<Soldier, float> agentCountQuery;

        public BaseNode Root { get; private set; }

        public BTNPCPlayer(NPCPlayer player,MainPlayer main)
        {
            npc = player;
            mainPlayer = main;

            constructCountQuery = new Dictionary<ConstructId, float>();
            agentCountQuery = new Dictionary<Soldier, float>();
        }

        private void InitBT()
        {
            List<BaseNode> children = new List<BaseNode>();

            ActionNode getGameStatus = new ActionNode(CheckGameStatus);

            Root = new Selector(children);
        }

        //  commons action node
        private NodeState CheckGameStatus()
        {
            if (UpdateGameStatus.Instance.GameIsRunning)
            {
                return NodeState.Success;
            }
            return NodeState.Failure;
        }

        private NodeState CheckEnoughGold(int gold)
        {
            if (npc.GetGold() >= gold) return NodeState.Success;
            return NodeState.Failure;
        }

        private NodeState SetConstructPosition(ConstructId type,Vector3 pos,Transform construct)
        {
            if(constructCountQuery.ContainsKey(type))
            {
                construct.position = pos;
                constructCountQuery.Remove(type);
                return NodeState.Success;
            }           
            return NodeState.Failure;
        }

        private NodeState CheckConstructCountdown(ConstructId type)
        {
            if(constructCountQuery.ContainsKey(type))
            {
                if (constructCountQuery[type] >= ConstructBuyDelay[type]) return NodeState.Success;
            }
            return NodeState.Failure;
        }

        private NodeState CheckAgentCountdown(Soldier type)
        {
            if (agentCountQuery.ContainsKey(type))
            {
                if (agentCountQuery[type] >= agentCountQuery[type]) return NodeState.Success;
            }
            return NodeState.Failure;
        }

        private NodeState CheckUnclockedConstruct(ConstructId type)
        {
            if (npc.IsCanBuild(type)) return NodeState.Success;
            return NodeState.Failure;
        }

        private NodeState SelectOnRefineryConstruct()
        {
            List<Construct> constructs = npc.Constructs;
            for (int i = 0; i < constructs.Count; i++)
            {
                if(constructs[i] is Refinery)
                {
                    ((Refinery)constructs[i]).Produce(null);
                    return NodeState.Success;
                }
            }
            return NodeState.Failure;
        }

        private NodeState CheckContainConstruct(ConstructId type)
        {
            List<Construct> constructs = npc.Constructs;
            for (int i = 0; i < constructs.Count; i++)
            {
                if (constructs[i] is Refinery)
                {
                    return NodeState.Success;
                }
            }
            return NodeState.Failure;
        }

        private NodeState CheckAllBuyProgressSuccess(System.Type type)
        {
            if(type == typeof(ConstructId))
            {
                foreach (KeyValuePair<ConstructId,float> item in constructCountQuery)
                {
                    if (ConstructBuyDelay[item.Key] > item.Value) return NodeState.Failure;                       
                }
                return NodeState.Success;
            }

            if (type == typeof(Soldier))
            {
                foreach (KeyValuePair<Soldier, float> item in agentCountQuery)
                {
                    if (AgentBuyDelay[item.Key] > item.Value) return NodeState.Failure;
                }
                return NodeState.Success;
            }
            return NodeState.Failure;
        }

        private NodeState MoveAgentsTo(Vector3 pos,TargetType targetType)
        {
            List<AIAgent> agent = npc.Agents;
            for (int i = 0; i < agent.Count; i++)
            {
                agent[i].SetTarget(TargetType.Place, pos);
            }
            return NodeState.Success;
        }

        private NodeState CheckAgentsReachedTarget()
        {
            List<AIAgent> agent = npc.Agents;
            for (int i = 0; i < agent.Count; i++)
            {
                if (!agent[i].IsReachedTarget) return NodeState.Failure;
            }
            return NodeState.Success;
        }

        private NodeState CountAgentsToAttack()
        {
            if (npc.Agents.Count >= 10) return NodeState.Success;
            return NodeState.Failure;
        }

        private NodeState BuyConstruct(ConstructId type)
        {
            if (!constructCountQuery.ContainsKey(type))
            {
                constructCountQuery.Add(type, 0.0f);
            }
            return NodeState.Success;
        }

        private NodeState BuyAgent(Soldier type)
        {
            if (!agentCountQuery.ContainsKey(type))
            {
                agentCountQuery.Add(type, 0.0f);
            }
            return NodeState.Success;
        }

    }
}