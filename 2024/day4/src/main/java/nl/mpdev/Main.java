package nl.mpdev;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Scanner;

public class Main {
  public static int counter = 0;

  public static void main(String[] args) {

    File puzzle = new File("./puzzle.txt");
    var arr = createArr(puzzle);
//    searchToTheRight(arr);
//    searchToTheLeft(arr);
//    searchToBottom(arr);
//    searchToTop(arr);
//    seachDiagonalBottomLeft(arr);
//    seachDiagonalBottomRight(arr);
//    seachDiagonalTopLeft(arr);
//    searchDiagonalTopRight(arr);
    searchTheX(arr);
    System.out.println(counter);
  }

  private static void searchToTheRight(ArrayList<String> arr) {
    for (int i = 0; i < arr.size(); i++) {
      var currentLine = arr.get(i);
      for (int j = 0; j < currentLine.length() - 3; j++) {
        char firstLetter = currentLine.charAt(j);
        char secondLetter = currentLine.charAt(j + 1);
        char tirthLetter = currentLine.charAt(j + 2);
        char fourthLetter = currentLine.charAt(j + 3);
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter);
      }
    }
  }

  private static void searchToTheLeft(ArrayList<String> arr) {
    for (int i = 0; i < arr.size(); i++) {
      var currentLine = arr.get(i);
      for (int j = 3; j < currentLine.length(); j++) {
        char firstLetter = currentLine.charAt(j);
        char secondLetter = currentLine.charAt(j - 1);
        char tirthLetter = currentLine.charAt(j - 2);
        char fourthLetter = currentLine.charAt(j - 3);
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter);
      }
    }
  }

  private static void searchToBottom(ArrayList<String> arr) {
    for (int i = 0; i < arr.size() - 3; i++) {
      var currentLine = arr.get(i);
      for (int j = 0; j < currentLine.length(); j++) {
        char firstLetter = currentLine.charAt(j); // current line
        char secondLetter = arr.get(i + 1).charAt(j); // next line +  same index
        char tirthLetter = arr.get(i + 2).charAt(j); // next line +  same index
        char fourthLetter = arr.get(i + 3).charAt(j); // next line +  same index
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter);
      }
    }
  }

  private static void searchToTop(ArrayList<String> arr) {
    for (int i = 3; i < arr.size(); i++) {
      var currentLine = arr.get(i);
      for (int j = 0; j < currentLine.length(); j++) {
        char firstLetter = currentLine.charAt(j); // current line
        char secondLetter = arr.get(i - 1).charAt(j); // next line +  same index
        char tirthLetter = arr.get(i - 2).charAt(j); // next line +  same index
        char fourthLetter = arr.get(i - 3).charAt(j); // next line +  same index
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter);
      }
    }
  }

  private static void seachDiagonalBottomLeft(ArrayList<String> arr) {
    for (int i = 0; i < arr.size() - 3; i++) {
      var currentLine = arr.get(i);
      for (int j = 3; j < currentLine.length(); j++) {
        char firstLetter = currentLine.charAt(j); // current line
        char secondLetter = arr.get(i + 1).charAt(j - 1);
        char tirthLetter = arr.get(i + 2).charAt(j - 2);
        char fourthLetter = arr.get(i + 3).charAt(j - 3);
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter);
      }
    }
  }

  private static void seachDiagonalBottomRight(ArrayList<String> arr) {
    for (int i = 0; i < arr.size() - 3; i++) {
      var currentLine = arr.get(i);
      for (int j = 0; j < currentLine.length() - 3; j++) {
        char firstLetter = currentLine.charAt(j); // current line
        char secondLetter = arr.get(i + 1).charAt(j + 1);
        char tirthLetter = arr.get(i + 2).charAt(j + 2);
        char fourthLetter = arr.get(i + 3).charAt(j + 3);
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter);
      }
    }
  }

  private static void seachDiagonalTopLeft(ArrayList<String> arr) {
    for (int i = 3; i < arr.size(); i++) {
      var currentLine = arr.get(i);
      for (int j = 3; j < currentLine.length(); j++) {
        char firstLetter = currentLine.charAt(j); // current line
        char secondLetter = arr.get(i - 1).charAt(j - 1);
        char tirthLetter = arr.get(i - 2).charAt(j - 2);
        char fourthLetter = arr.get(i - 3).charAt(j - 3);
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter);
      }
    }
  }

  private static void searchDiagonalTopRight(ArrayList<String> arr) {
    for (int i = 3; i < arr.size(); i++) {
      var currentLine = arr.get(i);
      for (int j = 0; j < currentLine.length() - 3; j++) {
        char firstLetter = currentLine.charAt(j); // current line
        char secondLetter = arr.get(i - 1).charAt(j + 1);
        char tirthLetter = arr.get(i - 2).charAt(j + 2);
        char fourthLetter = arr.get(i - 3).charAt(j + 3);
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter);
      }
    }
  }

  private static void searchTheX(ArrayList<String> arr) {
    for (int i = 0; i < arr.size() - 2; i++) {
      var currentLine = arr.get(i);
      for (int j = 0; j < currentLine.length() - 2; j++) {
        char firstLetter = currentLine.charAt(j); // current line
        char secondLetter = arr.get(i + 1).charAt(j + 1);
        char tirthLetter = arr.get(i + 2).charAt(j + 2);
        char fourthLetter = arr.get(i + 2).charAt(j);
        char fiftLetter = arr.get(i + 1).charAt(j + 1);
        char sixLetter = arr.get(i).charAt(j + 2);
        validateLetter(firstLetter, secondLetter, tirthLetter, fourthLetter, fiftLetter, sixLetter, i, j);
      }
    }
  }

  private static void validateLetter(char firstLetter, char secondLetter, char tirthLetter, char fourthLetter, char fiftLetter,
                                     char sixtLetter, int currentLine, int position) {
    int insidecount = 0;
    if ((firstLetter == 'S' && secondLetter == 'A' &&  tirthLetter == 'M') || firstLetter == 'M'  && secondLetter == 'A' &&  tirthLetter == 'S') {
      insidecount++;

    }
    if ((fourthLetter == 'S' && fiftLetter == 'A' &&  sixtLetter == 'M') || fourthLetter == 'M'  && fiftLetter == 'A' &&  sixtLetter == 'S') {
      insidecount++;
    }
    if (insidecount == 2) {
//      System.out.println(currentLine + 1);
//      System.out.println("Top-left-to-bottom-right diagonal match at " + firstLetter + secondLetter + tirthLetter);
//      System.out.println("Bottom-left-to-top-right diagonal match: " + fourthLetter + fiftLetter + sixtLetter);
      counter++;
//      System.out.println("Full X match found! Incremented counter.");
    }
  }

  private static void validateLetter(char firstLetter, char secondLetter, char tirthLetter, char fourthLetter) {
    if (firstLetter == 'X' && secondLetter == 'M' && tirthLetter == 'A' && fourthLetter == 'S') {
      counter++;
    }
  }

  private static void validateLetter(char firstLetter, char secondLetter, char tirthLetter, char fourthLetter, int line) {
    if (firstLetter == 'X' && secondLetter == 'M' && tirthLetter == 'A' && fourthLetter == 'S') {
      counter++;
      System.out.println(line);
    }
  }

  private static ArrayList<String> createArr(File puzzle) {
    ArrayList<String> puzzleLine = new ArrayList<>();
    try {
      Scanner scanner = new Scanner(puzzle);

      while (scanner.hasNextLine()) {
        puzzleLine.add(scanner.nextLine());
      }
    } catch (FileNotFoundException e) {
      e.printStackTrace();
    }
    return puzzleLine;
  }

}
