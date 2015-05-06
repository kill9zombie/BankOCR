module Reader

let splitIntoRows (text:string) =
    text.Split([|'\n'|])
    |> Array.toList

let firstChars n (lines:string list) =
    [lines.[0].[0..n-1];
     lines.[1].[0..n-1];
     lines.[2].[0..n-1]]

let charsFrom n (lines:string list) =
    [lines.[0].[n..];
     lines.[1].[n..];
     lines.[2].[n..]]

let rec characters = function
    | ""::_ -> [[];[];[]]
    | lines -> (lines |> firstChars 3)::(lines |> charsFrom 3 |> characters)

let toNumber = function
    | [" _ ";
       "| |";
       "|_|"] -> "0"
    | ["   ";
       "  |";
       "  |"] -> "1"
    | _ -> ""

let read text =
    text 
    |> splitIntoRows 
    |> characters 
    |> List.map toNumber
    |> String.concat ""
