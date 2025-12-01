package nl.mpdev;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.*;

public class Main {

  public static void main(String[] args) {
    File orderingPuzzle = new File("./ordering-puzzle.txt");
    File puzzle = new File("./puzzle.txt");

    var filterRules = createOrderingArr(orderingPuzzle, "\\|");
    var puzzleData = createOrderingArr(puzzle, ",");

    var results = filterArrays(filterRules, puzzleData, false);

    System.out.println("Already Valid Arrays:");
    results.get("validArrays").forEach(System.out::println);

    System.out.println("Invalid and Fixed Arrays:");
    results.get("invalidAndFixedArrays").forEach(System.out::println);

    System.out.println("Original Invalid Arrays:");
    results.get("invalidOriginalArrays").forEach(System.out::println);

    System.out.println("Sum of Middle Numbers (Invalid and Fixed Arrays:):");
    System.out.println(addMiddleNumbersToEachOther(results.get("invalidAndFixedArrays")));
  }

  public static List<List<Integer>> createOrderingArr(File orderingPuzzle, String delimiter) {
    List<List<Integer>> completeList = new ArrayList<>();
    try {
      Scanner scanner = new Scanner(orderingPuzzle);
      while (scanner.hasNextLine()) {
        String input = scanner.nextLine();
        String[] parts = input.split(delimiter);
        List<Integer> numbers = new ArrayList<>();
        for (String part : parts) {
          numbers.add(Integer.valueOf(part));
        }
        completeList.add(numbers);
      }
    } catch (FileNotFoundException e) {
      e.printStackTrace();
    }
    return completeList;
  }

  public static Map<String, List<List<Integer>>> filterArrays(List<List<Integer>> filterPuzzle, List<List<Integer>> puzzle, boolean onlyValidArrays) {
    List<List<Integer>> alreadyValidArrays = new ArrayList<>();
    List<List<Integer>> invalidAndFixedArrays = new ArrayList<>();
    List<List<Integer>> invalidOriginalArrays = new ArrayList<>();

    for (List<Integer> puzzleList : puzzle) {
      boolean isValid = splitOpenArray(filterPuzzle, puzzleList, true);
      if (isValid) {
        alreadyValidArrays.add(new ArrayList<>(puzzleList));
      } else {
        invalidOriginalArrays.add(new ArrayList<>(puzzleList));
        List<Integer> fixedList = fixInvalidArray(filterPuzzle, puzzleList);
        invalidAndFixedArrays.add(fixedList);
      }
    }

    if (onlyValidArrays) {
      return Map.of("validArrays", alreadyValidArrays);
    } else {
      return Map.of(
        "validArrays", alreadyValidArrays,
        "invalidAndFixedArrays", invalidAndFixedArrays,
        "invalidOriginalArrays", invalidOriginalArrays
      );
    }
  }

  private static List<Integer> fixInvalidArray(List<List<Integer>> filterPuzzle, List<Integer> puzzleList) {
    boolean isValid = splitOpenArray(filterPuzzle, puzzleList, true);
    if (isValid) {
      return puzzleList;
    }

    for (int i = 0; i < puzzleList.size() - 1; i++) {
      var nextNumber = puzzleList.get(i + 1);
      for (List<Integer> rules : filterPuzzle) {
        for (int j = 0; j < rules.size(); j += 2) {
          if (Objects.equals(puzzleList.get(i), rules.get(j + 1)) && Objects.equals(nextNumber, rules.get(j))) {
            int temp = puzzleList.get(i);
            puzzleList.set(i, nextNumber);
            puzzleList.set(i + 1, temp);
            return fixInvalidArray(filterPuzzle, puzzleList);
          }
        }
      }
    }
    return puzzleList;
  }

  private static boolean splitOpenArray(List<List<Integer>> filterPuzzle, List<Integer> puzzleList, boolean isValid) {
    for (int i = 0; i < puzzleList.size() - 1; i++) {
      var nextNumber = puzzleList.get(i + 1);
      for (List<Integer> rules : filterPuzzle) {
        isValid = comparePuzzleWithRules(puzzleList, isValid, rules, i, nextNumber);
        if (!isValid) break;
      }
      if (!isValid) break;
    }
    return isValid;
  }

  private static boolean comparePuzzleWithRules(List<Integer> puzzleList, boolean isValid, List<Integer> rules, int i, Integer nextNumber) {
    for (int j = 0; j < rules.size(); j += 2) {
      if (Objects.equals(puzzleList.get(i), rules.get(j)) && Objects.equals(nextNumber, rules.get(j + 1))) {
        continue;
      }
      if (Objects.equals(puzzleList.get(i), rules.get(j + 1)) && Objects.equals(nextNumber, rules.get(j))) {
        isValid = false;
        break;
      }
    }
    return isValid;
  }

  private static int addMiddleNumbersToEachOther(List<List<Integer>> results) {
    int finalResult = 0;
    for (List<Integer> result : results) {
      int middleIndex = result.size() / 2;

      if (result.size() % 2 == 0) {
        int add = result.get(middleIndex - 1) + result.get(middleIndex) / 2;
        finalResult += add;
      } else {
        int add = result.get(middleIndex);
        finalResult += add;
      }
    }
    return finalResult;
  }
}
