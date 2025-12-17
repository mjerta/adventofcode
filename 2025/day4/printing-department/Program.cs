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

        ForkLiftHandler forkLiftHandler = new ForkLiftHandler(lines);
        forkLiftHandler.RegisterPaperPositions();
    }
}

class ForkLiftHandler
{
    string[] lines;
    int amountOfLines;
    Dictionary<int, int> gridPositions = new Dictionary<int, int>();

    public ForkLiftHandler(string[] lines)
    {
        this.amountOfLines = lines.Count();
        this.lines = lines;
    }

    public void RegisterPaperPositions()
    {
        for (int i = 0; i < this.amountOfLines; i++)
        {
            for (int j = 0; j < this.lines[i].Count(); j++)
            {
                if (this.lines[i][j].ToString().Equals("@"))
                {
                    // perhaps I could directly test differnt positions here
                    bool toTheLeft = j > 0 && this.lines[i][j - 1].ToString().Equals("@");
                    bool toTheTopLeft =
                        i > 0 && j > 0 && this.lines[i - 1][j - 1].ToString().Equals("@");
                    bool toTheTop = i > 0 && this.lines[i - 1][j].ToString().Equals("@");
                    bool toTheTopRight =
                        i < 0
                        && j < this.lines[i].Count()
                        && this.lines[i - 1][j + 1].ToString().Equals("@");
                    bool toTheRight =
                        j < this.lines[i].Count() && this.lines[i][j + 1].ToString().Equals("@");
                    bool toTheBottomRight =
                        i < this.lines.Count()
                        && j < this.lines[i].Count()
                        && this.lines[i + 1][j + 1].ToString().Equals("@");
                    bool toTheBottom =
                        i < this.lines.Count() && this.lines[i + 1][j].ToString().Equals("@");
                    bool toTheBottomLeft =
                        i < this.lines.Count()
                        && j > 0
                        && this.lines[i + 1][j - 1].ToString().Equals("@");
                    // perhaps i Should refactor this above and put it in a list loop voer it.
                    // With a while loop and sort ciruit when 4 are foud or not found
                    // Most importantly is that the point is that the true position is when there a fewer than four rolls of paper in the eight adjacent positions
                }
            }
        }
    }

    // public int TakePosition()
    // {
    //     foreach (KeyValuePair<int, int> position in gridPositions)
    //     {
    //       // while loop
    //       // break out when 4 position found
    //       // position horizontal
    //       // postion diagonal
    //       // positon vertical
    //       int foundEntries = 0;
    //       while(foundEntries < 4)
    //       {
    //
    //       }
    //     }
    //     return 0;
    // }
}
