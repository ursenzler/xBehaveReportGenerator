module xBehaveReportGenerator.Parsing

open System.Text.RegularExpressions
open Types

let (|Step|_|) candidate =
    let pattern = Regex(@"(?<method>[\w\._@\+]*)\((?<example>.*)\) \[(?<step>.*)\] (?<text>.*)")
    let matches = pattern.Match candidate
    if matches.Success then
        Some(matches.Groups.["method"].Value.Trim(),
             matches.Groups.["example"].Value.Trim(),
             matches.Groups.["step"].Value.Trim(),
             matches.Groups.["text"].Value.Trim())
    else
        None

let parseData (data : xBehaveReportGenerator.Types.Data.TestRun) =
    data.Results.UnitTestResults
        |> Array.map (fun x ->
            match x.TestName with
            | Step (method, example, step, text) ->
                { ExecutionId = x.ExecutionId
                  TestId = x.TestId
                  Method = method
                  Example = example
                  Step = step
                  Text = text }
                |> Some
            | _ ->
                None)
        |> Array.choose id
        |> Array.toList
        |> List.groupBy (fun x -> x.TestId, x.Method, x.Example)
        |> List.map (fun ((_, method, example), steps) ->
            { Namespaces =
                method.Split('.')
                |> Array.toList
              Example = example
              Steps =
                steps
                |> List.map (fun x ->
                    { Step = x.Step
                      Text = x.Text }) })
