using System;
using System.Text.RegularExpressions;
using System.Linq;

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
        List<int> allValues = inputString
          .Select(c => int.Parse(c.ToString()))
          .ToList();

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
                    var lastPartAfterIndex = allValues.GetRange(indexMax + 1, allValues.Count() - indexMax - 1);
                    normalizedStr = normalizedStr[0] + lastPartAfterIndex.Max().ToString();
                }
            }
        }
        else
        {
            // first I want to see if i could get the largest number but also make sure it score high on the lowest position of the string
            // int highestNumber = allValues.Max();
            // int positionOfMax = Array.IndexOf(allValues.ToArray(), highestNumber);
            // numbersAndPositions.Add(positionOfMax, highestNumber);
            // var sortedDict = numbersAndPositions.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            // TODO - Need to put a foreach here stil
            //

            // string changingString = inputString;
            // for (int i = 0; i < inputString.Length; i++)
            // {
            //
            //     if (changingString.Length - 1 > 12)
            //     {
            //         changingString = inputString.Substring(i);
            //         Console.WriteLine(changingString);
            //     }
            //

            // Match all numbers
            MatchCollection matches = Regex.Matches(inputString, @"\d+");

            var numbers = matches.Cast<Match>()
                                 .Select(m => long.Parse(m.Value))
                                 .ToList();

            long maxNumber = numbers.Max();
            Console.WriteLine($"Highest number: {maxNumber}");
            normalizedStr = maxNumber.ToString();
        }


        return normalizedStr;
    }


    private string NormalizeStrinBacktoSingleSequenceNumber(string input)
    {
        List<int> allChars = input
          .Select(c => int.Parse(c.ToString()))
          .ToList();

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
