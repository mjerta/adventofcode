using System;

class Program
{
    static void Main()
    {
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


        // initialiing the classes
        TurnOperation turnOperation = new TurnOperation(50, 100);
        Log log = new Log();
        CrackManager crackManager = new CrackManager(turnOperation, log, lines);
        crackManager.runTheCrack();
        Console.WriteLine("Result " + turnOperation.getPassedByZero());
    }
}

class CrackManager
{
    private TurnOperation turnOperation;
    private Log log;
    private string[] lines;


    public CrackManager(TurnOperation turnOperation, Log log, string[] lines)
    {
        this.turnOperation = turnOperation;
        this.log = log;
        this.lines = lines;
    }

    public void runTheCrack()
    {
        Console.WriteLine("runTheCrack");
        foreach (string line in this.lines)
        {
            turnOperation.turnTheWheel(line);
            // this.log.addToList(numb);
        }
    }

}

class TurnOperation
{

    private string direction = "";
    private int maxValue;
    private int countingNumber = 0;
    private int position;
    private int passedByZero = 0;

    public TurnOperation(int initialPosition, int maxValue)
    {
        this.position = initialPosition;
        this.maxValue = maxValue;

    }

    public void turnTheWheel(string line)
    {
        this.setDirection(line);
        this.splitTheNumber(line);
        if (this.direction.Equals("left"))
        {
            for (int i = 0; i < this.countingNumber; i++)
            {
                this.position--;
                if (this.position.Equals(0))
                {
                    this.passedByZero++;
                }
                if (this.position.Equals(-1))
                {
                    this.position = 99;
                }

            }
        }
        else
        {
            for (int i = 0; i < this.countingNumber; i++)
            {
                this.position++;
                if (this.position.Equals(100))
                {
                    this.passedByZero++;
                }
                if (this.position.Equals(101))
                {
                    this.position = 1;
                }
            }
        }
    }

    public int getPassedByZero()
    {
        return this.passedByZero;
    }

    private void setDirection(string line)
    {
        if (line.Contains("L"))
        {
            this.direction = "left";
        }
        else if (line.Contains("R"))
        {
            this.direction = "right";
        }
    }

    private void splitTheNumber(string line)
    {
        string numberPart = line.Substring(1);
        this.countingNumber = int.Parse(numberPart);
    }

    private void calculatePosition()
    {
        if (direction.Equals("left"))
        {
            int substractNumber = this.devideTheNumberByMaxValue();
            int tempNumber = this.position - substractNumber;
            if (tempNumber < 0)
            {
                if (!this.position.Equals(0))
                {
                    this.passedByZero++;
                }
                this.position = this.maxValue - Math.Abs(tempNumber);
            }
            else
            {

                this.position = this.position - substractNumber;
            }

        }
        else
        {
            int additionNumber = this.devideTheNumberByMaxValue();
            int tempNumber = this.position + additionNumber;
            if (tempNumber > this.maxValue)
            {
                if (!this.position.Equals(0))
                {
                    this.passedByZero++;
                }
            }
            if (tempNumber >= this.maxValue)
            {
                this.position = tempNumber - this.maxValue;
            }
            else
            {
                this.position = this.position + additionNumber;
            }
        }
    }

    private int devideTheNumberByMaxValue()
    {
        double rawAmount = this.countingNumber / this.maxValue;
        int flooredAmount = (int)Math.Floor(rawAmount);
        int removeAmount = flooredAmount * this.maxValue;
        if (!this.position.Equals(0))
        {
            this.passedByZero += flooredAmount;
        }
        return this.countingNumber - removeAmount;
    }

}

class Log
{
    public List<int> notedNumbers = new List<int>();


    public void addToList(int number)
    {
        this.notedNumbers.Add(number);
    }

    public int checkForZeros()
    {
        int totalAmountZeros = 0;
        foreach (int numb in this.notedNumbers)
        {
            if (numb.Equals(0))
            {
                totalAmountZeros++;
            }
        }
        return totalAmountZeros;
    }
}
