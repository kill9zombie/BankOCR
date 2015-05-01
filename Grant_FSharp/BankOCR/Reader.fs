module Reader

open Numbers

let numberOfLines = 3
let rowsPerCharacter = 3

let split (text:string) =
    let numbeOfChars = text.Length / numberOfLines / rowsPerCharacter
    [text]

let read text =
    if numbers.ContainsKey(text) then
        numbers.[text]
    else
        12