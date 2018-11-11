using DelegateCollection;
using EnumCollection;

namespace AIs.BT.Commoms
{
    public class Repeater : BaseNode
    {
        protected BaseNode child;

        protected ActionNodeDelegate terminalCondition;

        public Repeater(BaseNode argChild, ActionNodeDelegate condition)
        {
            child = argChild;
            terminalCondition = condition;
        }

        public override NodeState Evaluate()
        {            
            switch (terminalCondition())
            {
                case NodeState.Failure:
                    child.Evaluate();
                    State = NodeState.Running;
                    break;
                case NodeState.Success:
                case NodeState.Running:
                    return State;
            }
            return State;
        }
    }
}