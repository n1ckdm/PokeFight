namespace PokeFight;

public class BattleService
{
    public readonly Pokemon PokemonA;
    public readonly Pokemon PokemonB;

    public bool PokemonATurn { get; private set; } = true;

    public BattleService()
    {
        PokemonA = new Pokemon()
        {
            Name = "Teicho",
            Description = "Fire type pokemon who is curious and will fight more for it's trainer than most other pokemon, very loyal",
            ImageName = "teicho.webp",
            Attack = 70,
            Defence = 60,
            Element = EElement.Fire,
            Colour = "red-text text-darken-2",
            Moves = [Moves.Tackle, Moves.Flamethrower]
        };

        PokemonB = new Pokemon()
        {
            Name = "Hydrochu",
            Description = "Water type pokemon, loves to wear waterproof makeup to keep itself looking cute event when swimming",
            ImageName = "hydrochu.webp",
            Attack = 60,
            Defence = 90,
            Element = EElement.Water,
            Colour = "blue-text text-lighten-2",
            Moves = [Moves.Swift, Moves.WaterCannon]
        };
    }

    public static float ModifierValue(EModifier modifier) => modifier switch
    {
        EModifier.NoEffect => 0,
        EModifier.Effective => 1,
        EModifier.NotEffective => 0.5f,
        EModifier.SuperEffective => 2,
        _ => throw new NotImplementedException()
    };

    public static EModifier GetModifier(EElement attElement, EElement defElement)
    {
        if (attElement is EElement.Normal)
        {
            return EModifier.Effective;
        }
        else if (attElement is EElement.Fire)
        {
            return defElement switch
            {
                EElement.Fire => EModifier.NotEffective,
                EElement.Water => EModifier.NotEffective,
                EElement.Grass => EModifier.SuperEffective,
                _ => EModifier.Effective
            };
        }
        else if (attElement is EElement.Water)
        {
            return defElement switch
            {
                EElement.Fire => EModifier.SuperEffective,
                EElement.Water => EModifier.NotEffective,
                EElement.Grass => EModifier.NotEffective,
                _ => EModifier.Effective
            };
        }
        else if (attElement is EElement.Grass)
        {
            return defElement switch
            {
                EElement.Fire => EModifier.NotEffective,
                EElement.Water => EModifier.SuperEffective,
                EElement.Grass => EModifier.NotEffective,
                _ => EModifier.Effective
            };
        }
        else if (attElement is EElement.Electric)
        {
            return defElement switch
            {
                EElement.Water => EModifier.SuperEffective,
                EElement.Grass => EModifier.SuperEffective,
                EElement.Electric => EModifier.NotEffective,
                _ => EModifier.Effective
            };
        }
        else
        {
            return EModifier.Effective;
        }
    }

    private AttackResult GetAttackResult(Pokemon attacker, Pokemon defender, Move move)
    {
        var rng = new Random();
        var elementMod = GetModifier(move.Element, defender.Element);

        // Check if the attack will hit or miss
        if (move.Accuracy < rng.NextDouble())
        {
            return new AttackResult(
                Hp: 0,
                Hit: false,
                Critical: false,
                Modifier: elementMod
            );
        }

        var elementModVal = ModifierValue(elementMod);

        var critical = rng.NextDouble() < 0.05 ? 2 : 1;

        var hp = ((move.Power * attacker.Attack) / defender.Defence) * critical * elementModVal;
        defender.HitPoints -= (int)hp;
        return new AttackResult(
            Hp: (int)hp,
            Hit: true,
            Critical: critical > 1,
            Modifier: elementMod
        );
    }

    public void DoTurn(Move move)
    {
        // Calculate the hit points for the move
        var result = GetAttackResult(
            PokemonATurn ? PokemonA : PokemonB,
            PokemonATurn ? PokemonB : PokemonA,
            move
        );

        PokemonATurn = !PokemonATurn;
    }
}

public record AttackResult(int Hp, bool Hit, bool Critical, EModifier Modifier);
