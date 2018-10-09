using Common;
using EnumCollection;

public class Radar : Construct
{
    public override void Produce() { }

    protected override void Start()
    {
        Id = ConstructId.Radar;
        IsUsePower = true;
        IsActive = true;
        ConsumePower = 20;       
        base.Start();
    }
}