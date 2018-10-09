using Common;
using EnumCollection;

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
    }
}
