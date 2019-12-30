using Xbehave;

namespace Sample.Nested
{
    public class NestedSample
    {
        [Scenario]
        public void SimpleScenario(
            string value)
        {
            "a value exists".x(() =>
                value = "hello world");

            "when the value is changed".x(() =>
                value = "HELLO WORLD");

            "the value should be changed".x(() => { });
        }
    }
}