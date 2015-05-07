package io.github.leedscodedojo;

import org.junit.Test;

import static org.hamcrest.CoreMatchers.containsString;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertThat;

public class BankOcrTest {
    @Test
    public void can_parse_12() {
        String textToScan =
                "    _ \n" +
                "  | _|\n" +
                "  ||_ \n";

        BankOcr bankOcr = new BankOcr();
        String actualNumber = bankOcr.read(textToScan);

        assertEquals("12", actualNumber);
    }

    @Test
    public void can_parse_1234() {
        String textToScan =
                "    _  _    \n" +
                "  | _| _||_|\n" +
                "  ||_  _|  |\n";

        BankOcr bankOcr = new BankOcr();
        String actualNumber = bankOcr.read(textToScan);

        assertEquals("1234", actualNumber);
    }

    @Test
    public void can_parse_0123456789() {
        String textToScan =
                " _     _  _     _  _  _  _  _ \n" +
                "| |  | _| _||_||_ |_   ||_||_|\n" +
                "|_|  ||_  _|  | _||_|  ||_| _|\n";

        BankOcr bankOcr = new BankOcr();
        String actualNumber = bankOcr.read(textToScan);

        assertEquals("0123456789", actualNumber);
    }
    @Test
    public void single_line_is_invalid() {
       String textToScan = "XXX";

        BankOcr bankOcr = new BankOcr();
        try {
            bankOcr.read(textToScan);
        }catch (Throwable e){
            assertThat(e.getMessage(), containsString("XXX"));
        }
    }

    @Test
    public void different_length_lines_throws_exception() {
        String textToScan = "XXXXXX\n" +
                "XXX\n" +
                "XXX\n";

        BankOcr bankOcr = new BankOcr();
        try {
            bankOcr.read(textToScan);
        }catch (Throwable e){
            assertThat(e.getMessage(), containsString("XXX"));
        }
    }

    @Test
    public void lines_must_be_multiple_of_three_long() {
        String textToScan = "XXXX\n" +
                "XXXX\n" +
                "XXXX\n";

        BankOcr bankOcr = new BankOcr();
        try {
            bankOcr.read(textToScan);
        }catch (Throwable e){
            assertThat(e.getMessage(), containsString("XXX"));
        }
    }

    @Test
    public void invalid_digit() {
        String textToScan =
                "XXX\n" +
                "XXX\n" +
                "XXX\n";

        BankOcr bankOcr = new BankOcr();
        String actualNumber = bankOcr.read(textToScan);

        assertEquals("?", actualNumber);
    }
}