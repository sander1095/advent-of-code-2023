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
        char firstChar = '0';
        char lastChar = '0';
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] is >= '0' and <= '9')
            {
                firstChar = x[i];
                break;
            }
        }

        for (int i = x.Length - 1; i >= 0; i--)
        {
            if (x[i] is >= '0' and <= '9')
            {
                lastChar = x[i];
                break;
            }
        }

        var combinedString = $"{firstChar}{lastChar}";
        var total = int.Parse(combinedString);
        return total;
    }).Sum()}");

    public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");
}