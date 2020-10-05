using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrimerWebApiM3.Controllers;
using PrimerWebApiM3.Entities;
using PrimerWebApiM3.Services;

namespace WebApiPruebasUnitarias
{
    [TestClass]
    public class AutorControllerTest
    {
        [TestMethod]
        public void GetIfAutorIsNull()
        {
            //Preparación
            var autorId = 1;

            //Moqueamos la dependencia de posee el controlador (IAutoresRepository)
            var mock = new Mock<IAutoresRepository>();
            mock.Setup(x => x.GetAutorById(autorId)).Returns(default(Autor));
            var autorController = new AutorController(mock.Object);


            //Prueba
            var resultado = autorController.GetAutorById(autorId);

            //Verificación
            Assert.IsInstanceOfType(resultado.Result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void GetValidAutor()
        {
            //Preparación

            var autorMock = new Autor()
            {
                Id = 1,
                Nombre = "Julio verne",
                Libros = null
            };
            var mock = new Mock<IAutoresRepository>();
            mock.Setup(x => x.GetAutorById(autorMock.Id)).Returns(autorMock);
            var autorController = new AutorController(mock.Object);


            //Prueba
            var resultado = autorController.GetAutorById(autorMock.Id);

            //Verificación
            Assert.IsNotNull(resultado.Value);
            Assert.AreEqual(resultado.Value.Id, autorMock.Id);
            Assert.AreEqual(resultado.Value.Nombre, autorMock.Nombre);
            Assert.AreEqual(resultado.Value.Libros, autorMock.Libros);


        }
    }
}
