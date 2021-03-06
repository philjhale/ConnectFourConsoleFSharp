﻿module Board
open System

    // Not ideal. In theory you could assign Empty to a player
    type Disc = Red | Yellow | Empty

    type Board = Disc[,]

    let private isInBounds (board:Board) coordindate =
        try
            match coordindate with
            | (x, y) -> board.[y, x] |> ignore 
            true
        with    
            | :? System.IndexOutOfRangeException -> false

    let private getColumn (board:Board) columnIndex =
        let toArrayFromColumn (arr:_[,]) = 
            Array.init arr.Length (fun i -> arr.[i, 0])

        board.[0..,columnIndex..columnIndex] |> toArrayFromColumn

    let private tryGetColumn (board:Board) columnIndex =
        match isInBounds board (columnIndex, 0) with
        | true -> Some (getColumn board columnIndex)
        | false -> None

    let create =
        // length1 is the number of arrays (rows or y)
        // length2 is the size of each array (columns or x)
        Array2D.create 6 7 Disc.Empty

    let private getNextAvailableDropPosition dropColumn =
        dropColumn |> Array.findIndex (fun y -> y = Disc.Empty)

    let private tryGetNextAvailableDropPosition dropColumn =
        dropColumn |> Array.tryFindIndex (fun y -> y = Disc.Empty)

    type DropResult =
        | Success
        | OutOfBounds
        | ColumnFull

    let canDrop board columnNumber = 
        let columnIndex = columnNumber - 1

        match isInBounds board (columnIndex, 0) with
        | true ->
            match tryGetNextAvailableDropPosition <| getColumn board columnIndex with
            | Some position -> Success
            | None -> ColumnFull
        | false -> OutOfBounds
  

    let drop board disc columnNumber =
        let boardCopy = Array2D.copy board
        let dropColumn = getColumn board (columnNumber-1)
        let rowIndex = getNextAvailableDropPosition dropColumn
        Array2D.set boardCopy rowIndex (columnNumber-1) disc
        boardCopy

    let showBoard board =
        let maxY = (Array2D.length1 board) - 1
        let maxX = (Array2D.length2 board) - 1
        for x in 0 .. maxX do
            printf "%d\t" (x+1)
        printfn ""
        for y in maxY .. -1 .. 0 do
            for x in 0 .. maxX do
                printf "%A\t" board.[y,x]
            printf "\n"
    
    let private getDiscsForCoordinates (board:Board) coordinates =
        let rec getDiscsForCoordinatesInternal (board:Board) coordinates discs =
            match coordinates with
            | (head::tail) -> 
                let disc = board.[snd head, fst head]
                getDiscsForCoordinatesInternal board tail (disc::discs)
            | [] -> discs

        getDiscsForCoordinatesInternal board coordinates []

    let rec private isConnectFourInList list =
        match list with
        | [Disc.Red;Disc.Red;Disc.Red;Disc.Red] -> true
        | [Disc.Yellow;Disc.Yellow;Disc.Yellow;Disc.Yellow] -> true
        | [] -> false
        | head::tail -> isConnectFourInList tail

    let private getNextCoordinateInDirection coordindate direction =
        let x = fst coordindate
        let y = snd coordindate
        match direction with
        | (dx, dy) -> (x + dx, y + dy)

    let private getCoordinatesInDirection board startCordinate direction =
        let rec getCoordinatesInDirectionInternal board currentCoordinate direction coordinateList =
           match isInBounds board currentCoordinate with
           | true -> 
                let nextCoordinate = getNextCoordinateInDirection currentCoordinate direction
                getCoordinatesInDirectionInternal board nextCoordinate direction (currentCoordinate::coordinateList)
           | false -> List.rev coordinateList

        getCoordinatesInDirectionInternal board startCordinate direction []

    let private getFirstCoordinateInDirection board currentCoordinate direction =
        let reverseDirection direction = 
            match direction with
            | (x, y) -> (x * -1, y * -1)
        
        let reversedDirection = reverseDirection direction

        let seq = getCoordinatesInDirection board currentCoordinate reversedDirection
        seq |> List.rev |> List.head

    let private getAllCoordinatesInDirection board startCordinate direction =
        let firstCoordinate = getFirstCoordinateInDirection board startCordinate direction
        getCoordinatesInDirection board firstCoordinate direction

    let private getLastDropRowIndex board x =
        let col = getColumn board x
        let firstEmptyRowIndex = col |> Array.findIndex (fun y -> y = Disc.Empty)
        firstEmptyRowIndex - 1

    let isConnectFour board lastDropColumnNumber =
        let lastDropColumnIndex = lastDropColumnNumber - 1
        let lastDropRowIndex = getLastDropRowIndex board lastDropColumnIndex

        let isConnectFourInternal board lastDropCoordinate searchDirection =
            searchDirection
            |> getAllCoordinatesInDirection board lastDropCoordinate
            |> getDiscsForCoordinates board
            |> isConnectFourInList

        let isConnectFourInDirection = isConnectFourInternal board (lastDropColumnIndex, lastDropRowIndex)
        let allDirections = [(1, 0);(0, 1);(1, 1);(-1, 1)]
        let isConnectFourInAnyDirection = List.fold (fun acc direction -> (isConnectFourInDirection direction)::acc) [] allDirections  

        isConnectFourInAnyDirection |> List.exists (fun x -> x = true) 