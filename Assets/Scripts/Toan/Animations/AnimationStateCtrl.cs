using Common.Entity;
using EnumCollection;
using UnityEngine;

namespace Manager
{
    public class AnimationStateCtrl : MonoBehaviour
    {
        private Animator anims;
        private Rigidbody agentRigid;
        private AIAgent agent;

        public AnimState DefaultState { get; set; }
        public AnimState CurrentState { get; set; }
        protected AnimState nextState;
        private void Awake()
        {

            anims = GetComponent<Animator>();
            agentRigid = GetComponent<Rigidbody>();
            agent = GetComponent<AIAgent>();

            DefaultState = AnimState.Idle;
            CurrentState = AnimState.Idle;

            Play(AnimState.Idle);

        }
        public void Play(AnimState state)
        {
            CurrentState = state;
            switch (state)
            {
                case AnimState.Idle:
                    anims.SetBool("IsRunning", false);
                    break;
                case AnimState.Run:
                    anims.SetBool("IsRunning", true);
                    if (state == AnimState.Attack)
                        ResetAnimationParams();
                    break;
                case AnimState.Damage:
                    anims.SetTrigger("Damage");
                    break;
                case AnimState.Attack:
                    anims.SetBool("IsAttack", true);
                    break;
                case AnimState.Dead:
                    anims.SetTrigger("Dead");
                    break;
            }
        }

        private void Update()
        {
            if (CurrentState == AnimState.Idle)
            {
                if (agentRigid.velocity.sqrMagnitude > 0.1f)
                {
                    nextState = AnimState.Run;
                }
                else if (agent.TargetType == TargetType.NPC)
                {
                    nextState = AnimState.Attack;
                }
            }
            else if (CurrentState == AnimState.Run)
            {
                if (agentRigid.velocity.sqrMagnitude <= 0.1f)
                {
                    nextState = AnimState.Idle;
                    agentRigid.velocity = Vector3.zero;
                }
            }
            if (CurrentState == AnimState.Attack)
            {
                if (agentRigid.velocity.sqrMagnitude > 0.1f)
                {
                    nextState = AnimState.Run;
                }
            }
            SetAnimationState();
        }

        private void SetAnimationState()
        {
            if (nextState != CurrentState)
            {
                CurrentState = nextState;
                Play(CurrentState);
            }
        }
        private void ResetAnimationParams()
        {
            anims.SetBool("IsAttack", false);
        }
    }
}