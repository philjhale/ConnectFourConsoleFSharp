module Board
open System

    type Disc = Red | Yellow | Empty

    type Board = Disc[,]

    // http://stackoverflow.com/a/2909339
    let toArrayFromColumn (arr:_[,]) = 
      Array.init arr.Length (fun i -> arr.[i, 0])

    let getColumn (board:Board) columnIndex =
        board.[0..,columnIndex..columnIndex] |> toArrayFromColumn
    
    let create =
        // length1 is the number of arrays (rows or y)
        // length2 is the size of each array (columns or x)
        Array2D.create 6 7 Disc.Empty

    let getNextAvailableDropPosition dropColumn =
        dropColumn |> Array.tryFindIndex (fun y -> y = Disc.Empty)

    type DropResult =
        | Success of Disc[,]
        | OutOfBounds of Disc[,]


    // TODO Handle errors. Error/success function?
    // TODO Move failed function position?
    let drop columnNumber board disc =
        let boardCopy = Array2D.copy board
        let dropColumn = getColumn board (columnNumber-1)

        match getNextAvailableDropPosition dropColumn with
        | Some position -> 
            Array2D.set boardCopy position (columnNumber-1) disc
            Success boardCopy
        | None -> OutOfBounds board
        // TODO Column full, more?

    // TODO Reverse board so it can be more easily displayed
    // TODO Move elsewhere?    
    let showBoard board =
        let maxY = (Array2D.length1 board) - 1
        let maxX = (Array2D.length2 board) - 1
        for y in maxY .. -1 .. 0 do
            for x in 0 .. maxX do
                printf "%A\t" board.[y,x]
            printf "\n"
        
    let isConnectFour board = 
        false