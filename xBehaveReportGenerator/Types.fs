module xBehaveReportGenerator.Types

open System
open FSharp.Data

type ParsedStep = // what we get out from the trx file
    { ExecutionId : Guid
      TestId : Guid
      Method : string
      Example : string
      Step : string
      Text : string }

type ExampleValues = ExampleValues of string

type Step =
    { StepNumber : string
      Text : string }

type Info = // a single example (scenarios without examples are represented as a scenario with a single empty example)
    { Namespaces : string list
      ExampleValues : ExampleValues
      Steps : Step list }

type Tree = // the document structure
    | Namespace of string*Tree list // namespaces, fixture, scenario
    | Example of ExampleValues*Step list

type Data = XmlProvider<"../xBehaveReportGenerator/SampleTestRun.xml">