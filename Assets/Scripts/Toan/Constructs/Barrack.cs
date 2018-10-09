using Common;
using EnumCollection;
using UnityEngine;
using Utils;

namespace Building
{
    public class Barrack : Construct
    {
        public override void Produce() { }

        protected override void Start()
        {
            Id = ConstructId.Barrack;
            IsActive = true;
            IsUsePower = false;
            ConsumePower = 0;
            base.Start();
        }

        public GameObject ProduceSoldier(Soldier soldier)
        {
            string name = "";
            switch (soldier)
            {
                case Soldier.Builder:
                    name = "Builder";
                    break;
                case Soldier.Infantry:
                    name = "Infantry";
                    break;
            }
            return AssetUtils.Instance.GetAsset(name) as GameObject;
        }
    }
}
