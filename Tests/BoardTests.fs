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