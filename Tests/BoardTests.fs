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

// canDrop
[<Test>]
let ``Can drop in empty column``() =
    let board = createBoard
    Board.canDrop board 1 |> should equal DropResult.Success

[<Test>]
let ``Dropping at column 100 is out of bounds``() =
    let board = createBoard
    Board.canDrop board 100 |> should equal DropResult.OutOfBounds

[<Test>]
let ``Dropping in a full column is full column``() =
    let board = createBoard |> fillColumn 0 [Disc.Red; Disc.Red; Disc.Red; Disc.Red; Disc.Red; Disc.Red]
    Board.canDrop board 1 |> should equal DropResult.ColumnFull

// drop
[<Test>]
let ``Drop in empty board updates board correctly``() =
    let board = createBoard
    let boardAfterDrop = createBoard |> fillColumn 0 [Disc.Red]
    Board.drop board Disc.Red 1 |> should equal boardAfterDrop

[<Test>]
let ``Drop in partially full column updates board correctly``() =
    let board = createBoard |> fillColumn 1 [Disc.Red; Disc.Yellow; Disc.Yellow]
    let boardAfterDrop = createBoard |> fillColumn 1 [Disc.Red; Disc.Yellow; Disc.Yellow; Disc.Yellow]
    Board.drop board Disc.Yellow 2 |> should equal boardAfterDrop