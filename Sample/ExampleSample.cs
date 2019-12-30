using Xbehave;

namespace Sample
{
    public class ExampleSample
    {
        [Scenario]
        [Example(
            "foo",
            42)]
        [Example("bar(blah)", 17)]
        [Example(@"multi
line",
            33)]
        public void WithExample(
            string s,
            int i,
            string value)
        {
            "an example value exists".x(() =>
                value = "hello world");

            "when the example value is changed".x(() =>
                value = "HELLO WORLD");

            "the example value should be changed".x(() => { });
        }
    }
}