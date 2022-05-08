
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sistema_TESIS.Controllers;
using Sistema_TESIS.Models;
using Sistema_TESIS.Repositories;
using System.Collections.Generic;


namespace PruebasUnitarias
{
    public class PersonaControllerTest
    {

        [Test]
        public void IndexEncuentraUnaPersonaPorSuNombre()
        {
            var repositoryP = new Mock<IPersonaRepository>();
            var repositoryD = new Mock<IDistritoRepository>();

            var personaController = new PersonaController(repositoryP.Object, repositoryD.Object);

            var view = personaController.Index("Persona", null);

            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        public void IndexEncuentraUnaPersonaPorSuDNI()
        {
            var repositoryP = new Mock<IPersonaRepository>();
            var repositoryD = new Mock<IDistritoRepository>();

            var personaController = new PersonaController(repositoryP.Object, repositoryD.Object);

            var view = personaController.Index(null, "dni");

            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        public void CrearUnaPersonaCorrectamente()
        {
            var repositoryP = new Mock<IPersonaRepository>();
            var repositoryD = new Mock<IDistritoRepository>();
            repositoryP.Setup(o => o.ListaPersonasBD()).Returns(new List<Persona>());

            var personaController = new PersonaController(repositoryP.Object, repositoryD.Object);
            
            var redirectToActionResul = personaController.Create(new Persona(), new Ubicacion());

            Assert.IsInstanceOf<RedirectToActionResult>(redirectToActionResul);
        }

        [Test]
        public void ErrorAlCrearUnaPersona()
        {
            var repositoryP = new Mock<IPersonaRepository>();
            var repositoryD = new Mock<IDistritoRepository>();
            repositoryP.Setup(o => o.ListaPersonasBD()).Returns(new List<Persona>());

            var personaController = new PersonaController(repositoryP.Object, repositoryD.Object);


            var view = personaController.Create(null, null);

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ActualizarPersonaCorrectamente()
        {
            var repositoryP = new Mock<IPersonaRepository>();
            var repositoryD = new Mock<IDistritoRepository>();

            var personaController = new PersonaController(repositoryP.Object, repositoryD.Object);
            var view = personaController.Update(new Persona(), new Ubicacion());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
        [Test]
        public void ActualizarPersonaFallido()
        {
            var repositoryP = new Mock<IPersonaRepository>();
            var repositoryD = new Mock<IDistritoRepository>();

            var personaController = new PersonaController(repositoryP.Object, repositoryD.Object);
            var view = personaController.Update(null, null);

            Assert.IsInstanceOf<ViewResult>(view);
        }
    }
}