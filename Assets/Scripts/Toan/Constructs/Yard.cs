using Common;
using EnumCollection;

namespace Building
{
    public class Yard : Construct
    {
        public override void Produce() { }

        protected override void Start()
        {
            Id = ConstructId.Yard;
            IsActive = true;
            IsUsePower = false;
            ConsumePower = 0;            
            base.Start();
        }
    }
}
