module Program
// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open Board
open Player
open System

// Board needs to
// - Allow discs to be dropped
// - Test for connect 4, up, down and diagonal
// - Be displayed fairly easily

// Options for representing board
// - Multi dimensional array
// - Array of lists
// - List of lists
// - types. e.g. type Cell = Disc, type Column = list Disc (?), type Board = list Column
// - Array2D
// - Regular multi dimensional array (array of arrays)

// http://geekswithblogs.net/Domagala/archive/2009/06/17/connect-four-in-f-part-2.aspx
// http://stackoverflow.com/questions/2909310/f-multidimensional-array-types


[<EntryPoint>]
let main argv = 
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


    printfn "Game started"
    let human = Player.create "Phil" Disc.Red PlayerType.Human
    let computer = Player.create "Computer" Disc.Yellow PlayerType.Computer
    
    let board = Board.create
    Board.showBoard board

    takeTurn board [human; computer] |> ignore
    
    Console.ReadLine() |> ignore
    0 // return an integer exit code