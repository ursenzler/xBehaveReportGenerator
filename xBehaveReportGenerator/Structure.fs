module xBehaveReportGenerator.Structure

open Types

let rec createTree info =
    match info.Namespaces with
    | head::tail ->
        let newInfo = { info with Namespaces = tail }
        Namespace(head, List.singleton(createTree(newInfo)))
    | [] ->
        Example(info.Example, info.Steps)

let rec add tree info =
    match info.Namespaces with
    | [] ->
        match tree with
        | Namespace(name, children) ->
            let siblings = children
            Namespace(name, (createTree info)::siblings)
        | _ ->
            tree
    //TODO: Consider more meaningful names that "head" and "tail"?
    | head :: tail ->
        match tree with
        | Namespace(name, children) ->
            let child =
                children
                |> List.tryFind (fun x ->
                    match x with
                    | Namespace(level, _) when level = head -> true
                    | _ -> false)

            match child with
            | Some next ->
                let siblings = children |> List.except (List.singleton next)
                let nextInfo = { info with Namespaces = tail }
                Namespace(name, (add next nextInfo)::siblings)
            | _ ->
                Namespace(name, (createTree info)::children)

        | _ ->
            //TODO: Can we make this state unrepresentable?
            tree // we should never get here

let structurize title infos =
    let emptyBaum = Namespace(title, List.Empty)
    infos |> List.fold add emptyBaum

let rec compact tree heading =
    match tree with
    | Namespace(name, children) ->
        let compactedHeading = heading + " " + name
        if children.Length > 1 then
            Namespace(compactedHeading, children)
        else
            compact children.Head  (heading + " " + name)
    | _ ->
        tree