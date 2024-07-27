namespace PokeFight;

public static class Moves
{
    public static Move Swift =>
        new() { Name = "Swift", Accuracy = 1, Element = EElement.Normal, Power = 3 };

    public static Move Thunderbolt =>
        new() { Name = "Thunderbolt", Accuracy = 0.75f, Element = EElement.Electric, Power = 6 };

    public static Move Tackle =>
        new() { Name = "Tackle", Accuracy = 0.95f, Element = EElement.Normal, Power = 3 };

    public static Move WaterCannon =>
        new() { Name = "Water Cannon", Accuracy = 0.75f, Element = EElement.Water, Power = 6 };

    public static Move Flamethrower =>
        new() { Name = "Flamethrower", Accuracy = 0.75f, Element = EElement.Fire, Power = 6 };

    public static Move SeedStorm =>
        new() { Name = "Seed Storm", Accuracy = 0.75f, Element = EElement.Grass, Power = 6 };
}

public class Move
{
    public required string Name { get; set; }
    public required EElement Element { get; set; }
    public required int Power { get; set; }
    public required float Accuracy { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Element})";
    }
}

public enum EModifier
{
    NoEffect, //0
    NotEffective, //0.5
    Effective, //1
    SuperEffective, //2
}
