Module Module1

    Dim temp_random As Integer
    Dim bank As Integer = 10000
    Dim bet As Integer = 0

    Declare Function GetAsyncKeyState Lib "user32" _
        (ByVal vKey As Long) As Integer
    Declare Sub Sleep Lib "kernel32" Alias "Sleep" _
        (ByVal dwMilliseconds As Long)

    Sub Main()

        Console.ForegroundColor = ConsoleColor.Black
        Console.BackgroundColor = ConsoleColor.DarkCyan
        Console.Clear()

        Console.CursorVisible = False
        Console.WindowHeight = 50
        Console.WindowWidth = 100

        'these arrays hold randomly generated cards - row 1 is card value, row 2 is card suit, row 3 is simplified card value, row 4 is simplified suit
        Dim player_hand(,) As String = New String(3, 5) {{" ", " ", " ", " ", " ", " "}, _
                                                         {" ", " ", " ", " ", " ", " "}, _
                                                         {" ", " ", " ", " ", " ", " "}, _
                                                    {" ", " ", " ", " ", " ", " "}}

        Dim dealer_hand(,) As String = New String(3, 5) {{" ", " ", " ", " ", " ", " "}, _
                                                         {" ", " ", " ", " ", " ", " "}, _
                                                         {" ", " ", " ", " ", " ", " "}, _
                                                    {" ", " ", " ", " ", " ", " "}}

        Randomize()

        'Randomly generating the cards for the player and dealer
        For i = 0 To 5

            random_card(player_hand(0, i), player_hand(1, i), player_hand(2, i), player_hand(3, i))
            random_card(dealer_hand(0, i), dealer_hand(1, i), dealer_hand(2, i), dealer_hand(3, i))

        Next

        'Writing general information to the screen

        'Title
        Console.SetCursorPosition(35, 1)
        Console.ForegroundColor = ConsoleColor.Red
        Console.Write(Chr(3) & Chr(4))
        Console.ForegroundColor = ConsoleColor.White
        Console.Write(" Welcome to Sascha's Blackjack! ")
        Console.ForegroundColor = ConsoleColor.Black
        Console.Write(Chr(5) & Chr(6))

        'Headers
        Console.SetCursorPosition(2, 2)
        Console.Write("DEALER'S CARDS")
        Console.SetCursorPosition(3, 24)
        Console.Write("YOUR CARDS")

        'Bank
        Console.SetCursorPosition(83, 1)
        Console.Write("Bank: $" & bank)

        'Divider
        For i = 0 To 99

            Console.SetCursorPosition(i, 42)
            Console.Write("_")

        Next

        Call get_bet()

        Do

            Console.SetCursorPosition(83, 1)
            Console.Write("Bank: $" & bank)

            print_card(4, 5, dealer_hand(2, 0), dealer_hand(3, 0), dealer_hand(0, 0), dealer_hand(1, 0)) 'prints the dealer's upcard

            print_card(4, 27, player_hand(2, 0), player_hand(3, 0), player_hand(0, 0), player_hand(1, 0)) 'prints your 1st card

            print_card(24, 27, player_hand(2, 1), player_hand(3, 1), player_hand(0, 1), player_hand(1, 1)) 'prints your 2nd card


            Console.SetCursorPosition(5, 44)
            Console.BackgroundColor = ConsoleColor.White
            Console.Write("Hit me!")
            Console.SetCursorPosition(15, 44)
            Console.BackgroundColor = ConsoleColor.DarkCyan
            Console.Write("Stand!")
            Console.SetCursorPosition(24, 44)
            Console.Write("Double Down!")

        Loop

        Sleep(1000000000000000)

    End Sub

    Sub get_bet()

        Console.SetCursorPosition(38, 44)

        Console.Write("Please enter your bet: ")
        bet = Console.Read

        If bet > bank Then

            Console.SetCursorPosition(38, 44)
            Console.Write("You do not have enough money!")
            Sleep(1000)
            Call get_bet()

        Else

            bank = bank - bet
            Console.SetCursorPosition(38, 44)
            Console.Write(Space(30))

        End If

    End Sub

    Function random_card(ByRef value As String, ByRef suit As String, ByRef simpvalue As String, ByRef simpsuit As String)

        'this function randomly assigns a card value (ace, 2-10, J, Q, K) and a suit to slots in the dealer / the player's card array using random_int function

        temp_random = random_int(1, 13)

        If temp_random = 1 Then
            value = "Ace"
            simpvalue = "A"
        ElseIf temp_random = 2 Then
            value = "Two"
            simpvalue = "2"
        ElseIf temp_random = 3 Then
            value = "Three"
            simpvalue = "3"
        ElseIf temp_random = 4 Then
            value = "Four"
            simpvalue = "4"
        ElseIf temp_random = 5 Then
            value = "Five"
            simpvalue = "5"
        ElseIf temp_random = 6 Then
            value = "Six"
            simpvalue = "6"
        ElseIf temp_random = 7 Then
            value = "Seven"
            simpvalue = "7"
        ElseIf temp_random = 8 Then
            value = "Eight"
            simpvalue = "8"
        ElseIf temp_random = 9 Then
            value = "Nine"
            simpvalue = "9"
        ElseIf temp_random = 10 Then
            value = "Ten"
            simpvalue = "10"
        ElseIf temp_random = 11 Then
            value = "Jack"
            simpvalue = "J"
        ElseIf temp_random = 12 Then
            value = "Queen"
            simpvalue = "Q"
        ElseIf temp_random = 13 Then
            value = "King"
            simpvalue = "K"
        End If

        temp_random = random_int(1, 4)

        If temp_random = 1 Then
            suit = "of Clubs"
            simpsuit = Chr(5)
        ElseIf temp_random = 2 Then
            suit = "of Spades"
            simpsuit = Chr(6)
        ElseIf temp_random = 3 Then
            suit = "of Hearts"
            simpsuit = Chr(3)
        ElseIf temp_random = 4 Then
            suit = "of Diamonds"
            simpsuit = Chr(4)
        End If

    End Function

    Function random_int(ByVal small As Integer, ByVal big As Integer) As Integer 'function that returns a random number from a given range of numbers

        Return Int(Rnd() * (big - small + 1)) + small

    End Function

    Function print_card(ByVal x As Integer, ByVal y As Integer, ByVal cardval As String, ByVal suitval As String, ByVal card As String, ByVal suit As String)
        'function that prints graphical cards to the screen at a given coordinate

        Console.SetCursorPosition(x - 1, y - 1)
        Console.Write(card & " " & suit)

        For i = 0 To 10

            Console.SetCursorPosition(i + x, y + 10)
            Console.Write("_")
            Console.SetCursorPosition(x, i + y)
            Console.Write("|")
            Console.SetCursorPosition(x + 10, y + i)
            Console.Write("|")
            Console.SetCursorPosition(i + x, y)
            Console.Write("_")

        Next

        If suitval = Chr(3) Then

            Console.ForegroundColor = ConsoleColor.Red

        ElseIf suitval = Chr(4) Then

            Console.ForegroundColor = ConsoleColor.Red

        End If

        Console.SetCursorPosition(x + 4, y + 5)
        Console.Write(cardval & " " & suitval)

        Console.ForegroundColor = ConsoleColor.Black

        Sleep(400)

    End Function

End Module


