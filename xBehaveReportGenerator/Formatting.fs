module xBehaveReportGenerator.Formatting

open System
open Types

let format =
    let rec format' level tree =
        seq {
            match tree with
            | Namespace(name, children) ->
                if level <= 6 then // markdown knows max heading 6
                    let heading = String.replicate level "#"
                    heading + " " + name
                else
                    "**" + name + "**"
                    ""
                yield! children
                       |> Seq.sortBy (function
                           | Namespace(name, _) ->
                               name
                           | Example(ExampleValues.ExampleValues(example), _) ->
                               example)
                       |> Seq.collect (format' (level + 1))
            | Example(ExampleValues(example), steps) ->
                if not (String.IsNullOrWhiteSpace(example)) then "*" + example + "*"
                yield! steps
                       |> List.sortBy (fun x -> x.StepNumber)
                       |> List.map (fun x -> x.StepNumber + ". " + x.Text)
                ""
        }
    format' 1