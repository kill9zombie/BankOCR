module Tests

open Xunit
open FsUnit.Xunit

[<Fact>]
let ``Can read a zero`` () =
    let line1 = " _ "
    let line2 = "| |"
    let line3 = "|_|"

    Bank.Decode [line1;line2;line3] |> should equal "0"

[<Fact>]
let ``Can read a one`` () =
    let line1 = "   "
    let line2 = "  |"
    let line3 = "  |"

    Bank.Decode [line1;line2;line3] |> should equal "1"

[<Fact>]
let ``Can read a zero and a one`` () =
    let line1 = " _    "
    let line2 = "| |  |"
    let line3 = "|_|  |"
    
    Bank.Decode [line1;line2;line3] |> should equal "01"

[<Fact>]
let ``Can read all the numbers`` () =
    let line1 = " _     _  _     _  _  _  _  _ "
    let line2 = "| |  | _| _||_||_ |_   ||_||_|"
    let line3 = "|_|  ||_  _|  | _||_|  ||_| _|"
    
    Bank.Decode [line1;line2;line3] |> should equal "0123456789"

[<Fact>]
let ``Can Get Checksum Of Valid Account Number`` () =
    Bank.ValidAccount "345882865" |> should equal true


[<Fact>]
let ``Can Test Checksum Of Invalid Account Number`` () =
    Bank.ValidAccount "345882565" |> should equal false


[<Fact>]
let ``Can Test valid Account Number Has nothing on the end`` () =
    let line1 = "    _  _     _  _  _  _  _ "
    let line2 = "  | _| _||_||_ |_   ||_||_|"
    let line3 = "  ||_  _|  | _||_|  ||_| _|"

    Bank.DecodeAndValidateAccount [line1;line2;line3] |> should equal "123456789"

[<Fact>]
let ``Can Test Invalid Account Number Has ERR on the end`` () =
    let line1 = "    _  _     _  _  _  _  _ "
    let line2 = "  | _| _||_||_ |_   ||_||_|"
    let line3 = "  ||_  _|  | _||_|  ||_||_|"

    Bank.DecodeAndValidateAccount [line1;line2;line3] |> should equal "123456788 ERR"


[<Fact>]
let ``Can Test Invalid Character Has ILL on the end`` () =
    let line1 = "    _  _     _  _  _  _  _ "
    let line2 = "  | _| _||_||_ |_    |_||_|"
    let line3 = "  ||_  _|  | _||_|  ||_||_|"

    Bank.DecodeAndValidateAccount [line1;line2;line3] |> should equal "123456?88 ILL"