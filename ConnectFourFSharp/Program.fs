module Program
open Game
open System

// TODO Reverse board so it can be more easily displayed. E.g. printfn "%A" board
// TODO Is List.fold useful anywhere?
// TODO Abstract printfn?
[<EntryPoint>]
let main argv = 
    Game.start "Phil" "Computer" |> ignore
    
    Console.ReadLine() |> ignore
    0