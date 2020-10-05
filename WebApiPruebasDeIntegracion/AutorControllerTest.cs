using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PrimerWebApiM3;
using PrimerWebApiM3.Entities;
using PrimerWebApiM3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPruebasDeIntegracion.Mocks;

namespace WebApiPruebasDeIntegracion
{
    [TestClass]
    public class AutorControllerTest
    {
        private WebApplicationFactory<Startup> _factory; 

        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

       public WebApplicationFactory<Startup> ConstruirWebhostBuilder()
        {
            return _factory.WithWebHostBuilder(builder =>
            {
               builder.ConfigureTestServices(services =>
               {
                   services.AddScoped<IAutoresRepository, AutoresRepositoryMock>();
               });

            });
       }

        [TestMethod]
        public async Task GetAutorById_SiNoExisteRetorna404()
        {
            var client = ConstruirWebhostBuilder().CreateClient();
            var url = "/api/autores/0";

            var response = await client.GetAsync(url);

            Assert.AreEqual(404, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task GetAutorById_RetornaUnAutorValido()
        {
            var client = ConstruirWebhostBuilder().CreateClient();
            var url = "/api/autores/1";

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Código de status no exitoso: " + response.StatusCode);
            }

            var result = JsonConvert.DeserializeObject<Autor>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Julio Verne", result.Nombre);
        }

    }
}
