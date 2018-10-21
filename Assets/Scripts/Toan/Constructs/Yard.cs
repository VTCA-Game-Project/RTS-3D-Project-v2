using EnumCollection;

namespace Common.Building
{
    public class Yard : Construct
    {
        protected override void Start()
        {
            Id = ConstructId.Yard;    
            base.Start();
        }
    }
}
