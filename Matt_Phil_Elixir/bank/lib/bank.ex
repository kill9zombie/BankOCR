defmodule Bank do
  require Logger

  @doc ~S"""
  Parse a line of characters

  Example:

      iex> str = """
      iex>  _    
      iex> | ||_|
      iex> |_|  |
      iex> """
      " _  \n| ||_|\n|_|  |\n"
      iex> Bank.line(str)
      [0, 4]

  """
  def line(str) do
    _line(String.split(str, "\n"), []) |> Enum.reverse
  end

  # Last three chars, so return the last state (ie, all the parsed characters so far: [0,3,4])
  defp _line([<< x1::utf8, x2::utf8, x3::utf8>>, << y1::utf8, y2::utf8, y3::utf8>>, << z1::utf8, z2::utf8, z3::utf8>>], state) do
    [parse(to_string([x1, x2, x3, ?\n, y1, y2, y3, ?\n, z1, z2, z3, ?\n])) | state]
  end
  defp _line([<< x1::utf8, x2::utf8, x3::utf8, tail_one::binary>>, << y1::utf8, y2::utf8, y3::utf8, tail_two::binary>>, << z1::utf8, z2::utf8, z3::utf8, tail_three::binary>>], state) do
    state_out = [parse(to_string([x1, x2, x3, ?\n, y1, y2, y3, ?\n, z1, z2, z3, ?\n])) | state]
    
    # We've got more characters, so carry on.
    _line([tail_one, tail_two, tail_three], state_out)
  end
  # As a result of the String.split, we get an extra blank element at the end of the three lines.
  defp _line([<< x1::utf8, x2::utf8, x3::utf8, tail_one::binary>>, << y1::utf8, y2::utf8, y3::utf8, tail_two::binary>>, << z1::utf8, z2::utf8, z3::utf8, tail_three::binary>>, _], state) do
    state_out = [parse(to_string([x1, x2, x3, ?\n, y1, y2, y3, ?\n, z1, z2, z3, ?\n])) | state]
    
    # We've got more characters, so carry on.
    _line([tail_one, tail_two, tail_three], state_out)
  end

  @doc ~S"""
  User Story 2

  Valiate a valid bank account number

  Example:
      iex> Bank.valid_account_number?([3,4,5,8,8,2,8,6,5])
      iex> true
  """
  def valid_account_number?(account_no) do
    case _checksum(Enum.reverse(account_no), %{}) |> rem(11) do
      0 -> true
      _ -> false
    end
  end

  defp _checksum([], state), do: state[:acc]
  defp _checksum([head | tail] = number, state) do
    case length(number) do
      9 -> _checksum(tail, %{counter: 2, acc: head})
      _ ->
        sum = state[:counter] * head
        _checksum(tail, %{counter: state[:counter] + 1, acc: state[:acc] + sum})
    end
  end

  @doc ~S"""
  User story 3
  """
  def account(str) do
    line(str) |> _check_account
  end
  defp _check_account(account_no_list) do
    case "?" in account_no_list do
      true -> Enum.join(account_no_list) <> " ILL"
      _ ->
        case valid_account_number?(account_no_list) do
          true -> Enum.join(account_no_list, "")
          false -> Enum.join(account_no_list, "") <> " ERR"
        end
    end
  end
    
    


  @doc ~S"""
  Parse a single digit.

  Example:
      iex> six = """
      iex>  _ 
      iex> |_
      iex> |_|
      iex> """
      " _ \n|_\n|_|\n"
      iex> Bank.parse(six)
      6
      iex> 

  """
  def parse("""
             _ 
            | |
            |_|
            """), do: 0


  def parse("""
               
             | 
             | 
            """), do: 1

  def parse("""
             _ 
             _|
            |_ 
            """), do: 2

  def parse("""
             _ 
             _|
             _|
            """), do: 3

  def parse("""
               
            |_|
              |
            """), do: 4

  def parse("""
             _ 
            |_ 
             _|
            """), do: 5

  def parse("""
             _ 
            |_ 
            |_|
            """), do: 6

  def parse("""
             _ 
              |
              |
            """), do: 7

  def parse("""
             _ 
            |_|
            |_|
            """), do: 8

  def parse("""
             _ 
            |_|
              |
            """), do: 9

  def parse(_), do: "?"

end
