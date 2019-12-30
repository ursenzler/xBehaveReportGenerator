using System;
using Xbehave;

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
        public void SimpleScenario(
            string value)
        {
            "a value exists".x(() =>
                value = "hello world");

            "when the value is changed".x(() =>
                value = "HELLO WORLD");

            "the value should be changed".x(() => { });
        }

        [Scenario]
        public void AnotherSimpleScenario(
            string value)
        {
            "another value exists".x(() =>
                value = "hello world");

            "when the other value is changed".x(() =>
                value = "HELLO WORLD");

            "the other value should be changed".x(() => { });

            "the other value should really be changed".x(() => { });
        }
    }
}