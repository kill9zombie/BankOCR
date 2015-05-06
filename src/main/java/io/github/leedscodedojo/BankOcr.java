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

    public String reader(String textToScan){
        int numOfDigits = 2;
        int topLine = 0;
        int midLine = numOfDigits * 3;
        int botLine = numOfDigits * 6;

        Pattern p = Pattern.compile(".{3}");
        Matcher m = p.matcher(textToScan);
        String resultNum;
        String result = "";

        for(int i = 0; i < numOfDigits; i++){
            resultNum = "";
            if(m.find(topLine)){
                resultNum += m.group();
                topLine += 3;
            }
            if(m.find(midLine)){
                resultNum += m.group();
                midLine += 3;
            }
            if(m.find(botLine)){
                resultNum += m.group();
                botLine += 3;
            }
            result += Integer.toString(parser(resultNum));
        }

        return result;
    }
}
