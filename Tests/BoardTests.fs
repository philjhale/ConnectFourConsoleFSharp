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