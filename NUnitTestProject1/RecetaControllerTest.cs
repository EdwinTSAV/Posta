
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sistema_TESIS.Controllers;
using Sistema_TESIS.Models;
using Sistema_TESIS.Repositories;
using System.Collections.Generic;


namespace PruebasUnitarias
{
    class RecetaControllerTest
    {
        [Test]
        public void MostrarCuadroClinicoCorrectamente()
        {
            var repositoryR = new Mock<IRecetaRepository>();
            repositoryR.Setup(o => o.GetRecetasByCuadroClinico(It.IsAny<int>()))
                .Returns(new List<Receta>());

            var controller = new RecetaController(repositoryR.Object);
            var view = controller.Index(It.IsAny<int>());

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearUnaRecetaCorrectamente()
        {
            var repositoryR = new Mock<IRecetaRepository>();
             var controller = new RecetaController(repositoryR.Object);

            var view = controller.Create(new Receta());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void ActualizarUnaRecetaCorrectamente()
        {
            var repositoryR = new Mock<IRecetaRepository>();
            var controller = new RecetaController(repositoryR.Object);

            var view = controller.Update(new Receta());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void EliminarUnaRecetaCorrectamente()
        {
            var repositoryR = new Mock<IRecetaRepository>();
            repositoryR.Setup(o => o.EliminarReceta(It.IsAny<int>())).Returns(It.IsAny<int>());

            var controller = new RecetaController(repositoryR.Object);

            var view = controller.Delete(It.IsAny<int>());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
    }
}
