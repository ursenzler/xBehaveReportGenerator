module xBehaveReportGenerator.Types

open System

type ParsedStep = // what we get out from the trx file
    {
        ExecutionId : Guid
        TestId : Guid
        Method : string
        Example : string
        Step : string
        Text : string
    }

type Name = string

type Example = string

type Step =
    {
        Step : string // the step number (e.g. 01)
        Text : string
    }

type Info = // a single example (scenarios without examples are represented as a scenario with a single empty example)
    {
        Namespaces : string list
        Example : string
        Steps : Step list
    }

type Tree = // the document structure
    | Namespace of string*Tree list // namespaces, fixture, scenario
    | Example of Example*Step list