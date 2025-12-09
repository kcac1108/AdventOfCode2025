
namespace Days.Day02;

public class Solution : BaseSolution
{
    private static readonly string Input = File.ReadAllText("Input Files/02.txt");
    private static readonly string[] RawRanges = Input.Split(',');

    public override object RunPart1()
    {
        var ranges = RawRanges
            .Select(rawRange =>
            {
                var numbers = rawRange.Split('-');
                return (First: long.Parse(numbers[0]), Last: long.Parse(numbers[1])); 
            })
            .ToList();
        
        var invalidIds = new List<long>();

        foreach (var range in ranges)
        {
            for (var id = range.First; id <= range.Last; id++)
            {
                var idText = id.ToString();

                if (idText.Length % 2 != 0)
                {
                    continue;
                }

                var halfSize = idText.Length / 2; 
                
                var firstHalf = idText[..halfSize];
                var secondHalf = idText[halfSize..];

                if (firstHalf == secondHalf)
                {
                    invalidIds.Add(id);
                }
            }
        }

        return invalidIds.Sum();
    }

    public override object RunPart2()
    {
        var ranges = RawRanges
            .Select(rawRange =>
            {
                var numbers = rawRange.Split('-');
                return (First: long.Parse(numbers[0]), Last: long.Parse(numbers[1])); 
            })
            .ToList();
        
        var invalidIds = new List<long>();

        foreach (var (first, last) in ranges)
        {
            for (var id = first; id <= last; id++)
            {
                var idText = id.ToString();

                for (var size = 1; size <= idText.Length / 2; size++)
                {
                    var pattern = idText[..size];
                    var sequence = pattern;

                    if (idText.Length % size != 0)
                    {
                        continue;
                    }

                    do
                    {
                        sequence += pattern;
                    } while (sequence.Length < idText.Length);

                    if (sequence != idText) continue;
                    invalidIds.Add(id);
                    break;
                }
            }
        }

        return invalidIds.Sum();
    }

}
