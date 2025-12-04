using System;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "./example-puzzle-input.txt";
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
        crackManager.getRanges()
          .ForEach(range => Console.WriteLine(range));

        int i = 0;
        crackManager.getAllCombinations()
          .ForEach(listOfRanges =>
          {
              i++;
              listOfRanges
              .ForEach(number =>
              {
                  // string validOrNot = crackManager.isInvalidNumber(number) ? "invalid" : "valid";
                  if(crackManager.isInvalidNumber(number)) {
                    
                  }
                 // Console.WriteLine("This is the number of " + i + ": " + number + " and is a " + validOrNot + " number");
              });
          });
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

    public List<List<int>> getAllCombinations()
    {
        return this.range.Select(range =>
        {
            string[] parts = range.Split("-");
            int start = int.Parse(parts[0]);
            int end = int.Parse(parts[1]);
            return Enumerable.Range(start, end - start + 1).ToList();
        })
        .ToList();
    }

    public bool isInvalidNumber(int number)
    {
        string str = number.ToString();
        if (str.Length > 1 && str.Length % 2 == 0)
        {
          int indexOfTheMiddle = str.Length / 2;
          string firstPart = str.Substring(0,indexOfTheMiddle);
          string secondPart = str.Substring(indexOfTheMiddle);
          if(firstPart.Equals(secondPart)) return true;
        }
        return false;
    }
}


class Score
{
  private List<int> invalidNumbers;

  public Score() {
    this.invalidNumbers = new List<int>();
  }

  public void addNumber(int inputNumber) {
    this.invalidNumbers.Add(inputNumber);
  }


  public List<int> getInvalidNumbers() {
    return this.invalidNumbers;
  }
}
