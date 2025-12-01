using System;

class Program {
  static void Main() {
    string filePath = "./puzzle-input.txt";
    string[] lines = Array.Empty<string>();

    if(File.Exists(filePath)) {
      lines = File.ReadAllLines(filePath);
    }
    else {
      Console.WriteLine("File not found");
    }


    // initialiing the classes
    TurnOperation turnOperation = new TurnOperation(50, 100);
    Log log = new Log();
    CrackManager crackManager = new CrackManager(turnOperation, log, lines);
    crackManager.runTheCrack();

  }
}

class CrackManager {
  private TurnOperation turnOperation;
  private Log log;
  private string[] lines;

  
  public CrackManager(TurnOperation turnOperation, Log log, string[] lines) {
    this.turnOperation = turnOperation;
    this.log = log;
    this.lines = lines;
  }
  
  public void runTheCrack() {
    foreach (string line in this.lines) {
      int test = turnOperation.turnTheWheel(line);
      Console.WriteLine(test);
    }
  }

}

class TurnOperation {

  private string direction = "";
  private int maxValue;
  private bool isPassedByMaxValue = false;
  private bool isPassedByZero = false;
  private int countingNumber = 0;
  private int position;

  public TurnOperation(int initialPosition, int maxValue) {
    this.position = initialPosition;
    this.maxValue = maxValue;

  }

  public int turnTheWheel(string line) {
    this.setDirection(line);
    this.splitTheNumber(line);
    this.calculatePosition();
    return this.position;
  }
  
  private void setDirection(string line) {
    if(line.Contains("L")){
      this.direction = "left";
    } else if (line.Contains("R")){
      this.direction = "right";
    }
  }

  private void splitTheNumber(string line) {
    string numberPart = line.Substring(1);
    this.countingNumber = int.Parse(numberPart);
  }

  private void calculatePosition() {
    if(direction.Equals("left")) {
      Console.WriteLine("LEFT");
      Console.WriteLine(this.isPassedByZero);
      // Console.WriteLine("Counting nunber " + this.countingNumber);
      int tempNumber = this.position - this.countingNumber;
      if(tempNumber < 0 && !this.isPassedByZero) {
        this.isPassedByZero = true;
        this.isPassedByMaxValue = false;
        this.position = this.maxValue - Math.Abs(tempNumber);
      } else {
        this.position = this.position - this.countingNumber;
      }
      
    } else {
      Console.WriteLine("RIGHT");
      // Console.WriteLine("Counting nunber " + this.countingNumber);
      int tempNumber = this.position + this.countingNumber;
      if(tempNumber > this.maxValue && !this.isPassedByMaxValue) {
        this.isPassedByMaxValue = true;
        this.isPassedByZero = false;
        this.position = tempNumber - this.maxValue;
      } else {
        this.position = this.position + this.countingNumber;
      }
    }
  }

}

class Log {
  public List<int> notedNumbers = new List<int>();


  public void addToList(int number) {
    this.notedNumbers.Add(number);
  }
}
