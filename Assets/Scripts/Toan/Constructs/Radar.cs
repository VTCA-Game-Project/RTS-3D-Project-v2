using EnumCollection;

namespace Common.Building
{
    public class Radar : Construct
    {

        protected override void Start()
        {
            Id = ConstructId.Radar;
            IsUsePower = true;
            IsActive = true;
            ConsumePower = 20;
            base.Start();
        }
    }
}