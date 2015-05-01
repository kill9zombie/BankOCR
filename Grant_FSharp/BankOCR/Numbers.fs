module Numbers

//    _  _     _  _  _  _  _
//  | _| _||_||_ |_   ||_||_|
//  ||_  _|  | _||_|  ||_| _| 

let one =
    "   " +
    "  |" +
    "  |"

let two =
    " _ " +
    " _|" +
    "|_ "

let numbers =
    Map.empty
        .Add(one, 1)
        .Add(two, 2)