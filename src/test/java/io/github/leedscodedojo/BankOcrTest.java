package io.github.leedscodedojo;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class BankOcrTest {
    @Test
    public void can_parse_12() {
        String textToScan =
                "    _ " +
                "  | _|" +
                "  ||_ ";

        BankOcr bankOcr = new BankOcr();
        String actualNumber = bankOcr.reader(textToScan,2);

        assertEquals("12", actualNumber);
     }

    @Test
    public void can_parse_1234() {
        // TODO
//        String textToScan =
//                "    _  _    " +
//                "  | _| _||_|" +
//                "  ||_  _|  |";
//
//        BankOcr bankOcr = new BankOcr();
//        String actualNumber = bankOcr.reader(textToScan,2);
//
//        assertEquals("1234", actualNumber);
    }
}