# xBehaveReportGenerator
Creates a (markdown) report from xBehave specs.

## Install
Install the report generator as a global tool with:

```dotnet tool install --global xBehaveReportGenerator```

## Create a trx file
Run the test runner with the trx logger enabled:

```dotnet test c:\your\specs.csproj --logger:trx -r c:\your\output\folder```

## run
Run the report generator with:

```xBehaveReportGenerator -i c:\your\output\folder\trace.trx -o c:\your\output\folder\report.md```

## CLI options

```
--input, -i <path>    specify an input trx file created with `dotnet test c:\your\spec\assembly.csproj --logger:trx -r c:\your\output\folder`.
--output, -o <path>   specify the output report file.
--title, -t [<title>] an optional title for the report
--help                display this list of options.
```

## Sample

The xBehave report generator will generate from this:

```c#
namespace Sample
{
    public class SimpleSample
    {
        [Background]
        public void Background()
        {
            "some background".x(() => { });
        }

        [Scenario]
        public void SimpleScenario()
        {
            "a value exists".x(() => { });

            "when the value is changed".x(() => { });

            "the value should be changed".x(() => { });
        }

        [Scenario]
        public void AnotherSimpleScenario(
            string value)
        {
            "another value exists".x(() => { });

            "when the other value is changed".x(() => { });

            "the other value should be changed".x(() => { });

            "the other value should really be changed".x(() => { });
        }
    }
}
```
