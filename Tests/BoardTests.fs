module ConnectFourFSharp.Tests

open Board
open NUnit.Framework
open FsUnit

// create
[<Test>]
let ``Can create board of correct size``() =
    let board = Board.create
    Array2D.length1 board |> should equal 6
    Array2D.length2 board |> should equal 7

// getNextAvailableDropPosition
//[<Test>]
//let ``Next available drop position is zero when column is empty``() =
//    let emptyColumn = Array.create 5 Disc.Empty
//    Board.getNextAvailableDropPosition emptyColumn |> should equal (Some 0)
//
//[<Test>]
//let ``Next available drop position is x when column is partially full``() =
//    let column = [| Disc.Red; Disc.Yellow; Disc.Empty; Disc.Empty |]
//    Board.getNextAvailableDropPosition column |> should equal (Some 2)
//
//[<Test>]
//let ``Next available drop position is None when column is full``() =
//    let fullColumn = Array.create 5 Disc.Red
//    Board.getNextAvailableDropPosition fullColumn |> should equal None
//
// // isInBounds
//[<Test>]
//let ``Coordinate in in bounds of grid returns true``() =
//    let board = Array2D.create 5 5 Disc.Empty
//    Board.isInBounds board (1, 1) |> should equal true
//
//[<Test>]
//let ``Length1 outside bounds of grid returns false``() =
//    let board = Array2D.create 5 7 Disc.Empty
//    Board.isInBounds board (1,5) |> should equal false
//
//[<Test>]
//let ``Length2 outside bounds of grid returns false``() =
//    let board = Array2D.create 7 5 Disc.Empty
//    Board.isInBounds board (5,1) |> should equal false
//
//// getFirstCoordinateInDirection
//[<Test>]
//let ``Get start of sequence right``() =
//    let board = Array2D.create 4 4 Disc.Empty
//    Board.getFirstCoordinateInDirection board (2,2) (1,0) |> should equal (0,2)
//
//[<Test>]
//let ``Get start of sequence up``() =
//    let board = Array2D.create 4 4 Disc.Empty
//    Board.getFirstCoordinateInDirection board (3,2) (0,1) |> should equal (3,0)
//
//[<Test>]
//let ``Get start of sequence up left``() =
//    let board = Array2D.create 5 5 Disc.Empty
//    Board.getFirstCoordinateInDirection board (0,4) (-1,1) |> should equal (4,0)
//
//[<Test>]
//let ``Get start of sequence up right``() =
//    let board = Array2D.create 5 5 Disc.Empty
//    Board.getFirstCoordinateInDirection board (3, 3) (1,1) |> should equal (0,0)
//
//// getCoordinatesInDirection
//[<Test>]
//let ``Get coordinates in direction going right``() =
//    let board = Array2D.create 5 5 Disc.Empty
//    Board.getCoordinatesInDirection board (2, 3) (1,0) |> should equal [2, 3; 3, 3; 4, 3]
//
//[<Test>]
//let ``Get coordinates in direction going up``() =
//    let board = Array2D.create 4 4 Disc.Empty
//    Board.getCoordinatesInDirection board (0, 0) (0, 1) |> should equal [0, 0; 0, 1; 0, 2; 0, 3]
//
//[<Test>]
//let ``Get coordinates in direction going up right``() =
//    let board = Array2D.create 4 4 Disc.Empty
//    Board.getCoordinatesInDirection board (1, 1) (1, 1) |> should equal [1, 1; 2, 2; 3, 3]

// isConnectFour
let fillColumn columnIndex discs board =
    let rec fillColumnInternal board columnIndex discs rowIndex =
        match discs with
        | head::tail -> 
            let boardCopy = Array2D.copy board
            Array2D.set boardCopy rowIndex columnIndex head
            fillColumnInternal boardCopy columnIndex tail (rowIndex + 1)
        | [] -> board

    fillColumnInternal board columnIndex discs 0

let createBoard = 
    Array2D.create 6 7 Disc.Empty // TODO Why does Board.create fail?

//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Red	Red	    Red	    Red	    Empty	Empty	Empty
[<Test>]
let ``Connect four found with four red disc in row``() =
    let board = createBoard
                           |> fillColumn 0 [Disc.Red]
                           |> fillColumn 1 [Disc.Red]
                           |> fillColumn 2 [Disc.Red]
                           |> fillColumn 3 [Disc.Red]
    Board.showBoard board
    Board.isConnectFour board 1 |> should equal true

//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Red	Red	    Red	    Empty   Empty	Empty	Empty
[<Test>]
let ``Connect four NOT found with three red discs in row``() =
    let board = createBoard
                           |> fillColumn 0 [Disc.Red]
                           |> fillColumn 1 [Disc.Red]
                           |> fillColumn 2 [Disc.Red]
    Board.showBoard board
    Board.isConnectFour board 1 |> should equal false

//Empty 	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	    Empty	Empty	Empty	Empty	Empty	Empty	
//Yellow	Empty	Empty	Empty	Empty	Empty	Empty	
//Yellow	Empty	Empty	Empty	Empty	Empty	Empty	
//Yellow	Empty	Empty	Empty	Empty	Empty	Empty	
//Yellow	Empty	Empty	Empty	Empty	Empty	Empty
[<Test>]
let ``Connect four found with four yellow discs in column``() =
    let board = createBoard |> fillColumn 0 [Disc.Yellow; Disc.Yellow; Disc.Yellow; Disc.Yellow]
    Board.showBoard board
    Board.isConnectFour board 1 |> should equal true

//Empty 	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	    Empty	Empty	Empty	Empty	Empty	Empty	
//Empty 	Empty	Empty	Empty	Empty	Empty	Empty	
//Yellow	Empty	Empty	Empty	Empty	Empty	Empty	
//Yellow	Empty	Empty	Empty	Empty	Empty	Empty	
//Yellow	Empty	Empty	Empty	Empty	Empty	Empty
[<Test>]
let ``Connect four NOT found with three yellow discs in column``() =
    let board = createBoard |> fillColumn 0 [Disc.Yellow; Disc.Yellow; Disc.Yellow]
    Board.showBoard board
    Board.isConnectFour board 1 |> should equal false

//Empty	    Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	    Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	    Empty	Empty	Yellow	Empty	Empty	Empty	
//Empty	    Empty	Yellow	Red 	Empty	Empty	Empty	
//Empty	    Yellow	Red 	Red 	Empty	Empty	Empty	
//Yellow	Red 	Red	    Red 	Empty	Empty	Empty
[<Test>]
let ``Connect four found with four yellow discs in up right diagonal``() =
    let board = createBoard
                           |> fillColumn 0 [Disc.Yellow]
                           |> fillColumn 1 [Disc.Red; Disc.Yellow]
                           |> fillColumn 2 [Disc.Red; Disc.Red; Disc.Yellow]
                           |> fillColumn 3 [Disc.Red; Disc.Red; Disc.Red; Disc.Yellow]

    Board.showBoard board
    Board.isConnectFour board 4 |> should equal true

//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Empty	Empty	Empty	Empty	
//Empty	Empty	Empty	Red	    Empty	Empty	Empty	
//Empty	Empty	Empty	Yellow	Red 	Empty	Empty	
//Empty	Empty	Empty	Yellow	Yellow	Red	    Empty	
//Empty	Empty	Empty	Yellow	Yellow	Yellow	Red
[<Test>]
let ``Connect four found with four red discs in up left diagonal``() =
    let board = createBoard
                           |> fillColumn 6 [Disc.Red]
                           |> fillColumn 5 [Disc.Yellow; Disc.Red]
                           |> fillColumn 4 [Disc.Yellow; Disc.Yellow; Disc.Red]
                           |> fillColumn 3 [Disc.Yellow; Disc.Yellow; Disc.Yellow; Disc.Red]
    Board.showBoard board
    Board.isConnectFour board 4 |> should equal true