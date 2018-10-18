using EnumCollection;

namespace Common.Building
{
    public class Radar : Construct
    {
        protected override void Start()
        {
            Id = ConstructId.Radar;
            base.Start();
        }
    }
}