package nl.mpdev;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Main {
  public static void main(String[] args) {

    int totalAmmount = 0;
    int positionOfPreviousMul = 0;
    int previousPosition = 0;
    boolean ableToCalculate = true;

    File puzzle = new File("./puzzle.txt");

    String start = "mul\\(";
    String end = "\\)";
    String regex = start + "\\d+,\\d+" + end;

    String regexDo = "\\bdo\\b(?!')";
    String regexDont = "\\bdon't\\b(?!')";
    Pattern patternDo = Pattern.compile(regexDo);
    Pattern patternDont = Pattern.compile(regexDont);

    StringBuilder text = new StringBuilder();
    try {
      Scanner scanner = new Scanner(puzzle);
      while (scanner.hasNextLine()) {
        text.append(scanner.nextLine());
      }
    } catch (FileNotFoundException e) {
      System.out.println("File is not found");
      e.printStackTrace();
    }
    System.out.println(text.length());

    var initialSearch = findMatches(text.toString(), regex);

    while (initialSearch.find()) {
      // look up previous search

      String queryString = initialSearch.group();
      positionOfPreviousMul = text.indexOf(queryString);
      if (previousPosition < positionOfPreviousMul) {
        var test = text.substring(previousPosition, positionOfPreviousMul);
        System.out.println(test);
        Matcher matcher = patternDo.matcher(test);
        if (matcher.find()) {
          ableToCalculate = true;
          System.out.println("found");
        }
        Matcher matcher1 = patternDont.matcher(test);
        if (matcher1.find()) {
          ableToCalculate = false;
          System.out.println("dont found");
        }

        previousPosition = positionOfPreviousMul;
      }
      String trimmedInput = queryString.replace("mul(", "").replace(")", "");

      // Split the string by the comma
      String[] numbers = trimmedInput.split(",");

      // Convert to integers
      int firstNumber = Integer.parseInt(numbers[0]);
      int secondNumber = Integer.parseInt(numbers[1]);

      // Output the two numbers
      System.out.println("First number: " + firstNumber);
      System.out.println("Second number: " + secondNumber);

      if (ableToCalculate) {
        totalAmmount += firstNumber * secondNumber;
      }
    }
    System.out.println(totalAmmount);
  }

  private static Matcher findMatches(String text, String regex) {
    Pattern pattern = Pattern.compile(regex);
    return pattern.matcher(text);
  }
}
