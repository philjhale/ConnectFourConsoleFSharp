module Game

open Board
open Player

    let (|Int|_|) str =
        match System.Int32.TryParse(str) with
        | (true,int) -> Some(int)
        | _ -> None

    let rec getValidColumnNumberInput getColumnNumberInput =
        match getColumnNumberInput() with
            | Int i -> i
            | _ -> 
                printfn "Please enter an integer"
                getValidColumnNumberInput getColumnNumberInput

    let rec getValidDrop drop =
        match drop with
        | Success board -> board
        | OutOfBounds board ->
            printf "That column is out of bounds, please choose another"
            getValidDrop drop

    let rec takeTurn board players =
        // Another way. Pass in current player to takenTurn
        // Recursive call is players |> List.find (fun player -> player.Name <> currentPlayer.Name)
        let currentPlayer = List.nth players 0

        printfn "%s, enter a column number:" currentPlayer.Name
        
        let validColumnNumber = getValidColumnNumberInput <| Player.getInput currentPlayer.Type
        let newBoard = getValidDrop <| Board.drop validColumnNumber board currentPlayer.Disc
        
        Board.showBoard newBoard

        // TODO Check for connect four
        // TODO Handle out of bounds input. E.g. Column 100
        match Board.isConnectFour newBoard validColumnNumber with
        | true -> printf "%s wins!" currentPlayer.Name
        | false -> takeTurn newBoard <| List.rev players
