namespace Days.Day01;

public class Solution : BaseSolution
{
    private const int NumberCount = 100;
    private static readonly Input Input = new(File.ReadAllText("Input Files/01.txt"));

    public override object RunPart1()
    {
        var currentPointer = 50;
        var timesReachedZero = 0;

        foreach (var line in Input.Lines)
        {
            var direction = line[0].ToString();
            var amount = int.Parse(line[1..]);

            if (direction == "L")
            {
                currentPointer = (currentPointer - amount + NumberCount) % NumberCount;
            }
            else
            {
                currentPointer = (currentPointer + amount + NumberCount) % NumberCount;
            }

            if (currentPointer == 0)
            {
                timesReachedZero++;
            }
        }
        return timesReachedZero;
    }

    public override object RunPart2()
    {
        var currentPointer = 50;
        var timesPassedZero = 0;

        foreach (var line in Input.Lines)
        {
            var direction = line[0].ToString();
            var amount = int.Parse(line[1..]);

            var increment = direction == "L" ? -1 : 1;

            for (var i = 0; i < amount; i++)
            {
                currentPointer = (currentPointer + increment + NumberCount) % NumberCount;
                if (currentPointer == 0)
                {
                    timesPassedZero++;
                }
            }
        }
        return timesPassedZero;
    }

}
