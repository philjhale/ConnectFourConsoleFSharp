module Game

open Board
open Player

    let private (|Int|_|) str =
        match System.Int32.TryParse(str) with
        | (true,int) -> Some(int)
        | _ -> None

    let rec private getValidColumnNumberInput board getColumnNumberInput =
        match getColumnNumberInput() with
            | Int i -> 
                match Board.canDrop board i with
                | Success -> i
                | OutOfBounds | ColumnFull -> 
                    printfn "You can't drop a disc there fool"
                    getValidColumnNumberInput board getColumnNumberInput
            | _ -> 
                printfn "Please enter an integer"
                getValidColumnNumberInput board getColumnNumberInput

    let private getNextPlayer players currentPlayer =
        players |> List.find (fun player -> player.Name <> currentPlayer.Name)

    let rec private takeTurn board players currentPlayer =
        printfn "%s, enter a column number:" currentPlayer.Name
        
        let validColumnNumber = getValidColumnNumberInput board <| Player.getInput currentPlayer.Type
        let newBoard = Board.drop board currentPlayer.Disc validColumnNumber
        
        Board.showBoard newBoard

        match Board.isConnectFour newBoard validColumnNumber with
        | true -> printf "%s wins!" currentPlayer.Name
        | false -> takeTurn newBoard players <| getNextPlayer players currentPlayer

    let start player1Name player2Name =
        let human = Player.create player1Name Disc.Red PlayerType.Human
        let computer = Player.create player2Name Disc.Yellow PlayerType.Computer
    
        let board = Board.create
        Board.showBoard board

        takeTurn board [human; computer] human |> ignore
