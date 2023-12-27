using System.Linq;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly IList<string> _input;
    public int FirstLineIndex = 0;
    public int LastLineIndex;

    public int LineStartIndex = 0;
    public int LineEndIndex;

    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath);
        LastLineIndex = _input.Count - 1;
        LineEndIndex = _input[0].Length - 1;
    }

    public override ValueTask<string> Solve_1()
    {
        var partNumbers = new List<int>();
        for (int i = 0; i < _input.Count; i++)
        {
            var line = _input[i];

            if (!line.ContainsSpecialCharacters())
            {
                continue;
            }

            for (int j = 0; j < line.Length; j++)
            {
                var character = line[j];
                if (!character.IsSpecialCharacter())
                {
                    continue;
                }

                // We found a special character
                // Now we need to look on the same line before and after the character for numbers
                // and on the line above and below for numbers, above, below and diagonally

                var topLineSearchIsSafe = i != FirstLineIndex;
                var bottomLineSearchIsSafe = i != LastLineIndex;
                var leftSearchIsSafe = j != LineStartIndex;
                var rightSearchIsSafe = j != LineEndIndex;

                if (leftSearchIsSafe)
                {
                    Stack<char> numbers = [];
                    var nextIndexToSearch = j - 1;
                    while (Char.IsNumber(line[nextIndexToSearch]))
                    {
                        numbers.Push(line[nextIndexToSearch]);
                        if (nextIndexToSearch - 1 < LineStartIndex)
                        {
                            break;
                        }
                        nextIndexToSearch--;
                    }
                    if (numbers.Count > 0)
                    {
                        partNumbers.Add(int.Parse(string.Join("", numbers)));
                    }
                }
                if (rightSearchIsSafe)
                {
                    Queue<char> numbers = [];
                    var nextIndexToSearch = j + 1;
                    while (Char.IsNumber(line[nextIndexToSearch]))
                    {
                        numbers.Enqueue(line[nextIndexToSearch]);
                        if (nextIndexToSearch + 1 > LineEndIndex)
                        {
                            break;
                        }
                        nextIndexToSearch++;
                    }
                    if (numbers.Count > 0)
                    {
                        partNumbers.Add(int.Parse(string.Join("", numbers)));
                    }
                }

                // TODO: Top and diagonal searches

                //var numbersAsChars = line.Skip(j).TakeWhile(Char.IsNumber);
                //var fullNumber = int.Parse(string.Join("", numbersAsChars));
            }
        }

        
        return new(partNumbers.Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new("TODO");
    }


    private bool HasSpecialChars(string yourString)
    {
        return yourString.Any(ch => !char.IsLetterOrDigit(ch) && ch is not '.');
    }
}

public static class CharExtensions
{
    public static bool IsSpecialCharacter(this char c) => !char.IsLetterOrDigit(c) && c is not '.';
}

public static class StringExtensions
{
    public static bool ContainsSpecialCharacters(this string s) => s.Any(c => c.IsSpecialCharacter());
}