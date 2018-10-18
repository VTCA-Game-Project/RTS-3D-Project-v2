using Common.Entity;
using EnumCollection;
using UnityEngine;

namespace Animation
{
    public abstract class BaseAnimation : MonoBehaviour
    {
        protected Animator anims;
        protected Rigidbody agentRigid;
        protected AIAgent agent;

        public AnimState DefaultState   { get; set; }
        public AnimState CurrentState   { get; set; }
        public AnimState NextState      { get; set; }

        public void Play(AnimState type)
        {
            if (type == CurrentState) return;
            ResetParams();
            CurrentState = type;
            switch (type)
            {
                case AnimState.Idle:
                    Idle();
                    break;
                case AnimState.Attack:
                    Attack();
                    break;                
                case AnimState.Run:
                    Run();
                    break;
                case AnimState.Dead:
                    Dead();
                    break;
                case AnimState.Damage:
                    Damage();
                    break;
                default:
                    break;
            }
        }

        protected abstract void Idle();
        protected abstract void Run();
        protected abstract void Damage();
        protected abstract void Attack();
        protected abstract void Dead();
        protected abstract void ResetParams();

        private void Awake()
        {
            agent = GetComponent<AIAgent>();
            anims = GetComponent<Animator>();
            agentRigid = GetComponent<Rigidbody>();

            DefaultState = AnimState.Idle;
            CurrentState = AnimState.None;
            NextState = DefaultState;
        }
    }
}