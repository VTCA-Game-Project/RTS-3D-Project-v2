using EnumCollection;
using Pattern;
using UnityEngine;

namespace Common.Building
{
    public class Barrack : Construct
    {
        protected override void Start()
        {
            Id = ConstructId.Barrack;
            IsUsePower = false;
            base.Start();
        }

        public GameObject Produce(System.Enum type)
        {
            if (type.GetType() == typeof(Soldier))
            {
                return Singleton.AssetUtils.GetAsset(type.ToString()) as GameObject;
            }
            return null;
        }
    }
}
