module Reader

let splitIntoRows (text:string) =
    text.Split([|'\n'|])
    |> Array.toList

let firstCharacter (textLines:string list) =
    [textLines.[0].[0..2];
     textLines.[1].[0..2];
     textLines.[2].[0..2]]

let toNumber characterLines =
    match characterLines with
    | [" _ ";
       "| |";
       "|_|"] -> "0"
    | ["   ";
       "  |";
       "  |"] -> "1"

let read text =
    if text |> splitIntoRows |> firstCharacter |> toNumber = "0" then
        "000000000"
    else
        "111111111"