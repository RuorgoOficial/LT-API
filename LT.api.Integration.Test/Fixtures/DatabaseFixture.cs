using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Xunit;

namespace LT.api.Integration.Test.Fixtures
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private IContainer? _container;
        public string ConnectionString { 
            get
            {
                return "Server=localhost;InitialCatalog=master;Persist Security Info=true;User ID=sa;Password=yourStrong(!)Password;Trust Server Certificate=true";
            }
        }

        public async Task InitializeAsync()
        {
            _container = new ContainerBuilder()
                // Set the image for the container to "testcontainers/helloworld:1.1.0".
                .WithImage("testcontainers/helloworld:1.1.0")
                // Bind port 8080 of the container to a random port on the host.
                .WithPortBinding(8080, true)
                // Wait until the HTTP endpoint of the container is available.
                .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(8080)))
                // Build the container configuration.
                .Build();

            await Task.Delay(1000);
        }

        public Task DisposeAsync()
        {
            throw new NotImplementedException();
        }


    }
}
