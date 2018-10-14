using EnumCollection;
using UnityEngine;

namespace Manager
{
    public class AnimationStateCtrl : MonoBehaviour
    {
        private Animator anims;

        public AnimState DefaultState { get; set; }
        public AnimState CurrentState { get; set; }
        private void Awake()
        {
            
            anims = GetComponent<Animator>();

            DefaultState = AnimState.Idle;
            CurrentState = AnimState.Idle;

            Play(AnimState.Idle);

        }
        public void Play(AnimState state)
        {
            CurrentState = state;
            switch(state)
            {
                case AnimState.Idle:
                    anims.SetBool("IsRunning", false);
                    break;
                case AnimState.Run:
                    anims.SetBool("IsRunning", true);
                    break;
                case AnimState.Damage:
                    anims.SetTrigger("Damage");
                    break;
                case AnimState.Attack:
                    anims.SetTrigger("Attack");
                    break;
                case AnimState.Dead:
                    anims.SetTrigger("Dead");
                    break;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                Play(AnimState.Attack);
            } else if (Input.GetKeyDown(KeyCode.R))
            {
                Play(AnimState.Run);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Play(AnimState.Damage);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Play(AnimState.Dead);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Play(AnimState.Idle);
            }
        }

    }
}