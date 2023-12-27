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
        foreach (var game in _input)
        {
            var seperated = game.Split(':');
            var cubeSetInfo = seperated.Last();
            var gameIdentifier = int.Parse(seperated.First().Split(' ').Last());

            List<(int Amount, CubeColor Color)> shownAmountWithCubes = [];
            foreach (var set in cubeSetInfo.Split(';'))
            {
                foreach (var thing in set.Split(',').Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)))
                {
                    shownAmountWithCubes.Add((int.Parse(thing.First()), Enum.Parse<CubeColor>(thing.Last(), true)));
                }
            }

            var totalAmountOfGreen = shownAmountWithCubes.Where(x => x.Color == CubeColor.Green).Sum(x => x.Amount);
            var totalAmountOfRed = shownAmountWithCubes.Where(x => x.Color == CubeColor.Red).Sum(x => x.Amount);
            var totalAmountOfBlue = shownAmountWithCubes.Where(x => x.Color == CubeColor.Blue).Sum(x => x.Amount);

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