module Reader

open Numbers

let numberOfLines = 3
let columnsPerCharacter = 3

let subString startIndex length (text:string) =
    text.Substring(startIndex, length)

let splitIntoRows lineLength text =
    [|0..numberOfLines-1|]
    |> Array.map (fun lineNumber ->
        text |> subString (lineLength * lineNumber) lineLength)

let splitIntoCharacters numberOfChars rows =
    [0..numberOfChars-1]
    |> List.map (fun charIndex ->
        rows |> Array.fold( fun output line ->
            output + (line |> subString (columnsPerCharacter * charIndex) columnsPerCharacter)) "")

let split (text:string) =
    let numberOfChars = text.Length / numberOfLines / columnsPerCharacter
    let lineLength = numberOfChars * columnsPerCharacter

    text 
    |> splitIntoRows lineLength
    |> splitIntoCharacters numberOfChars

let read text =
    if numbers.ContainsKey(text) then
        numbers.[text]
    else
        12