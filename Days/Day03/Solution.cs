
namespace Days.Day03;

public class Solution : BaseSolution
{
    private static readonly string Input = File.ReadAllText("Input Files/03.txt");
    private static readonly string[] Lines = Input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

    public override object RunPart1()
    {
        const int batteriesToPick = 2;

        return GetBankJoltages(batteriesToPick).Sum();
    }

    public override object RunPart2()
    {
        const int batteriesToPick = 12;

        return GetBankJoltages(batteriesToPick).Sum();
    }
    
    private static List<long> GetBankJoltages(int batteriesToPick)
    {
        var banks = Lines
            .Select(bank => bank
                .Select((joltage, index) => new Battery(joltage, index))
                .ToList())
            .ToList();
        var bankJoltages = banks
            .Select(bank =>
            {
                var biggestJoltageBattery = new Battery('\0', -1);
                var rawJoltage = string.Empty;

                for (var i = 0; i < batteriesToPick; i++)
                {
                    var subBank = bank[(biggestJoltageBattery.IndexInBank + 1)..^(batteriesToPick - 1 - i)];
                    var biggestJoltage = subBank.Max(battery => battery.Joltage)!;
                    biggestJoltageBattery = subBank.First(battery => battery.Joltage == biggestJoltage);
                    rawJoltage += biggestJoltageBattery.RawJoltage;
                }

                return long.Parse(rawJoltage);
            })
            .ToList();

        return bankJoltages;
    }


}
