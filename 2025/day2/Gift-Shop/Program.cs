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
        CrackManager crackManager = new CrackManager();
        crackManager.splitTheString(inputLine);
        crackManager.getRanges()
          .ForEach(range => Console.WriteLine(range));

    }
}


class CrackManager
{
    private List<string> range;

    public CrackManager()
    {
        this.range = new List<string>();
    }


    public void splitTheString(string input)
    {
        this.range = input.Split(",").ToList();
    }

    public List<string> getRanges() {
      return this.range;
    }
}
