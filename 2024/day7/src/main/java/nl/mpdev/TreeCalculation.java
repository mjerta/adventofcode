package nl.mpdev;

import java.io.*;
import java.util.*;

public class TreeCalculation {

  public static void main(String[] args) {
    try (Scanner scanner = new Scanner(new File("puzzle.txt"))) {
      long result = 0;

      while (scanner.hasNextLine()) {
        String line = scanner.nextLine();
        List<List<Long>> tree = new ArrayList<>();
        String[] parts = line.split(":");
        long answer = Long.parseLong(parts[0].trim());
        String[] questions = parts[1].trim().split("\\s+");

        for (int idx = 0; idx < questions.length; idx++) {
          long q = Long.parseLong(questions[idx]);

          if (idx == 0) {
            tree.add(new ArrayList<>(List.of(q)));
          } else {
            List<Long> currentLevel = new ArrayList<>();
            for (long prev : tree.get(idx - 1)) {
              currentLevel.add(prev * q);
              currentLevel.add(prev + q);
              currentLevel.add(Long.parseLong(prev + "" + q)); // concatenation
            }
            tree.add(currentLevel);
          }
        }

        if (tree.get(questions.length - 1).contains(answer)) {
          System.out.println("Answer " + answer + " gevonden");
          result += answer;
        }
      }

      System.out.println("Part 1 result = " + result);

    } catch (IOException e) {
      e.printStackTrace();
    }
  }
}
