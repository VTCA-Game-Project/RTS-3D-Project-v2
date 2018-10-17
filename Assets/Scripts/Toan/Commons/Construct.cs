using EnumCollection;
using Manager;
using UnityEngine;

namespace Common
{
    public abstract class Construct : MonoBehaviour
    {

        protected ConstructId[] onwed;
        protected int ConsumePower { get; set; }
        protected int hp;        

        public bool IsActive { get; set; }
        public bool IsUsePower { get; protected set; }
        public ConstructId Id { get; protected set; }
        public ConstructId[] Owned
        {
            get { return onwed; }
            protected set { onwed = value; }
        }
        public int Hp
        {
            get { return hp; }
            protected set { hp = value; }
        }

        protected virtual void Start()
        {
            Init();
            Hp = 10;
        }

        protected void Init()
        {
            switch (Id)
            {
                case ConstructId.Yard:
                    onwed = new ConstructId[]
                    {
                    ConstructId.Power,
                    };
                    break;
                case ConstructId.Power:
                    onwed = new ConstructId[]
                    {
                    ConstructId.Refinery,
                    };
                    break;
                case ConstructId.Refinery:
                    onwed = new ConstructId[]
                    {
                    ConstructId.War,
                    ConstructId.Barrack,
                    };
                    break;
                case ConstructId.War:
                    onwed = new ConstructId[]
                    {
                    ConstructId.Radar,
                    ConstructId.FarDefender,
                    };
                    break;
                case ConstructId.Barrack:
                    onwed = new ConstructId[]
                    {
                    ConstructId.NearDefender,
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
            StoredManager.AddConstruct(this);
        }

        // public method
        public virtual void Build()
        {
            UnlockConstruct();
            GlobalGameStatus.Instance.IncreaseRequirePower(ConsumePower);
        }

        public void RecieveDamage(int damage)
        {
            hp -= damage;
            if (hp <= 0) hp = 0;
        }

        public virtual void DestroyConstruct()
        {
            StoredManager.RemoveConstruct(this);
            GlobalGameStatus.Instance.DecreaseRequirePower(ConsumePower);
            Destroy(this.gameObject);
        }

        public void Reqair()
        {
            // do something
        }
        
        protected virtual void Update()
        {
            if (Hp <= 0)
            {
                DestroyConstruct();
            }
        }
    }
}