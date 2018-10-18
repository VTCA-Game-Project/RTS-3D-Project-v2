using EnumCollection;
using Manager;
using UnityEngine;

namespace Common
{
    public abstract class Construct : MonoBehaviour
    {

        protected ConstructId[] onwed;

        public int  Hp              { get; protected set; }
        public bool IsUsePower      { get; protected set; }
        public ConstructId Id       { get; protected set; }
        public ConstructId[] Owned  { get; protected set; }

        protected virtual void Start()
        {
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
            StoredManager.AddConstruct(this);
        }

        // public method
        public virtual void Build()
        {
            UnlockConstruct();
        }

        public void RecieveDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0) Hp = 0;
        }

        public virtual void DestroyConstruct()
        {
            StoredManager.RemoveConstruct(this);
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