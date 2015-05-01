module Tests

open Fuchu
open Reader
open Swensen.Unquote
open Numbers

let onetwo = 
    "     _ " +
    "  |  _|" +
    "  | |_"

[<Tests>]
let singleCharTests = 
    testList "single chars" [
        testCase "one test" (fun _ -> test <@ read one = 1 @>)
        testCase "two test" (fun _ -> test <@ read two = 2 @>)
    ]
    
[<Tests>]
let multipleCharTests = 
    testList "multiple chars" [
        //testCase "one-two test" (fun _ -> test <@ read onetwo = 12 @>)
        testCase "split works" (fun _ -> test <@ split one = [one] @>)
        testCase "split works more" (fun _ -> test <@ split onetwo = [one; two] @>)
    ]