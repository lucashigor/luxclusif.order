using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet.Verifier;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace luxclusif.order.tests.Components.Utils
{
    public class PactsGenericValidator : IDisposable
    {
        public PactsGenericValidator()
        {
        }

        public void Dispose()
        {
            // make sure you dispose the verifier to stop the internal messaging server
            GC.SuppressFinalize(this);
        }

        public static void EnsureEventApiHonoursPactWithConsumer(string microServiceName, string eventName, string fileName, object eventSended)
        {
            var verifier = new PactVerifier();

            string pactPath = Path.Combine("..",
                                           "..",
                                           "..",
                                           "..",
                                           "luxclusif.order.tests",
                                           "Components",
                                           "pacts",
                                           fileName);

            var defaultSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            verifier
                .MessagingProvider(microServiceName, defaultSettings)
                .WithProviderMessages(scenarios =>
                {

                    scenarios.Add(eventName, builder =>
                     {
                         builder
                                .WithContent(() => new[]
                                {
                                    eventSended
                                });
                     });
                })
                .WithFileSource(new FileInfo(pactPath))
                .Verify();

            verifier.Dispose();
        }
    }
}
