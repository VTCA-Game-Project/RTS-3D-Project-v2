using Common;
using EnumCollection;

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
    }
}
