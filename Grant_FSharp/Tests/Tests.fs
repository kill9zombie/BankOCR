module Tests

open Fuchu
open Reader
open Swensen.Unquote

//    _  _     _  _  _  _  _
//  | _| _||_||_ |_   ||_||_|
//  ||_  _|  | _||_|  ||_| _| 

let one =
    " " +
    "|" +
    "|"

let two =
    " _ " +
    " _|" +
    "|_ "

[<Tests>]
let OCRTests = 
    testList "single chars" [
        testCase "zero test" (fun _ -> test <@ read one = 1 @>)
    ]