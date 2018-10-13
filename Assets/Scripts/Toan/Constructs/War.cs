using EnumCollection;
using InterfaceCollection;
using Pattern;
using UnityEngine;
using Utils;

namespace Common.Building
{
    public class War : Construct, IProduce
    {

        protected override void Start()
        {
            Id = ConstructId.War;
            IsActive = true;
            IsUsePower = false;
            ConsumePower = 0;
            base.Start();
        }

        public GameObject Produce(System.Enum type)
        {
            if(type.GetType() == typeof(Vehicle))
            {
                return Singleton.AssetUtils.GetAsset(type.ToString()) as GameObject;
            }
            return null;
        }
    }
}
