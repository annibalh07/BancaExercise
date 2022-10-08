using BpInterface.Core.Extensions;
using BpInterface.Core.Models.Dto;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace BpInterface.WebApi.IntegrationTests.ControllersTests
{
    public class ClienteControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public ClienteControllerTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();


        [Fact]
        public async Task GetClientes_WhenCalled_ReturnsOneClient()
        {
            //Arrange

            //Act
            var response = await _client.GetAsync("/api/clientes");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var clientes = responseString.FromJson<List<ClienteResponse>>();

            //Assert
            Assert.NotNull(clientes);
            Assert.Contains("Leonardo", responseString);
            Assert.True(clientes.Count == 1);
        }

        [Fact]
        public async Task CreateCliente_WhenCalled_ReturnsOneClient()
        {
            //Arrange
            var request = new ClienteRequestForCreate
            {
                Contrasenia = "123",
                Direccion = "Av. la prensa",
                Edad = 19,
                Genero = "Masculino",
                Identificacion = "1206121624",
                Nombres = "Teodoro Salazar",
                Telefono = "0997201316"
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            
            //Act
            var response = await _client.PostAsync("/api/clientes", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var clientes = responseString.FromJson<ClienteResponse>();

            //Assert
            Assert.NotNull(clientes);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Contains("Teodoro", responseString);
        }

        [Fact]
        public async Task CreateCliente_WhenClientIsYounger_ReturnsBadRequest()
        {
            //Arrange
            var request = new ClienteRequestForCreate
            {
                Contrasenia = "123",
                Direccion = "Av. la prensa",
                Edad = 15,
                Genero = "Masculino",
                Identificacion = "1206121624",
                Nombres = "Teodoro Salazar",
                Telefono = "0997201316"
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            //Act
            var response = await _client.PostAsync("/api/clientes", jsonContent);
            
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
