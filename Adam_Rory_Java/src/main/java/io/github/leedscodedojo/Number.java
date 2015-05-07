package io.github.leedscodedojo;

public class Number {
    private final String ocrText;
    private final int number;

    public Number(int number) {
        this.number = number;
        this.ocrText = getNumberOcrText(number);
    }

    private String getNumberOcrText(int number){
        switch (number) {
            case 1:
                return "   \n" +
                       "  |\n" +
                       "  |\n";

            case 2:
                return " _ \n" +
                       " _|\n" +
                       "|_ \n";
        }

        throw new ArithmeticException();
    }

    public String getOcrText(){
        return ocrText;

    }

    public int getNumber(){
        return number;
    }
}
