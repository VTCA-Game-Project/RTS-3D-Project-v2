using EnumCollection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public class ArcherAnimation : BaseAnimation
    {
        protected override void Attack()
        {
            anims.SetBool("IsAttack", true);
        }

        protected override void Damage()
        {
        }

        protected override void Dead()
        {
            anims.SetTrigger("Dead");
        }

        protected override void Idle()
        {
            anims.SetBool("IsRunning", false);
        }

        protected override void Run()
        {
            anims.SetBool("IsRunning", true);
        }

        protected override void ResetParams()
        {
            anims.SetBool("IsRuning", false);
            anims.SetBool("IsAttack", false);
        }

        private void Update()
        {
            if (CurrentState == AnimState.Idle)
            {
                if (agentRigid.velocity.sqrMagnitude > 0.1f)
                {
                    NextState = AnimState.Run;
                }
                else if (agent.TargetType == TargetType.NPC)
                {
                    NextState = AnimState.Attack;
                }
            }
            else if (CurrentState == AnimState.Run)
            {
                if (agentRigid.velocity.sqrMagnitude <= 0.1f)
                {
                    NextState = AnimState.Idle;
                    agentRigid.velocity = Vector3.zero;
                }
            }
            if (CurrentState == AnimState.Attack)
            {
                if (agentRigid.velocity.sqrMagnitude > 0.1f)
                {
                    NextState = AnimState.Run;
                }
            }

            Play(NextState);
        }
    }
}