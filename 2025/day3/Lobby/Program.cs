using System;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = "./example-puzzle-input.txt";
        // string filePath = "./example-puzzle-input.txt";
        string[] lines = Array.Empty<string>();

        if (File.Exists(filePath))
        {
            lines = File.ReadAllLines(filePath);
        }
        else
        {
            Console.WriteLine("File not found");
        }

        BatteryScanner batteryScanner = new BatteryScanner();
        Log log = new Log();

        foreach (string line in lines)
        {
            string sequence = batteryScanner.CheckMaximumJoltage(line, true);
            log.AddSequenceString(sequence);
        }

        log.PrintOutSequenceList();
        log.CalculateAllNumbers();
    }
}

class BatteryScanner
{
    public string CheckMaximumJoltage(string inputString, bool secondPart)
    {
        Dictionary<int, int> numbersAndPositions = new Dictionary<int, int>();
        List<int> collectHighNumbers = new List<int>();
        List<int> allValues = inputString.Select(c => int.Parse(c.ToString())).ToList();

        var normalizedStr = "";
        if (!secondPart)
        {
            collectHighNumbers.Add(allValues.Max());
            for (int i = 0; i < allValues.Count(); i++)
            {
                int count = allValues.Count() - (i + 1);
                if (count > 0)
                {
                    List<int> lastPart = allValues.GetRange(i + 1, count);
                    collectHighNumbers.Add(lastPart.Max());
                }
            }
            string rawResult = string.Join("", collectHighNumbers);
            normalizedStr = this.NormalizeStrinBacktoSingleSequenceNumber(rawResult);
            if (normalizedStr[0].Equals(normalizedStr[1]))
            {
                var indexMax = Array.IndexOf(allValues.ToArray(), allValues.Max());
                if (indexMax.Equals(allValues.Count() - 1))
                {
                    var firstPart = allValues.GetRange(0, allValues.Count() - 1);
                    normalizedStr = firstPart.Max().ToString() + normalizedStr[1];
                }
                else
                {
                    var lastPartAfterIndex = allValues.GetRange(
                        indexMax + 1,
                        allValues.Count() - indexMax - 1
                    );
                    normalizedStr = normalizedStr[0] + lastPartAfterIndex.Max().ToString();
                }
            }
        }
        else
        {
            int i = 0;
            while (true)
            {
                if (i >= allValues.Count - 1)
                {
                    if (collectHighNumbers.Count < 12)
                    {
                        Console.WriteLine("Addition1");
                        collectHighNumbers.Add(allValues[i]);
                    }
                    break;
                }

                if (
                    allValues.GetRange(i, allValues.Count - i).Count < 12
                    && collectHighNumbers.Count < 12
                )
                {
                    Console.WriteLine("Addition2 : " + allValues[i]);
                    collectHighNumbers.Add(allValues[i]);
                    i++;
                    continue;
                }

                bool isBiggerThenNext = allValues[i] > allValues[i + 1];
                if (isBiggerThenNext)
                {
                    Console.WriteLine("Addition3");
                    collectHighNumbers.Add(allValues[i]);
                }

                i++;
            }
            string joined = string.Join("", collectHighNumbers);
            Console.WriteLine(joined);
            normalizedStr = joined;
        }

        return normalizedStr;
    }

    private string NormalizeStrinBacktoSingleSequenceNumber(string input)
    {
        List<int> allChars = input.Select(c => int.Parse(c.ToString())).ToList();

        int best = allChars.Max();
        int secondBest = allChars.OrderByDescending(n => n).Skip(1).First();
        return best.ToString() + secondBest.ToString();
    }
}

class Log
{
    private List<string> sequenceList = new List<string>();

    public void AddSequenceString(string inputString)
    {
        sequenceList.Add(inputString);
    }

    public void PrintOutSequenceList()
    {
        foreach (string sequence in this.sequenceList)
        {
            Console.WriteLine(sequence);
        }
    }

    public void CalculateAllNumbers()
    {
        long result = 0;
        foreach (string sequence in sequenceList)
        {
            result += long.Parse(sequence);
        }
        Console.WriteLine(result);
    }
}
