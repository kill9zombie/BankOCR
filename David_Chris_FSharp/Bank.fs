module Bank

open Constants

let (|Equals|_|) expected actual =
    if actual = expected then Some() else None

let CharToInt c =
   match System.Int32.TryParse(c.ToString()) with
   | (true,int) -> int
   | _ -> 0

let Decode lines =
    let decodeFullString = function
        | Equals zero -> "0"
        | Equals one -> "1"
        | Equals two -> "2"
        | Equals three -> "3"
        | Equals four -> "4"
        | Equals five -> "5"
        | Equals six -> "6"
        | Equals seven -> "7"
        | Equals eight -> "8"
        | Equals nine -> "9"
        | _ -> "?"

    let rec splitInto3 = function
        | (l:string) when l.Length >= 3 -> l.[0..2]::splitInto3 l.[3..]
        | _ -> []

    let threes = lines |> List.map splitInto3 |> List.toArray
    let zipped = List.zip3 threes.[0] threes.[1] threes.[2]
    zipped |> List.map decodeFullString
           |> String.concat ""
           
let ValidAccount (number : string) =
    let total = number |> Seq.toArray
                       |> Array.rev
                       |> Array.mapi (fun i x -> (i+1) * (CharToInt x))
                       |> Array.sum
    total % 11 = 0

let DecodeAndValidateAccount lines =
    let number = Decode lines
    match number with
        | n when n |> Seq.exists (fun c -> c = '?') -> sprintf "%s ILL" n
        | n when not (ValidAccount n) -> sprintf "%s ERR" n
        | n -> n

//
//let rec mutate tuple index = 
//    match index with
//        | 1 _> 