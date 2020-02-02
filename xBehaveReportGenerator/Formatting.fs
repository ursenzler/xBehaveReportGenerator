module xBehaveReportGenerator.Formatting

open System
open Types

let rec format level (tree : Tree) = seq {
    match tree with
    | Namespace(name, children) ->
        if level <= 6 then // markdown knows max heading 6
            let heading = String.replicate level "#"
            heading + " " + name
        else
            "**" + name + "**"
            ""
        yield!
            children
            |> Seq.sortBy (fun x ->
                match x with
                | Namespace(name, _) -> name
                | Example(example, _) -> example)
           |> Seq.collect (format (level + 1))
    | Example(example, steps) ->
        if not (String.IsNullOrWhiteSpace(example)) then "*" + example + "*"
        yield! steps
               |> List.sortBy (fun x -> x.Step)
               |> List.map (fun x -> x.Step + ". " + x.Text)
        "" }