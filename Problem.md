# Bank OCR Problem Description

This is a slightly simplified version of the [Bank OCR Kata from CodingDojo.org](http://codingdojo.org/cgi-bin/index.pl?KataBankOCR).  The main difference is that it specifies working out one number at a time, rather than a file of them.

### User Story 1
You work for a bank, which has recently purchased an ingenious machine to assist in reading letters and faxes sent in by branch offices. The machine scans the paper documents, and produces a file with a number of entries which each look like this:

      _  _     _  _  _  _  _ 
    | _| _||_||_ |_   ||_||_| 
    ||_  _|  | _||_|  ||_| _|  

Each entry is 3 lines long, and each line has 27 characters. The characters represent an account number written using pipes and underscores. Each account number should have 9 digits, all of which should be in the range 0-9.

**Note**: You probbaly want a newline at the end of each line of text.

Your first task is to write a program that can convert any such valid pipe-based account number to a normal string containing the number it represents, e.g. "123456789".

### User Story 2

Having done that, you quickly realize that the ingenious machine is not in fact infallible. Sometimes it goes wrong in its scanning. The next step therefore is to validate that the numbers you read are in fact valid account numbers. A valid account number has a valid checksum. This can be calculated as follows:

    account number:  3  4  5  8  8  2  8  6  5  
    position names:  d9 d8 d7 d6 d5 d4 d3 d2 d1

checksum calculation:
( d1 + 2 * d2 + 3 * d3 + .. + 9 * d9 ) mod 11 = 0

So now you should also write some code that calculates the checksum for a given number, and identifies if it is a valid account number.

### User Story 3

If there is something wrong with an input number, it would be good to output some information.  Update your code so that:

For a valid input, it just outputs the number (still as a string):  
    457508000

If the checksum fails, it outputs the number followed by "ERR":  
    664371495 ERR
    
If there are any illegal characters, when output the number with the invalid characterd replaced with "?", followed by "ILL":  
    86110??36 ILL

### User Story 4

It turns out that often when a number comes back as ERR or ILL it is because the scanner has failed to pick up on one pipe or underscore for one of the figures. For example

        _  _  _  _  _  _     _ 
    |_||_|| || ||_   |  |  ||_ 
      | _||_||_||_|  |  |  | _| 

The 9 could be an 8 if the scanner had missed one |. Or the 0 could be an 8. Or the 1 could be a 7. The 5 could be a 9 or 6. So your next task is to look at numbers that have come back as ERR or ILL, and try to guess what they should be, by adding or removing just one pipe or underscore. If there is only one possible number with a valid checksum, then use that. If there are several options, the status should be AMB. If you still can't work out what it should be, the status should be reported ILL.

#### Clues

I recommend finding a way to write out 3x3 cells on 3 lines in your code, so they form an identifiable digits. Even if your code actually doesn't represent them like that internally. I'd much rather read

    "   " +
    "|_|" +
    "  |"
than
    "   |_|  |"
anyday.

When this kata was originally presented, the solution made extensive use of recursion. Many people are more comfortable with iteration than recursion. Try this kata both ways.

#### Some gotchas to avoid:

 - be very careful to read the definition of checksum correctly. It is not a simple dot product, the digits are reversed from what you expect.
 - The spec does not list all the possible alternatives for valid digits when one pipe or underscore has been removed or added
 - don't forget to try to work out what a ? should have been by adding or removing one pipe or underscore.

