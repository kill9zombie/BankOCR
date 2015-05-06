package io.github.leedscodedojo;

import javax.print.DocFlavor;
import java.util.ArrayList;
import java.util.List;

public class OcrReader {
    List<Number> numbers = new ArrayList<Number>();

    public OcrReader() {
        numbers.add(new Number(1));
        numbers.add(new Number(2));
    }

    public int read(String inputString) {
        String[] lines = inputString.split("\n");

        String singleCharacter = getOcrCharater(lines, 0);
        int number1 = parseSingleCharacter(singleCharacter);

        if (lines[0].length() <= 3){
            return number1;
        }

        String secondCharacter = getOcrCharater(lines, 1);
        int number2 = parseSingleCharacter(secondCharacter);

        return number1 * 10 + number2;
    }

    private int parseSingleCharacter(String character) {
        for (Number number : numbers) {
            if (number.getOcrText().equals(character)) {
                return number.getNumber();
            }
        }

        return -1;
    }

    private String getOcrCharater(String[] lines, int characterIndex) {
        String line1 = lines[0].substring(characterIndex * 3,characterIndex * 3 + 3);
        String line2 = lines[1].substring(characterIndex * 3, characterIndex * 3 + 3);
        String line3 = lines[2].substring(characterIndex * 3, characterIndex * 3 + 3);

        return String.format("%s\n%s\n%s\n", line1,line2,line3);
    }
}
