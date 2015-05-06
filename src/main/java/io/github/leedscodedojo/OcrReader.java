package io.github.leedscodedojo;

import java.util.ArrayList;
import java.util.List;

public class OcrReader {
    List<Number> numbers = new ArrayList<Number>();

    public OcrReader() {
        numbers.add(new Number(1));
        numbers.add(new Number(2));
    }

    public int read(String inputString) {
        for (Number number : numbers) {
            if (number.getOcrText().equals(inputString)) {
                return number.getNumber();
            }
        }

        // TODO - Unknown?
        return -1;
    }
}
