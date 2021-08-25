using FluentAssertions;
using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private static HttpClient http = new HttpClient();

        [TestMethod]
        public async Task Given_OpenApiDocumentUrl_When_Url_Invoked_Then_It_Should_Return_Title()
        {
            var requestUri = "http://localhost:7071/api/openapi/v3.json";
            var response = await http.GetStringAsync(requestUri).ConfigureAwait(false);

            var doc = JsonConvert.DeserializeObject<OpenApiDocument>(response);

            doc.Info.Title.Should().Be("Azure Functions OpenAPI Extension");

            var schemas = doc.Components.Schemas;
            schemas.Should().ContainKey("greeting");
        }

        [DataTestMethod]
        [DataRow("greeting")]
        public async Task Given_OpenApiDocumentUrl_When_Url_Invoked_Then_It_Should_Return_SchemaKey(string schemaKey)
        {
            var requestUri = "http://localhost:7071/api/openapi/v3.json";
            var response = await http.GetStringAsync(requestUri).ConfigureAwait(false);

            var doc = JsonConvert.DeserializeObject<OpenApiDocument>(response);

            var schemas = doc.Components.Schemas;
            schemas.Should().ContainKey(schemaKey);
        }

        [DataTestMethod]
        [DataRow("greeting", "object")]
        public async Task Given_OpenApiDocumentUrl_When_Url_Invoked_Then_It_Should_Return_SchemaDataType(string schemaKey, string dataType)
        {
            var requestUri = "http://localhost:7071/api/openapi/v3.json";
            var response = await http.GetStringAsync(requestUri).ConfigureAwait(false);

            var doc = JsonConvert.DeserializeObject<OpenApiDocument>(response);

            var schemas = doc.Components.Schemas[schemaKey];
            schemas.Type.Should().Be(dataType);
        }

        [DataTestMethod]
        [DataRow("greeting", "message")]
        public async Task Given_OpenApiDocumentUrl_When_Url_Invoked_Then_It_Should_Return_PropertyKey(string schemaKey, string propertyKey)
        {
            var requestUri = "http://localhost:7071/api/openapi/v3.json";
            var response = await http.GetStringAsync(requestUri).ConfigureAwait(false);

            var doc = JsonConvert.DeserializeObject<OpenApiDocument>(response);

            var schemas = doc.Components.Schemas[schemaKey];
            schemas.Properties.Should().ContainKey(propertyKey);
        }

        [DataTestMethod]
        [DataRow("greeting", "message", "string")]
        public async Task Given_OpenApiDocumentUrl_When_Url_Invoked_Then_It_Should_Return_PropertyDataType(string schemaKey, string propertyKey, string dataType)
        {
            //Given / arrange
            var requestUri = "http://localhost:7071/api/openapi/v3.json";
            var response = await http.GetStringAsync(requestUri).ConfigureAwait(false);

            //When / Act
            var doc = JsonConvert.DeserializeObject<OpenApiDocument>(response);

            var schemas = doc.Components.Schemas[schemaKey];
            var properties = schemas.Properties[propertyKey];

            // Then / Assert
            properties.Type.Should().Be(dataType);
        }

    }
}
