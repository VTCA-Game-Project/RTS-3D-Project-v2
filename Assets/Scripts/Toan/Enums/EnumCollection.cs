
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
        Warrior,
        Archer,
        WarMachine,
        Magic,
        WolfKnight,
    }

    public enum AnimState
    {
        Run,
        Dead,
        Idle,
        Damage,
        Attack,
    }

    public enum Group
    {
        Player,
        NPC
    }

    public enum TargetType
    {
        Place,
        NPC,
        Construct,
        None
    }
    public enum GameStatus
    {
        Win,
        Lose,
        Playing
    }
}

