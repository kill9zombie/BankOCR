package io.github.leedscodedojo;

import org.junit.Test;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;

public class OcrReaderTests {
    @Test
    public void read_one() {
        String inputString =
                "   \n" +
                "  |\n" +
                "  |\n";
        OcrReader reader = new OcrReader();

        int number = reader.read(inputString);

        assertThat(number, is(1));
    }

    @Test
    public void read_two() {
        String inputString =
                " _ \n" +
                " _|\n" +
                "|_ \n";
        OcrReader reader = new OcrReader();

        int number = reader.read(inputString);

        assertThat(number, is(2));
    }
}

