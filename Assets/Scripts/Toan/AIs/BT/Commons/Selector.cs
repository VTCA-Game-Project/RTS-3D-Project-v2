﻿using System.Collections.Generic;
using EnumCollection;

namespace AIs.BT.Commoms
{
    public class Selector : BaseNode
    {
        protected List<BaseNode> children;

        public Selector(List<BaseNode> argChildren)
        {
            children = new List<BaseNode>();
        }

        public override NodeState Evaluate()
        {
            for (int i = 0; i < children.Count; i++)
            {
                switch(children[i].Evaluate())
                {
                    case NodeState.Failure:
                        State = NodeState.Failure;
                        continue;
                    case NodeState.Success:
                        State = NodeState.Success;
                        return State;
                    case NodeState.Running:
                        continue;
                }
            }
            State = NodeState.Failure;
            return State;
        }
    }
}