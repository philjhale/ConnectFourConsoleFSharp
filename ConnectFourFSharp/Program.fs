module Program
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

// TODO Handle out of bounds input. E.g. Column 100
// TODO getNextAvailableDropPosition Handle column full, board full, more?
// TODO Reverse board so it can be more easily displayed. E.g. printfn "%A" board
// TODO Is List.fold useful anywhere?
// TODO Abstract printfn?
[<EntryPoint>]
let main argv = 
    Game.start "Phil" "Computer" |> ignore
    
    Console.ReadLine() |> ignore
    0 // return an integer exit code