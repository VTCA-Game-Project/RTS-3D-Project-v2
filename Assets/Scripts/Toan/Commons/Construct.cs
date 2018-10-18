using DelegateCollection;
using EnumCollection;
using Manager;
using UnityEngine;

namespace Common
{
    public abstract class Construct : MonoBehaviour
    {

        protected ConstructId[] onwed;
        protected Player player;

        public int  Hp                      { get; protected set; }
        public ConstructId Id               { get; protected set; }
        public ConstructId[] Owned          { get; protected set; }
        public GameAction AddConstruct      { protected get; set; }
        public GameAction RemoveConstruct   { protected get; set; }

        protected virtual void Start()
        {
            player          = GetComponent<Player>();
            AddConstruct    =  player.AddConstruct;
            RemoveConstruct = player.RemoveConstruct;
            Hp = 1;
            Init();
        }
        protected void Init()
        {
            switch (Id)
            {
                case ConstructId.Yard:
                    onwed = new ConstructId[]
                    {
                    ConstructId.Refinery,
                    };
                    break;
                case ConstructId.Refinery:
                    onwed = new ConstructId[]
                    {
                    ConstructId.Barrack,
                    };
                    break;
                case ConstructId.Barrack:
                    onwed = new ConstructId[]
                    {
                    ConstructId.Defender,
                    ConstructId.Radar,
                    };
                    break;
                default:
                    onwed = new ConstructId[0];
#if UNITY_EDITOR
                    Debug.Log("Tower name not found");
#endif
                    break;
            }
        }
        protected void UnlockConstruct()
        {
           AddConstruct(this);
        }

        // public method
        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Hp = 0;
                DestroyConstruct();
            }
        }
        public virtual void Build()
        {
            UnlockConstruct();
        }       
        public virtual void DestroyConstruct()
        {
            RemoveConstruct(this);
            Destroy(this.gameObject);
        }
        
    }
}