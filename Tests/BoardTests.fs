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
[<Test>]
let ``Next available drop position is zero when column is empty``() =
    let emptyColumn = Array.create 5 Disc.Empty
    Board.getNextAvailableDropPosition emptyColumn |> should equal (Some 0)

[<Test>]
let ``Next available drop position is x when column is partially full``() =
    let column = [| Disc.Red; Disc.Yellow; Disc.Empty; Disc.Empty |]
    Board.getNextAvailableDropPosition column |> should equal (Some 2)

[<Test>]
let ``Next available drop position is None when column is full``() =
    let fullColumn = Array.create 5 Disc.Red
    Board.getNextAvailableDropPosition fullColumn |> should equal None

 // isInBounds
[<Test>]
let ``Inside grid returns true``() =
    let board = Array2D.create 5 5 Disc.Empty
    Board.isInBounds board (1, 1) |> should equal true

[<Test>]
let ``Length1 outside the grid returns false``() =
    let board = Array2D.create 5 7 Disc.Empty
    Board.isInBounds board (1,5) |> should equal false

[<Test>]
let ``Length2 outside the grid returns false``() =
    let board = Array2D.create 7 5 Disc.Empty
    Board.isInBounds board (5,1) |> should equal false

// getStartOfSequence
[<Test>]
let ``Start of sequence right``() =
    let board = Array2D.create 4 4 Disc.Empty
    Board.getStartOfSequence board (2,2) (1,0) |> should equal (0,2)

[<Test>]
let ``Start of sequence up``() =
    let board = Array2D.create 4 4 Disc.Empty
    Board.getStartOfSequence board (3,2) (0,1) |> should equal (3,0)

[<Test>]
let ``Start of sequence up left``() =
    let board = Array2D.create 5 5 Disc.Empty
    Board.getStartOfSequence board (0,4) (-1,1) |> should equal (4,0)

[<Test>]
let ``Start of sequence up right``() =
    let board = Array2D.create 5 5 Disc.Empty
    Board.getStartOfSequence board (3, 3) (1,1) |> should equal (0,0)
