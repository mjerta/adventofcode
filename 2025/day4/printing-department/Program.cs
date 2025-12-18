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
        int amountFOund = forkLiftHandler.getLogger().getAccesPositions();
        Console.WriteLine(amountFOund);
    }
}

class ForkLiftHandler
{
    string[] lines;
    int amountOfLines;
    Dictionary<int, int> gridPositions = new Dictionary<int, int>();
    Logger logger = new Logger();
    Movement movement;

    public ForkLiftHandler(string[] lines)
    {
        this.amountOfLines = lines.Count();
        this.lines = lines;
        this.movement = new Movement(lines);
    }

    public void RegisterPaperPositions()
    {
        for (int i = 0; i < this.amountOfLines; i++)
        {
            for (int j = 0; j < this.lines[i].Count(); j++)
            {
                if (this.lines[i][j].ToString().Equals("@"))
                {
                    this.movement.registerDirection(i, j);
                }
            }
        }
        int k = 0;
        while (this.logger.getAccesPositions() < 3)
        {
            if (this.movement.getListOfDirections()[k])
            {
                this.logger.registerGrabbalbePaper(j, i);
            }
            k++;
        }
    }

    public Logger getLogger()
    {
        return this.logger;
    }
}

class Logger
{
    private int accessPositions = 0;
    private Dictionary<int, List<int>> actualPosition = new Dictionary<int, List<int>>();

    public void registerGrabbalbePaper(int x, int y)
    {
        accessPositions++;
        if (!actualPosition.ContainsKey(y))
        {
            actualPosition[y] = new List<int>();
        }
        actualPosition[y].Add(x);
    }

    public int getAccesPositions()
    {
        return this.accessPositions;
    }

    public Dictionary<int, List<int>> getActualPostions()
    {
        return this.actualPosition;
    }
}

class Movement
{
    private List<bool> listOfDirection = new List<bool>();
    private string[] lines;

    public Movement(string[] lines)
    {
        this.lines = lines;
    }

    public void registerDirection(int i, int j)
    {
        this.listOfDirection.Add(j > 0 && this.lines[i][j - 1].ToString().Equals("@"));
        this.listOfDirection.Add(i > 0 && j > 0 && this.lines[i - 1][j - 1].ToString().Equals("@"));
        this.listOfDirection.Add(i > 0 && this.lines[i - 1][j].ToString().Equals("@"));
        this.listOfDirection.Add(
            i > 0
                && j < this.lines[i].Count() - 1
                && this.lines[i - 1][j + 1].ToString().Equals("@")
        );
        this.listOfDirection.Add(
            j < this.lines[i].Count() - 1 && this.lines[i][j + 1].ToString().Equals("@")
        );
        this.listOfDirection.Add(
            i < this.lines.Count() - 1
                && j < this.lines[i].Count() - 1
                && this.lines[i + 1][j + 1].ToString().Equals("@")
        );
        this.listOfDirection.Add(
            i < this.lines.Count() - 1 && this.lines[i + 1][j].ToString().Equals("@")
        );
        this.listOfDirection.Add(
            i < this.lines.Count() - 1 && j > 0 && this.lines[i + 1][j - 1].ToString().Equals("@")
        );
    }

    public List<bool> getListOfDirections()
    {
        return this.listOfDirection;
    }
}
