using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace echoserver.tests
{
    public class Test : IDisposable
    {
        private readonly TestServer testServer;
        public HttpClient Client { get; }

        public Test()
        {
            var builder = echoserver.Program.CreateWebHostBuilder(Array.Empty<String>());

            testServer = new TestServer(builder);
            Client = testServer.CreateClient();
        }

        [Fact]
        public async Task Get()
        {
            var response = await Client.GetAsync("/");

            response.EnsureSuccessStatusCode();

            var responseStrong = await response.Content.ReadAsStringAsync();

            Assert.Equal(string.Empty, responseStrong);
        }

        [Fact]
        public async Task Get_With_Custom_Header()
        {
            string headerName = "Custom-Header";
            Client.DefaultRequestHeaders.Add(headerName, "echo");
            var response = await Client.GetAsync("/");

            response.EnsureSuccessStatusCode();

            var responseStrong = await response.Content.ReadAsStringAsync();

            Assert.Equal(string.Empty, responseStrong);
            Assert.Equal("echo", response.Headers.GetValues(headerName).First());
        }

        public void Dispose()
        {
            Client.Dispose();
            testServer.Dispose();
        }
    }
}
