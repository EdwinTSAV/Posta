using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sistema_TESIS.Controllers;
using Sistema_TESIS.Models;
using Sistema_TESIS.Repositories;

namespace PruebasUnitarias
{
    class SeguimientoControllerTest
    {
        [Test]
        public void MostrarSeguimientoCorrectamente()
        {
            var repositoryS = new Mock<ISeguimientoRepostory>();
            var repositoryD = new Mock<IDistritoRepository>();
            var controller = new SeguimientoController(repositoryS.Object, repositoryD.Object);
            var view = controller.Index(It.IsAny<int>());

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearContactoCorrectamente()
        {
            var repositoryS = new Mock<ISeguimientoRepostory>();
            var repositoryD = new Mock<IDistritoRepository>();
            var controller = new SeguimientoController(repositoryS.Object, repositoryD.Object);
            repositoryS.Setup(o => o.HistoriaClinicaById(new Seguimiento())).Returns(It.IsAny<int>());
            var view = controller.CreateContacto(new ContactoPaciente(), new Ubicacion(), It.IsAny<int>());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
        [Test]
        public void CrearContactoFallido()
        {
            var repositoryS = new Mock<ISeguimientoRepostory>();
            var repositoryD = new Mock<IDistritoRepository>();
            var controller = new SeguimientoController(repositoryS.Object, repositoryD.Object);
            var view = controller.CreateContacto(null, null, It.IsAny<int>());

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ActualizarContactoExitoso()
        {
            var repositoryS = new Mock<ISeguimientoRepostory>();
            var repositoryD = new Mock<IDistritoRepository>();
            var controller = new SeguimientoController(repositoryS.Object, repositoryD.Object);
            repositoryS.Setup(o => o.HistoriaClinicaById(new Seguimiento())).Returns(It.IsAny<int>());
            var view = controller.UpdateContacto(new ContactoPaciente());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
        [Test]
        public void ActualizarContactoFallido()
        {
            var repositoryS = new Mock<ISeguimientoRepostory>();
            var repositoryD = new Mock<IDistritoRepository>();
            var controller = new SeguimientoController(repositoryS.Object, repositoryD.Object);
            var view = controller.UpdateContacto(null);

            Assert.IsInstanceOf<ViewResult>(view);
        }
    }
}
