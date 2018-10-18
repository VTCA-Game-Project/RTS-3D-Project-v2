using System;
using EnumCollection;
using InterfaceCollection;
using Manager;


namespace Common.Building
{
    public class Refinery : Construct,IProduce
    {
        public int RemainingGold { get; protected set; }
        protected override void Start()
        {
            Id = ConstructId.Refinery;
            base.Start();
        }

        public void Produce(Enum type = null)
        {
            player.TakeGold(RemainingGold);
        }
    }
}