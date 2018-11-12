using AIs.BT.Commoms;
using Common;
using Common.Building;
using Common.Entity;
using EnumCollection;
using Manager;
using Pattern;
using RTS_ScriptableObject;
using System.Collections.Generic;
using UnityEngine;
using Utils;

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

        private QueryList<ConstructId, float> constructCountQuery;
        private QueryList<Soldier, float> agentCountQuery;

        public ConstructLocationOffset LocationOffset   { get; set; }
        public ConstructPrice ConstructPrice            { get; set; }
        public GameEntityPrice AgentPrice               { get; set; }

        public BaseNode Root { get; private set; }

        public BTNPCPlayer(NPCPlayer player,MainPlayer main)
        {
            npc = player;
            mainPlayer = main;

            constructCountQuery = new QueryList<ConstructId, float>();
            agentCountQuery = new QueryList<Soldier, float>();

            InitBT();
        }

        public void UpdateCountDown(float deltaTime)
        {
            List<QueryItem<ConstructId,float>> queryItems = constructCountQuery.QueryItemList();
            for (int i = 0; i < queryItems.Count; i++)
            {
                queryItems[i].value += deltaTime;
            }

            List<QueryItem<Soldier, float>> agentItems = agentCountQuery.QueryItemList();
            for (int i = 0; i < agentItems.Count; i++)
            {
                agentItems[i].value += deltaTime;
            }
            
        }

        public NodeState Evaluate()
        {
            return Root.Evaluate();
        }

        private void InitBT()
        {
            //Root = new Selector(new List<BaseNode>()
            //{
            //    //new ActionNode(CheckGameStatus),
            //    BuyRefineryConstructSequence(),
            //});
            Root = BuyRefineryConstructSequence();
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
            if(constructCountQuery.Remove(type))
            {
                construct.position = pos;                
                return NodeState.Success;
            }           
            return NodeState.Failure;
        }

        private NodeState CheckConstructCountdown(ConstructId type)
        {
            float time;
            if(constructCountQuery.TryGetValue(type,out time))
            {
                if (time >= ConstructBuyDelay[type]) return NodeState.Success;
            }
            return NodeState.Failure;
        }

        private NodeState CheckAgentCountdown(Soldier type)
        {
            float time;
            if (agentCountQuery.TryGetValue(type,out time))
            {
                if (time >= AgentBuyDelay[type]) return NodeState.Success;
            }
            return NodeState.Failure;
        }

        private NodeState CheckUnclockedConstruct(ConstructId type)
        {
            if (npc.IsCanBuild(type))
            {
                return NodeState.Success;
            }
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
                List<QueryItem<ConstructId, float>> queryItems = constructCountQuery.QueryItemList();
                for (int i = 0; i < queryItems.Count; i++)
                {
                    if(queryItems[i].value < ConstructBuyDelay[queryItems[i].key]) return NodeState.Failure;
                }
                return NodeState.Success;
            }

            if (type == typeof(Soldier))
            {
                List<QueryItem<Soldier, float>> agentItems = agentCountQuery.QueryItemList();
                for (int i = 0; i < agentItems.Count; i++)
                {
                    if (agentItems[i].value < AgentBuyDelay[agentItems[i].key]) return NodeState.Failure;
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
                constructCountQuery.Add(new QueryItem<ConstructId,float>(type, 0.0f));
            }
            return NodeState.Success;
        }

        private NodeState BuyAgent(Soldier type)
        {
            if (!agentCountQuery.ContainsKey(type))
            {
                agentCountQuery.Add(new QueryItem<Soldier,float>(type, 0.0f));
            }
            return NodeState.Success;
        }

        // tree node

        #region Location Yard Construct Sequence
        private Sequence LocationYardConstructNode()
        {
            return new Sequence(new List<BaseNode>()
            {
                new ActionNode(CheckYardEndCountDown),
                new ActionNode(SetYardContrustPostion),                
            });
        }
        
        private NodeState SetYardContrustPostion()
        {
            GameObject prefab;
            if(Singleton.classname == "Human")
            {
                prefab = AssetUtils.Instance.GetAsset("OrcYard") as GameObject;
            }
            else
            {
                prefab = AssetUtils.Instance.GetAsset("HumanYard") as GameObject;
            }

            GameObject yardGameObj = GameObject.Instantiate(prefab);
            Construct yard = yardGameObj.GetComponentInChildren<Construct>();
            yard.Group = Group.NPC;
            yardGameObj.SetActive(true);
            yard.Player = npc;
            yard.Build();

            return SetConstructPosition(ConstructId.Yard, LocationOffset.Yard, yardGameObj.transform);
        }

        private NodeState CheckYardEndCountDown()
        {
            return CheckConstructCountdown(ConstructId.Yard);
        }
        #endregion

        #region Location Refinery Construct Sequence
        private Sequence LocationRefineryConstructNode()
        {
            return new Sequence(new List<BaseNode>()
            {
                new ActionNode(CheckRefineryEndCountDown),
                new ActionNode(SetRefineryConstructPosition),
            });
        }

        private NodeState SetRefineryConstructPosition()
        {
            GameObject prefab;
            if (Singleton.classname == "Human")
            {
                prefab = AssetUtils.Instance.GetAsset("OrcRefinery") as GameObject;
            }
            else
            {
                prefab = AssetUtils.Instance.GetAsset("HumanRefinery") as GameObject;
            }
            GameObject refineryGameObj = GameObject.Instantiate(prefab);
            Construct refinery = refineryGameObj.GetComponentInChildren<Construct>();
            refinery.Player = npc;
            refinery.Group = Group.NPC;
            refineryGameObj.SetActive(true);
            refinery.Build();
            return SetConstructPosition(ConstructId.Refinery, LocationOffset.Refinery, refineryGameObj.transform);
        }

        private NodeState CheckRefineryEndCountDown()
        {
            return CheckConstructCountdown(ConstructId.Refinery);
        }
        #endregion

        #region Buy Yard Construct Sequence
        private Sequence BuyYardConstructSequence()
        {
            return new Sequence(new List<BaseNode>()
            {
                new ActionNode(CheckEnoughGoldToBuyYard),
                new ActionNode(BuyYardConstruct),
                RepeatUntilSetYardLocationSuccess(),
            });
        }

        private Repeater RepeatUntilSetYardLocationSuccess()
        {
            return new Repeater(LocationYardConstructNode());
        }

        private NodeState CheckEnoughGoldToBuyYard()
        {
            return CheckEnoughGold(ConstructPrice.Yard);
        }

        private NodeState BuyYardConstruct()
        {
            return BuyConstruct(ConstructId.Yard);
        }

        #endregion

        #region Unclock Refinery Selector
        private Selector UnClockRefinerySelector()
        {
            return new Selector(new List<BaseNode>()
            {
                new ActionNode(CheckUnclockedRefinery),
                BuyYardConstructSequence(),
            });
        }

        private NodeState CheckUnclockedRefinery()
        {
            return CheckUnclockedConstruct(ConstructId.Refinery);
        }
        #endregion

        #region Buy Refinery Construct Sequence
        private Sequence BuyRefineryConstructSequence()
        {
            return new Sequence(new List<BaseNode>()
            {
                UnClockRefinerySelector(),
                new ActionNode(CheckEnoughGoldToBuyRefinery),
                new ActionNode(BuyRefinery),
                new Repeater(LocationRefineryConstructNode()),
            });
        }

        private NodeState CheckEnoughGoldToBuyRefinery()
        {
            return CheckEnoughGold(ConstructPrice.Refinery);
        }

        private NodeState BuyRefinery()
        {
            return BuyConstruct(ConstructId.Refinery);
        }

        #endregion
    }
}