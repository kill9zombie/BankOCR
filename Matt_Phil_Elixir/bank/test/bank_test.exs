defmodule BankTest do
  use ExUnit.Case

  test "zero" do
    str = """
           _ 
          | |
          |_|
          """
    assert Bank.parse(str) == 0
  end

  test "zero four" do
    str = """
           _    
          | ||_|
          |_|  |
          """

    assert Bank.line(str) == [0,4]
  end
    
  test "zero four seven nine" do
    str = """
           _     _  _ 
          | ||_|  ||_|
          |_|  |  |  |
          """

    assert Bank.line(str) == [0,4,7,9]
  end

  test "a valid bank account number" do
    assert Bank.valid_account_number?([3,4,5,8,8,2,8,6,5]) == true
  end
  test "an invalid bank account number" do
    assert Bank.valid_account_number?([3,4,5,8,8,2,8,6,6]) == false
  end

  test "a valid account number, returned as a string" do
    str = """
           _     _  _  _  _  _  _  _ 
           _||_||_ |_||_| _||_||_ |_ 
           _|  | _||_||_||_ |_||_| _|
          """

    assert Bank.account(str) == "345882865"
  end

  test "an invalid account number, returns ERR" do
    str = """
           _     _  _  _  _  _  _  _ 
           _||_||_ |_||_| _||_||_ |_ 
           _|  | _||_||_||_ |_||_||_|
          """

    assert Bank.account(str) == "345882866 ERR"
  end

  test "illegal characters returns ILL" do
    str = """
           _     _  _  _  _  _  _  _ 
           _||_||_ |_|||| _||_||_ |_ 
           _|  | _||_|||||_ |_||_| _|
          """

    assert Bank.account(str) == "3458?2865 ILL"
  end
    
end
