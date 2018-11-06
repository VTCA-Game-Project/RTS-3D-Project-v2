
namespace EnumCollection
{
    public enum ConstructId
    {
        Refinery,
        Barrack,
        Radar,
        Yard,
        Defender
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

    public enum Soldier
    {
        Magic,
        Warrior,
        Archer,
        WoodHorse,
        HumanWarrior,
        OrcTanker
    }

    public enum AnimState
    {
        Run,
        Dead,
        Idle,
        Damage,
        Attack,
        None
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
        UI,
        None
    }
    public enum GameStatus
    {
        Win,
        Lose,
        Playing
    }
}

