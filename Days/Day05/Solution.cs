namespace Days.Day05;

public class Solution : BaseSolution
{
    private static readonly string Input = File.ReadAllText("Input Files/05.txt").Replace("\r", "");
    private static readonly string[] Lines = Input.Split('\n');
    private static readonly int BlankLineIndex = Array.IndexOf(Lines, string.Empty);
    private static readonly string[] FreshRanges = Lines[..BlankLineIndex];
    private static readonly string[] IdLines = Lines[(BlankLineIndex + 1)..];

    public override object RunPart1()
    {
        var ids = IdLines.Select(long.Parse).ToList();
        var ranges = FreshRanges
            .Select(line => line.Trim().Split('-'))
            .Select(parts => (Start: long.Parse(parts[0]), End: long.Parse(parts[1])))
            .ToList();
        
        return ids.Count(id => ranges.Any(range => Contains(id, range.Start,  range.End)));
    }
    
    private static bool Contains(long value, long start, long end) => value >= start && value <= end;

    public override object RunPart2()
    {
        var sortedRanges = FreshRanges
            .Select(line => line.Trim().Split('-'))
            .Select(parts => (Start: long.Parse(parts[0]), End: long.Parse(parts[1])))
            .OrderBy(r => r.Start)
            .ToList();
        
        long totalCount = 0;
    
        var currentStart = sortedRanges[0].Start;
        var currentEnd = sortedRanges[0].End;

        for (var i = 1; i < sortedRanges.Count; i++)
        {
            var next = sortedRanges[i];
            
            if (next.Start <= currentEnd + 1) 
            {
                currentEnd = Math.Max(currentEnd, next.End);
            }
            else
            {
                totalCount += (currentEnd - currentStart + 1);

                currentStart = next.Start;
                currentEnd = next.End;
            }
        }

        totalCount += (currentEnd - currentStart + 1);

        return totalCount;
    }
}