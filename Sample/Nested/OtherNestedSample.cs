using Xbehave;

namespace Sample.Nested
{
    public class OtherNestedSample
    {
        [Scenario]
        public void SimpleScenario(
            string value)
        {
            "another value exists".x(() =>
                value = "hello world");

            "when the other value is changed".x(() =>
                value = "HELLO WORLD");

            "the other value should be changed".x(() => { });
        }
    }
}