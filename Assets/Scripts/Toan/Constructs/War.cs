using Common;
using EnumCollection;
using UnityEngine;
using Utils;

namespace Building
{
    public class War : Construct
    {
        public override void Produce() { }

        protected override void Start()
        {
            Id = ConstructId.War;
            IsActive = true;
            IsUsePower = false;
            ConsumePower = 0;
            base.Start();
        }

        public GameObject ProduceVehicle(Vehicle vehicle)
        {
            string name = "";
            switch (vehicle)
            {
                case Vehicle.GrizzlyTank:
                    name = "GrizzlyTank";
                    break;
            }
            
            return AssetUtils.Instance.GetAsset(name) as GameObject;
        }
    }
}
