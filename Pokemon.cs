using MudBlazor;
using System.Drawing;

namespace PokeFight;

public class Pokemon
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageName { get; set; }

    public required int Defence { get; set; }
    public required int Attack { get; set; }
    public required EElement Element { get; set; }

    public required string Colour { get; set; }

    public required List<Move> Moves { get; set; }

    public int HitPoints { get; set; } = 100;
}

public enum EElement
{
    Fire,
    Grass,
    Water,
    Electric,
    Normal
}


