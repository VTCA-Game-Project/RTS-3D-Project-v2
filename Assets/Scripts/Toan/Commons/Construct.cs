using DelegateCollection;
using EnumCollection;
using Manager;
using Pattern;
using UnityEngine;

namespace Common
{
    public abstract class Construct : GameEntity
    {

        public Player Player;

        public int Hp { get; protected set; }
        public ConstructId Id { get; protected set; }
        public ConstructId[] Owned { get; protected set; }
        public GameAction AddConstruct { protected get; set; }
        public GameAction RemoveConstruct { protected get; set; }

        public override Vector3 Position
        {
            get
            { return Vector3.ProjectOnPlane(transform.position, Vector3.up); }
        }
        public override Vector3 Heading
        {
            get
            { return Vector3.ProjectOnPlane(transform.forward, Vector3.up); }
        }
        public override Vector3 Velocity { get { return Vector3.zero; } }
        public override bool IsDead { get; protected set; }
       
        protected virtual void Awake() { }
        protected virtual void Start()
        {
        //    if (Singleton.classname == "Human")
        //    {
        //        Player = FindObjectOfType<MainPlayer>();
        //    }
        //    else
        //    {
        //        Player = FindObjectOfType<NPCPlayer>();
        //    }
            AddConstruct = Player.AddConstruct;
            RemoveConstruct = Player.RemoveConstruct;
            Hp = 1;
            Init();
        }
        protected virtual void Update() { }

        protected void Init()
        {
            switch (Id)
            {
                case ConstructId.Yard:
                    Owned = new ConstructId[]
                    {
                    ConstructId.Refinery,
                    };
                    break;
                case ConstructId.Refinery:
                    Owned = new ConstructId[]
                    {
                    ConstructId.Barrack,
                    };
                    break;
                case ConstructId.Barrack:
                    Owned = new ConstructId[]
                    {
                    ConstructId.Defender,
                    ConstructId.Radar,
                    };
                    break;
                default:
                    Owned = new ConstructId[0];
#if UNITY_EDITOR
                    Debug.Log("Tower name not found");
#endif
                    break;
            }
        }
        protected void UnlockConstruct()
        {
            if(AddConstruct == null) AddConstruct = Player.AddConstruct;
            AddConstruct(this);
        }

        // public method
        public override void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                IsDead = true;
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