module Tests

open Fuchu
open Reader
open Swensen.Unquote

let zeroes = 
    " _  _  _  _  _  _  _  _  _ \n" +
    "| || || || || || || || || |\n" +
    "|_||_||_||_||_||_||_||_||_|\n"

let ones = 
    "                           \n" +
    "  |  |  |  |  |  |  |  |  |\n" +
    "  |  |  |  |  |  |  |  |  |\n"

let zeroesAndOnes = 
    "    _     _     _     _    \n" +
    "  || |  || |  || |  || |  |\n" +
    "  ||_|  ||_|  ||_|  ||_|  |\n"

[<Tests>]
let singleCharTests = 
    testList "single chars" [
        testCase "zeroes test" (fun _ -> test <@ read zeroes = "000000000" @>)
        testCase "ones test" (fun _ -> test <@ read ones = "111111111" @>)
    ]

[<Tests>]
let mixedCharTests = 
    testList "mixed chars" [
        testCase "ones and zeroes test" (fun _ -> test <@ read zeroesAndOnes = "0101010101" @>)
    ]    