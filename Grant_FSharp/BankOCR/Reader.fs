module Reader

open System

let toNumber = function
    | [" _ ";
       "| |";
       "|_|"] -> "0"
    | ["   ";
       "  |";
       "  |"] -> "1"
    | [" _ ";
       " _|";
       "|_ "] -> "2"
    | [" _ ";
       " _|";
       " _|"] -> "3"
    | ["   ";
       "|_|";
       "  |"] -> "4"
    | [" _ ";
       "|_ ";
       " _|"] -> "5"
    | [" _ ";
       "|_ ";
       "|_|"] -> "6"
    | [" _ ";
       "  |";
       "  |"] -> "7"
    | [" _ ";
       "|_|";
       "|_|"] -> "8"
    | [" _ ";
       "|_|";
       " _|"] -> "9"
    | _ -> ""

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

let read text =
    text 
    |> splitIntoRows 
    |> characters 
    |> List.map toNumber
    |> String.concat ""

let (|Int|) char =
   match Int32.TryParse(char.ToString()) with
   | (true,int) -> int 
   | _ -> 0

let checksumValid (number:string) =
    let rec checksum index remaining =
        match remaining with
        | [] -> 0
        | Int current::rest -> current * index + checksum (index - 1) rest

    (number |> Seq.toList |> checksum 9) % 11 = 0