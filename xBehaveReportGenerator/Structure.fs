module xBehaveReportGenerator.Structure

open Types

let rec createTree info =
    match info.Namespaces with
    | head::tail ->
        let newInfo = { info with Namespaces = tail }
        Namespace(head, [createTree(newInfo)])
    | [] ->
        Example(info.ExampleValues, info.Steps)

let rec add tree info =
    match info.Namespaces with
    | [] ->
        match tree with
        | Namespace(name, children) ->
            let siblings = children
            Namespace(name, (createTree info)::siblings)
        | _ ->
            tree
    | head::tail ->
        match tree with
        | Namespace(name, children) ->
            let child =
                children
                |> List.tryFind (function
                    | Namespace(level, _) -> level = head
                    | _ -> false)

            match child with
            | Some next ->
                let siblings =
                    children
                    |> List.except (List.singleton next)
                let nextInfo = { info with Namespaces = tail }
                Namespace(name, (add next nextInfo)::siblings)
            | _ ->
                Namespace(name, (createTree info)::children)

        | _ ->
            tree // we should never get here

let structurize title infos =
    let emptyTree = Namespace(title, List.Empty)
    infos
    |> List.fold add emptyTree

let compact =
    let rec compact' heading tree =
        match tree with
        | Namespace(name, children) ->
            let compactedHeading = heading + " " + name
            if children.Length > 1 then
                Namespace(compactedHeading, children)
            else
                compact' compactedHeading children.Head

        | _ -> tree
    compact' ""