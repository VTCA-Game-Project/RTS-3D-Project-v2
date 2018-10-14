using EnumCollection;
using Manager;


namespace Common.Building
{
    public class Refinery : Construct
    {
        protected override void Start()
        {
            Id = ConstructId.Refinery;
            ConsumePower = 0;
            IsUsePower = false;
            IsActive = true;
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }

        public void ReceiveGold(float gold)
        {
            GlobalGameStatus.ReceiveGold(gold);
        }

    }
}