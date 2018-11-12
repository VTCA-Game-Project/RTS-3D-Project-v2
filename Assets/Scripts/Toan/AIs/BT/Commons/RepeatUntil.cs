using DelegateCollection;
using EnumCollection;

namespace AIs.BT.Commoms
{
    public class RepeaterUntil : BaseNode
    {
        protected BaseNode child;
        protected ActionNodeDelegate terminalCondition;

        public RepeaterUntil(BaseNode argChild,ActionNodeDelegate condition) : base()
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