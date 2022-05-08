
using Moq;
using NUnit.Framework;
using Sistema_TESIS.Controllers;
using Sistema_TESIS.Models;
using Sistema_TESIS.Repositories;
using System.Collections.Generic;
using System.Linq;


namespace PruebasUnitarias
{
    public class HomeControllerTest
    {

        [Test]
        public void ObteniendoCoordenadasCorrectamente()
        {
            var repositoryP = new Mock<IPersonaRepository>();
            repositoryP.Setup(o => o.ListaPersonasConUbicacion()).Returns(new List<Persona>());

            var repositoryD = new Mock<IDistritoRepository>();

            var personaController = new HomeController(repositoryP.Object, repositoryD.Object);

            var cords = personaController.Cords();

            var pruebaString = System.Text.Json.JsonSerializer.Serialize(new List<Persona>());

            Assert.AreEqual(pruebaString,cords);
        }

        [Test]
        public void ObteniendoDatosFiltrados()
        {
            var repositoryP = new Mock<IPersonaRepository>();
            repositoryP.Setup(o => o.PintarUbicacion(It.IsAny<string>())).Returns(new List<Persona>().AsQueryable);

            var repositoryD = new Mock<IDistritoRepository>();

            var personaController = new HomeController(repositoryP.Object, repositoryD.Object);

            var pintar = personaController.Pintar(It.IsAny<string>());

            var pruebaString = System.Text.Json.JsonSerializer.Serialize(new List<Persona>());

            Assert.AreEqual(pruebaString, pintar);
        }

    }
}