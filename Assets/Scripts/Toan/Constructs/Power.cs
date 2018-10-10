using EnumCollection;
using Manager;

namespace Common.Building
{
    public class Power : Construct
    {
        public int PowerVolume { get; protected set; }

        protected override void Start()
        {
            Id = ConstructId.Power;
            PowerVolume = 10;
            ConsumePower = 0;
            IsActive = true;
            IsUsePower = false;
            base.Start();
        }

        public override void Build()
        {
            GlobalGameStatus.PowerBuilded(this);
            base.Build();
        }

        public override void DestroyConstruct()
        {
            GlobalGameStatus.PowerBuildDestroyed(this);
            base.DestroyConstruct();
        }

    }
}
