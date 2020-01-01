module Tests

open FsUnit
open Xunit
open xBehaveReportGenerator.Parsing
open xBehaveReportGenerator.Structure
open xBehaveReportGenerator.Formatting

let Trx = """<?xml version="1.0" encoding="utf-8"?>
<TestRun id="3e4a7e5a-6ff0-4159-8501-01bc2d193c9d" name="ursen@ABADDON 2019-12-29 12:09:42" runUser="ABADDON\ursen" xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010">
  <Times creation="2019-12-29T12:09:42.9702545+01:00" queuing="2019-12-29T12:09:42.9702568+01:00" start="2019-12-29T12:09:42.0174229+01:00" finish="2019-12-29T12:09:43.0083045+01:00" />
  <TestSettings name="default" id="bf6b8627-311b-4b87-8de4-3dcbab75da18">
    <Deployment runDeploymentRoot="ursen_ABADDON_2019-12-29_12_09_42" />
  </TestSettings>
  <Results>
    <UnitTestResult executionId="7ab1bf5c-15e7-4018-9765-3cebb7b2dc48" testId="e66a06bd-c031-5863-c7e2-e46519d621ef" testName="Sample.SimpleSample.SimpleScenario() [02] a value exists" computerName="ABADDON" duration="00:00:00.0000449" startTime="2019-12-29T12:09:42.9047288+01:00" endTime="2019-12-29T12:09:42.9047292+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="7ab1bf5c-15e7-4018-9765-3cebb7b2dc48" />
    <UnitTestResult executionId="b158695a-cdbd-440b-9659-29e88a93c178" testId="f65bd867-1d3d-3777-7915-5ab0cd873988" testName="Sample.ExampleSample.WithExample(s: &quot;foo&quot;, i: 42) [02] when the example value is changed" computerName="ABADDON" duration="00:00:00.0000621" startTime="2019-12-29T12:09:42.9048701+01:00" endTime="2019-12-29T12:09:42.9048702+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="b158695a-cdbd-440b-9659-29e88a93c178" />
    <UnitTestResult executionId="5c853a2d-3326-49c2-bd12-78ed077ed56a" testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" testName="Sample.SimpleSample.AnotherSimpleScenario() [04] the other value should be changed" computerName="ABADDON" duration="00:00:00.0000631" startTime="2019-12-29T12:09:42.9355694+01:00" endTime="2019-12-29T12:09:42.9355695+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="5c853a2d-3326-49c2-bd12-78ed077ed56a" />
    <UnitTestResult executionId="418a17dc-4788-4183-b48b-1ced76112ac2" testId="c8572ea5-a60b-d561-4ff6-2a68393e5480" testName="Sample.Nested.NestedSample.SimpleScenario() [03] the value should be changed" computerName="ABADDON" duration="00:00:00.0000280" startTime="2019-12-29T12:09:42.9049001+01:00" endTime="2019-12-29T12:09:42.9049002+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="418a17dc-4788-4183-b48b-1ced76112ac2" />
    <UnitTestResult executionId="f51d3b33-be00-410c-a9e2-0e1dcc6dec45" testId="f65bd867-1d3d-3777-7915-5ab0cd873988" testName="Sample.ExampleSample.WithExample(s: &quot;bar&quot;, i: 17) [03] the example value should be changed" computerName="ABADDON" duration="00:00:00.0000041" startTime="2019-12-29T12:09:42.9348058+01:00" endTime="2019-12-29T12:09:42.9348059+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="f51d3b33-be00-410c-a9e2-0e1dcc6dec45" />
    <UnitTestResult executionId="1c42b096-d203-4e1f-b512-c11a040ad28b" testId="f65bd867-1d3d-3777-7915-5ab0cd873988" testName="Sample.ExampleSample.WithExample(s: &quot;bar&quot;, i: 17) [01] an example value exists" computerName="ABADDON" duration="00:00:00.0000055" startTime="2019-12-29T12:09:42.9347778+01:00" endTime="2019-12-29T12:09:42.9347779+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="1c42b096-d203-4e1f-b512-c11a040ad28b" />
    <UnitTestResult executionId="c3b16085-4a98-4206-ac1d-d86bd1c0577d" testId="f65bd867-1d3d-3777-7915-5ab0cd873988" testName="Sample.ExampleSample.WithExample(s: &quot;foo&quot;, i: 42) [03] the example value should be changed" computerName="ABADDON" duration="00:00:00.0000356" startTime="2019-12-29T12:09:42.9347475+01:00" endTime="2019-12-29T12:09:42.9347476+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="c3b16085-4a98-4206-ac1d-d86bd1c0577d" />
    <UnitTestResult executionId="8738e4a3-dea0-40b0-9f54-fa5ce838936e" testId="f65bd867-1d3d-3777-7915-5ab0cd873988" testName="Sample.ExampleSample.WithExample(s: &quot;foo&quot;, i: 42) [01] an example value exists" computerName="ABADDON" duration="00:00:00.0010768" startTime="2019-12-29T12:09:42.9046552+01:00" endTime="2019-12-29T12:09:42.9046553+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="8738e4a3-dea0-40b0-9f54-fa5ce838936e" />
    <UnitTestResult executionId="0bfa9776-df9e-49dd-b9ed-bfd0f11d1647" testId="9b5e8a61-d6ea-cddf-5858-da293dadc801" testName="Sample.Nested.OtherNestedSample.SimpleScenario() [03] the other value should be changed" computerName="ABADDON" duration="00:00:00.0000312" startTime="2019-12-29T12:09:42.9347284+01:00" endTime="2019-12-29T12:09:42.9347290+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="0bfa9776-df9e-49dd-b9ed-bfd0f11d1647" />
    <UnitTestResult executionId="0af2c23e-6a02-4bcb-bb93-dead0aaa2d06" testId="e66a06bd-c031-5863-c7e2-e46519d621ef" testName="Sample.SimpleSample.SimpleScenario() [03] when the value is changed" computerName="ABADDON" duration="00:00:00.0000367" startTime="2019-12-29T12:09:42.9049121+01:00" endTime="2019-12-29T12:09:42.9049123+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="0af2c23e-6a02-4bcb-bb93-dead0aaa2d06" />
    <UnitTestResult executionId="c8e77303-55b9-4420-b805-82414e4b6e64" testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" testName="Sample.SimpleSample.AnotherSimpleScenario() [03] when the other value is changed" computerName="ABADDON" duration="00:00:00.0000632" startTime="2019-12-29T12:09:42.9355547+01:00" endTime="2019-12-29T12:09:42.9355548+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="c8e77303-55b9-4420-b805-82414e4b6e64" />
    <UnitTestResult executionId="fce14c4b-7f41-42c0-81ba-7dac008df6d5" testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" testName="Sample.SimpleSample.AnotherSimpleScenario() [01] (Background) some background" computerName="ABADDON" duration="00:00:00.0000041" startTime="2019-12-29T12:09:42.9355186+01:00" endTime="2019-12-29T12:09:42.9355189+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="fce14c4b-7f41-42c0-81ba-7dac008df6d5" />
    <UnitTestResult executionId="7e085fea-b537-470c-9a8a-fbf92e17fe97" testId="c8572ea5-a60b-d561-4ff6-2a68393e5480" testName="Sample.Nested.NestedSample.SimpleScenario() [01] a value exists" computerName="ABADDON" duration="00:00:00.0011209" startTime="2019-12-29T12:09:42.9045925+01:00" endTime="2019-12-29T12:09:42.9046076+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="7e085fea-b537-470c-9a8a-fbf92e17fe97" />
    <UnitTestResult executionId="a9bea9ab-7d33-4a73-81cc-03549902eb45" testId="f65bd867-1d3d-3777-7915-5ab0cd873988" testName="Sample.ExampleSample.WithExample(s: &quot;bar&quot;, i: 17) [02] when the example value is changed" computerName="ABADDON" duration="00:00:00.0000037" startTime="2019-12-29T12:09:42.9347917+01:00" endTime="2019-12-29T12:09:42.9347918+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="a9bea9ab-7d33-4a73-81cc-03549902eb45" />
    <UnitTestResult executionId="4ddc0d06-43db-4a69-a49e-d3ae95e066e2" testId="9b5e8a61-d6ea-cddf-5858-da293dadc801" testName="Sample.Nested.OtherNestedSample.SimpleScenario() [01] another value exists" computerName="ABADDON" duration="00:00:00.0010348" startTime="2019-12-29T12:09:42.9013439+01:00" endTime="2019-12-29T12:09:42.9014015+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="4ddc0d06-43db-4a69-a49e-d3ae95e066e2" />
    <UnitTestResult executionId="9a712b5e-1060-48c7-9b87-45cf9d1a056f" testId="e66a06bd-c031-5863-c7e2-e46519d621ef" testName="Sample.SimpleSample.SimpleScenario() [04] the value should be changed" computerName="ABADDON" duration="00:00:00.0000260" startTime="2019-12-29T12:09:42.9347632+01:00" endTime="2019-12-29T12:09:42.9347633+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="9a712b5e-1060-48c7-9b87-45cf9d1a056f" />
    <UnitTestResult executionId="5d984734-1812-4edc-a870-6388b70cb13b" testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" testName="Sample.SimpleSample.AnotherSimpleScenario() [05] the other value should really be changed" computerName="ABADDON" duration="00:00:00.0000485" startTime="2019-12-29T12:09:42.9360054+01:00" endTime="2019-12-29T12:09:42.9360058+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="5d984734-1812-4edc-a870-6388b70cb13b" />
    <UnitTestResult executionId="24553790-eaa2-451b-8dd1-f11a9625dbfc" testId="c8572ea5-a60b-d561-4ff6-2a68393e5480" testName="Sample.Nested.NestedSample.SimpleScenario() [02] when the value is changed" computerName="ABADDON" duration="00:00:00.0000452" startTime="2019-12-29T12:09:42.9048457+01:00" endTime="2019-12-29T12:09:42.9048460+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="24553790-eaa2-451b-8dd1-f11a9625dbfc" />
    <UnitTestResult executionId="dc64655f-7346-4f0d-b966-551757ceaacf" testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" testName="Sample.SimpleSample.AnotherSimpleScenario() [02] another value exists" computerName="ABADDON" duration="00:00:00.0000671" startTime="2019-12-29T12:09:42.9355391+01:00" endTime="2019-12-29T12:09:42.9355392+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="dc64655f-7346-4f0d-b966-551757ceaacf" />
    <UnitTestResult executionId="c873ffbd-aeb5-4526-a3bc-5fd190a15775" testId="9b5e8a61-d6ea-cddf-5858-da293dadc801" testName="Sample.Nested.OtherNestedSample.SimpleScenario() [02] when the other value is changed" computerName="ABADDON" duration="00:00:00.0000678" startTime="2019-12-29T12:09:42.9048790+01:00" endTime="2019-12-29T12:09:42.9048791+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="c873ffbd-aeb5-4526-a3bc-5fd190a15775" />
    <UnitTestResult executionId="0d20729e-f05b-4989-9aa9-d37a661cda6f" testId="e66a06bd-c031-5863-c7e2-e46519d621ef" testName="Sample.SimpleSample.SimpleScenario() [01] (Background) some background" computerName="ABADDON" duration="00:00:00.0011076" startTime="2019-12-29T12:09:42.9046669+01:00" endTime="2019-12-29T12:09:42.9046670+01:00" testType="13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b" outcome="Passed" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" relativeResultsDirectory="0d20729e-f05b-4989-9aa9-d37a661cda6f" />
  </Results>
  <TestDefinitions>
    <UnitTest name="Sample.SimpleSample.SimpleScenario" storage="c:\projects\xbehavereportgenerator\sample\bin\debug\netcoreapp3.0\sample.dll" id="e66a06bd-c031-5863-c7e2-e46519d621ef">
      <Execution id="0d20729e-f05b-4989-9aa9-d37a661cda6f" />
      <TestMethod codeBase="C:\Projects\xBehaveReportGenerator\Sample\bin\Debug\netcoreapp3.0\Sample.dll" adapterTypeName="executor://xunit/VsTestRunner2/netcoreapp" className="Sample.SimpleSample" name="Sample.SimpleSample.SimpleScenario" />
    </UnitTest>
    <UnitTest name="Sample.SimpleSample.AnotherSimpleScenario" storage="c:\projects\xbehavereportgenerator\sample\bin\debug\netcoreapp3.0\sample.dll" id="160f8564-7f15-9f4a-8a1d-e77f351f752f">
      <Execution id="fce14c4b-7f41-42c0-81ba-7dac008df6d5" />
      <TestMethod codeBase="C:\Projects\xBehaveReportGenerator\Sample\bin\Debug\netcoreapp3.0\Sample.dll" adapterTypeName="executor://xunit/VsTestRunner2/netcoreapp" className="Sample.SimpleSample" name="Sample.SimpleSample.AnotherSimpleScenario" />
    </UnitTest>
    <UnitTest name="Sample.Nested.NestedSample.SimpleScenario" storage="c:\projects\xbehavereportgenerator\sample\bin\debug\netcoreapp3.0\sample.dll" id="c8572ea5-a60b-d561-4ff6-2a68393e5480">
      <Execution id="7e085fea-b537-470c-9a8a-fbf92e17fe97" />
      <TestMethod codeBase="C:\Projects\xBehaveReportGenerator\Sample\bin\Debug\netcoreapp3.0\Sample.dll" adapterTypeName="executor://xunit/VsTestRunner2/netcoreapp" className="Sample.Nested.NestedSample" name="Sample.Nested.NestedSample.SimpleScenario" />
    </UnitTest>
    <UnitTest name="Sample.ExampleSample.WithExample" storage="c:\projects\xbehavereportgenerator\sample\bin\debug\netcoreapp3.0\sample.dll" id="f65bd867-1d3d-3777-7915-5ab0cd873988">
      <Execution id="8738e4a3-dea0-40b0-9f54-fa5ce838936e" />
      <TestMethod codeBase="C:\Projects\xBehaveReportGenerator\Sample\bin\Debug\netcoreapp3.0\Sample.dll" adapterTypeName="executor://xunit/VsTestRunner2/netcoreapp" className="Sample.ExampleSample" name="Sample.ExampleSample.WithExample" />
    </UnitTest>
    <UnitTest name="Sample.Nested.OtherNestedSample.SimpleScenario" storage="c:\projects\xbehavereportgenerator\sample\bin\debug\netcoreapp3.0\sample.dll" id="9b5e8a61-d6ea-cddf-5858-da293dadc801">
      <Execution id="4ddc0d06-43db-4a69-a49e-d3ae95e066e2" />
      <TestMethod codeBase="C:\Projects\xBehaveReportGenerator\Sample\bin\Debug\netcoreapp3.0\Sample.dll" adapterTypeName="executor://xunit/VsTestRunner2/netcoreapp" className="Sample.Nested.OtherNestedSample" name="Sample.Nested.OtherNestedSample.SimpleScenario" />
    </UnitTest>
  </TestDefinitions>
  <TestEntries>
    <TestEntry testId="e66a06bd-c031-5863-c7e2-e46519d621ef" executionId="7ab1bf5c-15e7-4018-9765-3cebb7b2dc48" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="f65bd867-1d3d-3777-7915-5ab0cd873988" executionId="b158695a-cdbd-440b-9659-29e88a93c178" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" executionId="5c853a2d-3326-49c2-bd12-78ed077ed56a" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="c8572ea5-a60b-d561-4ff6-2a68393e5480" executionId="418a17dc-4788-4183-b48b-1ced76112ac2" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="f65bd867-1d3d-3777-7915-5ab0cd873988" executionId="f51d3b33-be00-410c-a9e2-0e1dcc6dec45" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="f65bd867-1d3d-3777-7915-5ab0cd873988" executionId="1c42b096-d203-4e1f-b512-c11a040ad28b" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="f65bd867-1d3d-3777-7915-5ab0cd873988" executionId="c3b16085-4a98-4206-ac1d-d86bd1c0577d" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="f65bd867-1d3d-3777-7915-5ab0cd873988" executionId="8738e4a3-dea0-40b0-9f54-fa5ce838936e" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="9b5e8a61-d6ea-cddf-5858-da293dadc801" executionId="0bfa9776-df9e-49dd-b9ed-bfd0f11d1647" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="e66a06bd-c031-5863-c7e2-e46519d621ef" executionId="0af2c23e-6a02-4bcb-bb93-dead0aaa2d06" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" executionId="c8e77303-55b9-4420-b805-82414e4b6e64" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" executionId="fce14c4b-7f41-42c0-81ba-7dac008df6d5" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="c8572ea5-a60b-d561-4ff6-2a68393e5480" executionId="7e085fea-b537-470c-9a8a-fbf92e17fe97" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="f65bd867-1d3d-3777-7915-5ab0cd873988" executionId="a9bea9ab-7d33-4a73-81cc-03549902eb45" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="9b5e8a61-d6ea-cddf-5858-da293dadc801" executionId="4ddc0d06-43db-4a69-a49e-d3ae95e066e2" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="e66a06bd-c031-5863-c7e2-e46519d621ef" executionId="9a712b5e-1060-48c7-9b87-45cf9d1a056f" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" executionId="5d984734-1812-4edc-a870-6388b70cb13b" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="c8572ea5-a60b-d561-4ff6-2a68393e5480" executionId="24553790-eaa2-451b-8dd1-f11a9625dbfc" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="160f8564-7f15-9f4a-8a1d-e77f351f752f" executionId="dc64655f-7346-4f0d-b966-551757ceaacf" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="9b5e8a61-d6ea-cddf-5858-da293dadc801" executionId="c873ffbd-aeb5-4526-a3bc-5fd190a15775" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestEntry testId="e66a06bd-c031-5863-c7e2-e46519d621ef" executionId="0d20729e-f05b-4989-9aa9-d37a661cda6f" testListId="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
  </TestEntries>
  <TestLists>
    <TestList name="Results Not in a List" id="8c84fa94-04c1-424b-9868-57a2d4851a1d" />
    <TestList name="All Loaded Results" id="19431567-8539-422a-85d7-44ee4e166bda" />
  </TestLists>
  <ResultSummary outcome="Completed">
    <Counters total="21" executed="21" passed="21" failed="0" error="0" timeout="0" aborted="0" inconclusive="0" passedButRunAborted="0" notRunnable="0" notExecuted="0" disconnected="0" warning="0" completed="0" inProgress="0" pending="0" />
    <Output>
      <StdOut>[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v2.4.1 (64-bit .NET Core 3.0.0)&#xD;
[xUnit.net 00:00:00.30]   Discovering: Sample&#xD;
[xUnit.net 00:00:00.33]   Discovered:  Sample&#xD;
[xUnit.net 00:00:00.33]   Starting:    Sample&#xD;
[xUnit.net 00:00:00.44]   Finished:    Sample&#xD;
</StdOut>
    </Output>
  </ResultSummary>
</TestRun>"""

let title = "some title"

let Report = """
#  some title Sample
## ExampleSample
### WithExample
*s: "bar", i: 17*
01. an example value exists
02. when the example value is changed
03. the example value should be changed

*s: "foo", i: 42*
01. an example value exists
02. when the example value is changed
03. the example value should be changed

## Nested
### NestedSample
#### SimpleScenario
01. a value exists
02. when the value is changed
03. the value should be changed

### OtherNestedSample
#### SimpleScenario
01. another value exists
02. when the other value is changed
03. the other value should be changed

## SimpleSample
### AnotherSimpleScenario
01. (Background) some background
02. another value exists
03. when the other value is changed
04. the other value should be changed
05. the other value should really be changed

### SimpleScenario
01. (Background) some background
02. a value exists
03. when the value is changed
04. the value should be changed
"""

[<Fact>]
let ``My test`` () =
    let data = xBehaveReportGenerator.TypeProviders.Data.Parse(Trx)
    let infos = parseData data
    let tree = structurize title infos
    let compactedTree = compact tree ""
    let report = format 1 compactedTree
    let text = report |> Seq.fold (fun a v -> a + "\r\n" + v) ""

    text |> should equal Report



