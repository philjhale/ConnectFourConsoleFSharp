module Player
open Board
open System

    type PlayerType = Human | Computer
    type Player  = {Name:string; Disc:Disc; Type:PlayerType}

    let create name disc playerType = 
        {Name = name; Disc = disc; Type = playerType}

    let getInput playerType =
        match playerType with
        | Human -> fun () -> Console.ReadLine()
        | Computer -> fun () -> System.Random().Next(1, 7).ToString()