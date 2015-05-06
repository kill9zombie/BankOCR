module Tests

open Fuchu
open Reader
open Swensen.Unquote

let zeroesAndOnes = 
    " _  _  _  _     _          \n" +
    "| || || || |  || |  |  |  |\n" +
    "|_||_||_||_|  ||_|  |  |  |\n"

let manyNumbers = 
    "    _  _     _  _  _  _  _ \n" +
    "  | _| _||_||_ |_   ||_||_|\n" +
    "  ||_  _|  | _||_|  ||_| _|\n"

let invalidChecksum = 
    " _     _  _  _  _  _  _  _ \n" +
    " _||_||_ |_||_| _||_||_ |_ \n" +
    " _|  | _||_||_||_ |_||_||_|\n"

let invalidChars = 
    "    _  ^     _  _  _  _  _ \n" +
    "    _| _||_||_ |_   ||_||_|\n" +
    "  ||_  _|  | _\|_|  /|_|  |\n"


[<Tests>]
let multipleCharTests = 
    testList "mixed chars" [
        testCase "ones and zeroes test" (fun _ -> test <@ read zeroesAndOnes = "000010111" @>) // chosen to pass checksum
        testCase "all numbers" (fun _ -> test <@ read manyNumbers = "123456789" @>)
    ]  
    
[<Tests>]
let checksumTests = 
    testList "checksums" [
        testCase "checksum positive case" (fun _ -> test <@ checksumValid "345882865" @>)
        testCase "checksum negative case" (fun _ -> test <@ not (checksumValid "345882866") @>)
    ]    

[<Tests>]
let infoTests = 
    testList "information output" [
        testCase "checksum fail gives ERR" (fun _ -> test <@ read invalidChecksum = "345882866 ERR" @>)
        testCase "invalid character gives ? and ILL" (fun _ -> test <@ read invalidChars = "?2?4?6?8? ILL" @>)
    ]    