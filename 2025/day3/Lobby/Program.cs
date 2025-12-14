using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = "./example-puzzle-input.txt";
        // string filePath = "./puzzle-input.txt";
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
        Console.WriteLine("");
        ;
        Console.WriteLine("The result is:");
        ;
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

        bool isHighNumbersComplete()
        {
            return collectHighNumbers.Count >= 12;
        }
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
            // method that get me splice and start with  the highest number and at least have 12 number left
            List<int> numberToCheck = GetSpliceFromMax(allValues, 12);
            while (true)
            {
                if (i >= numberToCheck.Count - 1)
                {
                    if (!isHighNumbersComplete())
                    {
                        // Console.WriteLine("Addition1 :" + numberToCheck[i]);
                        collectHighNumbers.Add(numberToCheck[i]);
                    }
                    break;
                }

                // This is looking if there at least 12 number to work with
                // TODO: I need to combine it with the count of collectHighNumbers somehow
                // Lets print out what the range is im using in the condition below
                int rangeLeft = numberToCheck.GetRange(i, numberToCheck.Count - i).Count;
                // Console.WriteLine("range left :" + rangeLeft);
                int amountOfNumbersNeeded = rangeLeft + collectHighNumbers.Count;
                // Console.WriteLine("range left minus CollectedNumbers :" + amountOfNumbersNeeded);
                if (amountOfNumbersNeeded <= 12)
                // if ( numberToCheck.GetRange(i, numberToCheck.Count - i).Count < 12)
                {
                    if (isHighNumbersComplete())
                    {
                        break;
                    }
                    // Console.WriteLine("Addition2 : " + numberToCheck[i]);
                    collectHighNumbers.Add(numberToCheck[i]);
                    i++;
                    continue;
                }

                bool isBiggerThenNext = numberToCheck[i] > numberToCheck[i + 1];
                if (isBiggerThenNext)
                {
                    if (isHighNumbersComplete())
                    {
                        break;
                    }
                    // Console.WriteLine("Addition3 :" + numberToCheck[i]);
                    collectHighNumbers.Add(numberToCheck[i]);
                }

                i++;
            }
            string joined = string.Join("", collectHighNumbers);
            Console.WriteLine(joined);
            normalizedStr = joined;
        }

        return normalizedStr;
    }

    private List<int> GetSpliceFromMax(List<int> sequence, int minLength)
    {
        List<int> spliceFound = new List<int>();
        List<int> hightToLow = new List<int>();
        hightToLow = sequence.OrderByDescending(n => n).ToList();

        int i = 0;
        while (true)
        {
            int maxNumber = hightToLow[i];
            int indexOfMax = sequence.IndexOf(maxNumber);

            spliceFound = sequence.GetRange(indexOfMax, sequence.Count - indexOfMax).ToList();
            int remaning = spliceFound.Count;
            // Console.WriteLine((remaning));
            if (remaning >= 12)
            {
                break;
            }
            i++;
        }
        return spliceFound;
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
        BigInteger result = 0;
        foreach (string sequence in sequenceList)
        {
            result += BigInteger.Parse(sequence);
        }
        Console.WriteLine(result);
    }
}
