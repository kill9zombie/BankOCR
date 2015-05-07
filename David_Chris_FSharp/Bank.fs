module Bank

open Constants

let (|Equals|_|) expected actual =
    if actual = expected then Some() else None

let CharToInt c =
   match System.Int32.TryParse(c.ToString()) with
   | (true,int) -> int
   | _ -> 0

let decodeSingleString = function
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

let Decode lines =
    let rec splitInto3 = function
        | (l:string) when l.Length >= 3 -> l.[0..2]::splitInto3 l.[3..]
        | _ -> []

    let threes = lines |> List.map splitInto3 |> List.toArray
    let zipped = List.zip3 threes.[0] threes.[1] threes.[2]
    zipped |> List.map decodeSingleString
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

// Works out the other possible state for this segment within a line of a digit.
// currentValue is the current state of the segment,  for each blank,  _ or |
// position is the zero-based location within the line.  (0=left, 1=middle and 2=right)
let ToggleSegment currentValue position = 
    if currentValue = ' ' then
        match position with
            | 1 -> "_"
            | _ -> "|"
    else
        " "

// Given a line of a digit, and a segment position this will return the line but with the state
// of the specified segment flipped.  For example flipping the middle segment of |_| gives | |
let FlipSegmentInTuple (currentValue : string) segmentNumber =
    match segmentNumber with
            | 0 -> (ToggleSegment currentValue.[0] 0) + currentValue.[1..2]
            | 1 -> currentValue.[0..0] + (ToggleSegment currentValue.[1] 1) + currentValue.[2..2]
            | _ -> currentValue.[0..1] + (ToggleSegment currentValue.[2] 2)

// Given a line of a digit this generates the other three possible lines.
let GenerateTuples currentValue =
    [FlipSegmentInTuple currentValue 0;FlipSegmentInTuple currentValue 1;FlipSegmentInTuple currentValue 2]

// Given a digit,  this generates the nine possible digits which can be obtained by just flipping a single segment.
let GenerateAllNinePossibilities (lines : string*string*string) =
    let (top,middle,bottom) = lines
    let tops = GenerateTuples top |> List.map (fun x -> (x,middle,bottom))
    let middles = GenerateTuples middle |> List.map (fun x -> (top,x,bottom))
    let bottoms = GenerateTuples bottom |> List.map (fun x -> (top,middle,x))
    tops @ middles @ bottoms

// Given a digit,  this generates any possible (valid) digits which can be obtained by just flipping a single segment.
let GetValidPossibilities lines =
    GenerateAllNinePossibilities lines 
                |> List.map decodeSingleString
                |> List.filter (fun n -> n <> "?")
    

// Given an invalid number,  we try and correct it.  We are assuming that only a single digit will ever be invalid.
let CorrectNumber (lines) =
    
    let rec splitInto3 = function
        | (l:string) when l.Length >= 3 -> l.[0..2]::splitInto3 l.[3..]
        | _ -> []

    let threes = lines |> List.map splitInto3 |> List.toArray
    let zipped = List.zip3 threes.[0] threes.[1] threes.[2]

    let number = Decode lines
    let digitNumber = number |> Seq.findIndex (fun c -> c = '?')
    let digitToCorrect = zipped.[digitNumber]

    let possibleCorrections = GetValidPossibilities digitToCorrect
                                    |> List.map (fun correction -> number.[..digitNumber - 1] + correction + number.[digitNumber + 1..])
                                    |> List.filter ValidAccount

    match possibleCorrections |> List.length with
        | 0 -> sprintf "%s ILL" number
        | 1 -> possibleCorrections.Head
        | _  -> sprintf "%s AMB" number