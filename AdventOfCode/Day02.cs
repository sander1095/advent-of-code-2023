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

            var totalAmountOfGreen = amountWithColors.Where(x => x.Color == CubeColor.Green).Sum(x => x.Amount);
            var totalAmountOfRed = amountWithColors.Where(x => x.Color == CubeColor.Red).Sum(x => x.Amount);
            var totalAmountOfBlue = amountWithColors.Where(x => x.Color == CubeColor.Blue).Sum(x => x.Amount);

            if (totalAmountOfGreen <= 13 && totalAmountOfRed <= 12 && totalAmountOfBlue <= 14)
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