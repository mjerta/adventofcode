class Program
{
    static void Main()
    {
        // string filePath = "./example-puzzle-input.txt";
        string filePath = "./puzzle-input.txt";
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
        int amountFound = forkLiftHandler.getLogger().getGrabbablePositions();
        Console.WriteLine(amountFound);
    }
}

class ForkLiftHandler
{
    string[] lines;
    int amountOfLines;
    Logger logger = new Logger();

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
                    bool bool1 = j > 0 && this.lines[i][j - 1].ToString().Equals("@");
                    bool bool2 = i > 0 && j > 0 && this.lines[i - 1][j - 1].ToString().Equals("@");
                    bool bool3 = i > 0 && this.lines[i - 1][j].ToString().Equals("@");
                    bool bool4 =
                        i > 0
                        && j < this.lines[i].Count() - 1
                        && this.lines[i - 1][j + 1].ToString().Equals("@");
                    bool bool5 =
                        j < this.lines[i].Count() - 1
                        && this.lines[i][j + 1].ToString().Equals("@");
                    bool bool6 =
                        i < this.lines.Count() - 1
                        && j < this.lines[i].Count() - 1
                        && this.lines[i + 1][j + 1].ToString().Equals("@");
                    bool bool7 =
                        i < this.lines.Count() - 1 && this.lines[i + 1][j].ToString().Equals("@");
                    bool bool8 =
                        i < this.lines.Count() - 1
                        && j > 0
                        && this.lines[i + 1][j - 1].ToString().Equals("@");
                    bool[] neighbours = { bool1, bool2, bool3, bool4, bool5, bool6, bool7, bool8 };
                    int trueCount = neighbours.Count(b => b);
                    if (trueCount < 4)
                    {
                        this.logger.addGrabbablePosition();
                    }
                }
            }
        }
    }

    public Logger getLogger()
    {
        return this.logger;
    }
}

class Logger
{
    private int grabablePositions = 0;

    public int getGrabbablePositions()
    {
        return this.grabablePositions;
    }

    public void addGrabbablePosition()
    {
        this.grabablePositions++;
    }
}
