using System;

class Program
{
    static void Main(string[] args)
    {
        // string filePath = "./example-puzzle-input.txt";
        string filePath = "./puzzle-input.txt";
        string inputLine = "";

        if (File.Exists(filePath))
        {
            inputLine = File.ReadAllText(filePath);
        }
        else
        {
            Console.WriteLine("File not found");
        }


        Console.WriteLine(inputLine);
        CrackManager crackManager = new CrackManager(inputLine);
        Score score = new Score();
        crackManager.getRanges()
          .ForEach(range => Console.WriteLine(range));

        long i = 0;
        crackManager.getAllCombinations()
          .ForEach(listOfRanges =>
          {
              i++;
              listOfRanges
              .ForEach(number =>
              {
                  if (crackManager.isInvalidNumber(number))
                  {
                      score.addNumber(number);
                  }
              });
          });
        long sumOfInvalidNumbers = score.getInvalidNumbers()
          .Select(number =>
          {
              Console.WriteLine("The invalid number is :" + number);
              return number;
          })
        .Sum();

        Console.WriteLine("The total sum of invalid numbers is " + sumOfInvalidNumbers);
    }
}


class CrackManager
{
    private List<string> range;

    public CrackManager(string inputLine)
    {
        this.range = new List<string>();
        this.splitTheString(inputLine);
    }

    private void splitTheString(string input)
    {
        this.range = input.Split(",").ToList();
    }

    public List<string> getRanges()
    {
        return this.range;
    }

    public List<List<long>> getAllCombinations()
    {
        return this.range.Select(range =>
        {
            string[] parts = range.Split("-");
            long start = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);
            return getRangeLong(start, end).ToList();
        })
        .ToList();
    }

    private IEnumerable<long> getRangeLong(long start, long end)
    {
        for (long i = start; i <= end; i++)
        {
            yield return i;
        }
    }

    public bool isInvalidNumber(long number)
    {
        string str = number.ToString();
        string firstPart = str.Substring(0, str.Length / 2);
        string secondPart = str.Substring(str.Length / 2);
        return firstPart.Equals(secondPart);
    }
}


class Score
{
    private List<long> invalidNumbers;

    public Score()
    {
        this.invalidNumbers = new List<long>();
    }

    public void addNumber(long inputNumber)
    {
        this.invalidNumbers.Add(inputNumber);
    }


    public List<long> getInvalidNumbers()
    {
        return this.invalidNumbers;
    }
}
