package io.github.leedscodedojo;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class BankOcr {

    public interface numConstsants{

        String ONE =   "     |  |";

        String TWO =   " _ " +
                       " _|" +
                       "|_ ";

        String THREE = " _  _| _|";
    }

    public int parser(String input){

        if (input.equals(numConstsants.ONE)){
            return 1;
        }
        if (input.equals(numConstsants.TWO)){
            return 2;
        }
        if (input.equals(numConstsants.THREE)){
            return 3;
        }
        return -1;
    }

    public String reader(String textToScan,int numOfDigits){
        int topLine = 0;
        int midLine = numOfDigits * 3;
        int botLine = numOfDigits * 6;

        StringBuilder stringBuilder = new StringBuilder();

        for(int i = 0; i < numOfDigits; i++){
            String resultNum = "";

            resultNum += textToScan.substring(topLine, topLine+3);;
            topLine+=3;

            resultNum += textToScan.substring(midLine, midLine+3);;
            midLine+=3;

            resultNum += textToScan.substring(botLine, botLine+3);;
            botLine+=3;

            stringBuilder.append(Integer.toString(parser(resultNum)));
        }

        return stringBuilder.toString();
    }
}
