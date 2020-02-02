module xBehaveReportGenerator.Types

open System
open FSharp.Data

//TODO: Consider single-case discriminated unions here
/// what we get out from the trx file
type ParsedStep =
    { ExecutionId : Guid
      TestId : Guid
      Method : string
      Example : string
      Step : string
      Text : string }

//TODO: Consider a single case DU
type Name = string
//TODO: Consider a single case DU
type Example = string

type Step =
    { /// the step number (e.g. 01)
      Step : string
      Text : string }

//TODO: Consider a two-case DU for scenarios without examples as opposed to identifying this case through a code comment?
/// a single example (scenarios without examples are represented as a scenario with a single empty example)
type Info =
    { Namespaces : string list
      Example : string
      Steps : Step list }

/// the document structure
type Tree =
    //TODO: Consider encoding this comment in the type system? Not sure if that's possible.
      /// namespaces, fixture, scenario
    | Namespace of string * Tree list
    | Example of Example * Step list

type Data = XmlProvider<"SampleData.xml">