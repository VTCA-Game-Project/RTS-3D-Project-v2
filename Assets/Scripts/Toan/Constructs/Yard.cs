using EnumCollection;

namespace Common.Building
{
    public class Yard : Construct
    {

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
