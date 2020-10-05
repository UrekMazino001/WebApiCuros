using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PrimerWebApiM3;
using System.Threading.Tasks;

namespace WebApiPruebasDeIntegracion
{
    [TestClass]
    public class ValuesControllerTest
    {

        private  WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task Get_ReturnTwoElements()
        {
            //Creamos un client ya que el endPoint solo responde a clientes.
            var client = _factory.CreateClient();
            var url = "/api/values";

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Código de status no exitoso: " + response.StatusCode);
            }

            var result = JsonConvert.DeserializeObject<string[]>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(2, result.Length);


        }
    }
}
