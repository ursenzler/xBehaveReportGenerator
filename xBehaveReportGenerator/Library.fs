open System.IO

open Argu
open xBehaveReportGenerator.Parsing
open xBehaveReportGenerator.Structure
open xBehaveReportGenerator.Formatting

type CLIArguments =
    | [<AltCommandLine("-i")>]Input of path:string
    | [<AltCommandLine("-o")>]Output of path:string
    | [<AltCommandLine("-t")>]Title of title:string option
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Input _ -> @"specify an input trx file created with `dotnet test c:\your\spec\assembly.csproj --logger:trx -r c:\your\output\folder`."
            | Output _ -> "specify the output report file."
            | Title _ -> "an optional title for the report"

[<EntryPoint>]
let main argv =
    let parser = ArgumentParser.Create<CLIArguments>(programName = "xBehaveReportGenerator.dll")
    let results = parser.Parse argv

    let input = results.GetResult Input
    let output = results.GetResult Output
    let title =
        match results.TryGetResult Title with
        | Some t -> t.Value
        | None -> ""

    printfn "reading trace file %s" input
    let data = xBehaveReportGenerator.Types.Data.Load(input)

    printfn "parsing test data"
    let infos = parseData data

    printfn "building structure"
    let tree = structurize title infos

    printfn "compacting structure"
    let compactedTree = compact tree ""

    printfn "creating report"
    let report = format 1 compactedTree

    printfn "writing report to %s" output
    File.WriteAllLines(output, report)

    printfn "done"
    0




