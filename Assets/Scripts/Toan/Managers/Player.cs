﻿using Common;
using Common.Entity;
using DelegateCollection;
using EnumCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class Player : MonoBehaviour
    {
        public Group Group;
        public GameAction LoseAction { get; set; }
        private PlayerContainer status;

        protected virtual void Awake()
        {
            status = new PlayerContainer();
        }
        protected virtual void Start()
        {
            UpdateGameStatus.Instance.AddPlayer(this);
        }

        public void AddConstruct(object construct)
        {
            if (construct is Construct)
            {
                status.AddConstruct((Construct)construct);
            }
        }

        public void RemoveConstruct(object construct)
        {
            if (construct is Construct)
            {
                status.RemoveConstruct((Construct)construct);
            }
        }

        public void TakeGold(int gold)
        {
            status.TakeGold(gold);
        }

        public void AddAgent(AIAgent agent)
        {
            status.AddAgent(agent);
        }

        public void RemoveAgent(AIAgent agent)
        {
            status.RemoveAgent(agent);
        }

        public AIAgent[] GetNeighbours(AIAgent agent)
        {
            return status.GetNeighbours(agent);
        }

        public bool IsAlive() { return status.IsAlive; }

        public void Lose()
        {
            // if this is enemy,broadcast to player
            // if this is play, show notify
            if (LoseAction != null)
                LoseAction(Group);
        }

        public List<AIAgent> Agents
        {
            get { return status.Agents; }
        }

    }
}
