module Board
open System

    // Not ideal. In theory you could assign Empty to a player
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
    

    // Maybe type GridDirection = {X:int,; Y:int}?
//    type GridDirection = GridDirection of int * int // x * y
//    
//    let up = GridDirection (0,1)  
//    let right = GridDirection (1,0)  
//    let upRight = GridDirection (1,1)
//    let upLeft = GridDirection (-1,1)    

    let isInBounds (board:Board) columnIndex rowIndex =
        try
            board.[rowIndex, columnIndex] |> ignore 
            true
        with    
            | :? System.IndexOutOfRangeException -> false

    let rec isConnectFourInList list =
        match list with
        | [Disc.Red;Disc.Red;Disc.Red;Disc.Red] -> true
        | [Disc.Yellow;Disc.Yellow;Disc.Yellow;Disc.Yellow] -> true
        | [] -> false
        | head::tail -> isConnectFourInList tail

       
    let getPrevious columnIndex rowIndex direction =
        match direction with
        | (x, y) -> (columnIndex + x, rowIndex + y)

    // This is poo
    let getStartOfSequence board columnIndex rowIndex direction =
        let reverseDirection direction = 
            match direction with
            | (x, y) -> (x * -1, y * -1)
        
        let rev = reverseDirection direction

        let rec findStart board columnIndex rowIndex rev =
            let previousPoint = getPrevious columnIndex rowIndex rev
            let previousColumnIndex = fst previousPoint // Awful
            let previousRowIndex = snd previousPoint
            match isInBounds board previousColumnIndex previousRowIndex with
            | true -> findStart board previousColumnIndex previousRowIndex rev
            | false -> (columnIndex, rowIndex)
        
        findStart board columnIndex rowIndex rev

      // TODO
//    let getSequence board startPoint =
//        let getSequenceInternal board startPoint [] =
                // Get next point, if in bounds add to the list and call function. Else return list
//            match isInBounds board  with
//            | (x, y) -> 

    // TODO Change column and row to x and y?
    // TODO Index or number
    let isConnectFour board lastDropColumn lastDropRow = 
//        let directionsToCheck = [up; right; upRight; upLeft]
//        directionsToCheck 
//            |> List.iter checkForConnectFour
        // For each direction
            // Reverse direction
            // Find start of sequence
            // For each element in seq
                // Get next four. Are they the same colour?
        false



//        0 0 0 0 0
//        0 0 0 0 0
//        0 0 0 0 0
//        0 0 0 0 0
//        0 0 0 0 0