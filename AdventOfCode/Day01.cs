using System.Linq;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly ICollection<string> _input;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => new($"{_input
    .Select(x =>
    {
        var firstCharNumber = x.First(x => ((short)x) is > 47 and < 58);
        var lastCharNumber = x.Last(x => ((short)x) is > 47 and < 58);

        var combinedString = $"{firstCharNumber}{lastCharNumber}";
        var total = int.Parse(combinedString);
        return total;
    }).Sum()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");
}