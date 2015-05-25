module Program
// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open Game
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
    Game.start "Phil" "Computer" |> ignore
    
    Console.ReadLine() |> ignore
    0 // return an integer exit code