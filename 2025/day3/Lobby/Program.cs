  using System;

  class Program
  {
      static void Main()
      {
          string filePath = "./puzzle-input.txt";
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
              string sequence = batteryScanner.CheckMaximumJoltage(line);
              log.AddSequenceString(sequence);
          }

          log.PrintOutSequenceList();
          log.CalculateAllNumbers();
      }
  }

  class BatteryScanner
  {

      public string CheckMaximumJoltage(string inputString)
      {
          string concatString = "";
          List<int> collectHighNumbers = new List<int>();
          int highestNumber;
          List<int> allValues = inputString
            .Select(c => int.Parse(c.ToString()))
            .ToList();

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
          var normalizedStr = this.NormalizeStrinBacktoSingleSequenceNumber(rawResult);
          if (normalizedStr[0].Equals(normalizedStr[1]))
          {
              // check for if largest number is end of string
              // if that is the case the next number before the max should come in front
              //
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

              // other if the max was not at the end
              // the string should be spliced after the max value is found
              // this string should be concatenated after the string
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
        int result = 0;
        foreach(string sequence in sequenceList){
          result += int.Parse(sequence);
        }
        Console.WriteLine(result);
      }
  }
