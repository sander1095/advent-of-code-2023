using System.Linq;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly ICollection<string> _input;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var possibleGames = new List<int>();
        foreach (var game in _input) //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        {
            List<(int Amount, CubeColor Color)> amountWithColors = [];

            var seperated = game.Split(':'); // ["Game 1", " 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
            var gameIdentifier = int.Parse(seperated.First().Split(' ').Last()); // 1

            var cubeSetInfo = seperated.Last(); // 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            foreach (var set in cubeSetInfo.Split(';')) // foreach set in ["3 blue", "4 red"]
            {
                var rawAmountWithColors = set.Split(',')
                    .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)); // "3", "blue"

                foreach (var amountWithColor in rawAmountWithColors)
                {
                    var amount = int.Parse(amountWithColor.First());
                    var color = Enum.Parse<CubeColor>(amountWithColor.Last(), true);
                    amountWithColors.Add((amount, color));
                }
            }

            if (!amountWithColors.Where(x => x.Color == CubeColor.Green).Any(x => x.Amount > 13) && !amountWithColors.Where(x => x.Color == CubeColor.Blue).Any(x => x.Amount > 14) && !amountWithColors.Where(x => x.Color == CubeColor.Red).Any(x => x.Amount > 12))
            {
                possibleGames.Add(gameIdentifier);
            }
        }

        return new(possibleGames.Sum().ToString());
    }

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");

    public enum CubeColor
    {
        Red,
        Blue,
        Green
    }
}