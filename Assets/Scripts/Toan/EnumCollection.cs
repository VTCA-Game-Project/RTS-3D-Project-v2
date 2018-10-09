

namespace EnumCollection
{
    public enum ConstructId
    {
        Refinery,
        Barrack,
        War,
        Radar,
        Yard,
        Power,
        FarDefender,
        NearDefender,
    }

    public enum Deceleration
    {
        Fast = 1,
        Normal,
        Slow,
    }

    public enum PayGoldStatus
    {
        Terminal, // remain gold equal 0
        Success,  // success 
        Pause     // remain gold less than require gold
    }

    public enum Vehicle
    {
        GrizzlyTank,
    }

    public enum Soldier
    {
        Builder,
        Infantry
    }
}

