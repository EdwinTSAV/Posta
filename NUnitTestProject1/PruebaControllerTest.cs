
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sistema_TESIS.Controllers;
using Sistema_TESIS.Models;
using Sistema_TESIS.Repositories;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    class PruebaControllerTest
    {
        [Test]
        public void MostrarCuadroClinicoCorrectamente()
        {
            var pruebasRepository = new Mock<IPruebasRepository>();
            pruebasRepository.Setup(o => o.PruebasByHistoriaClinicaId(It.IsAny<int>()))
                .Returns(new List<Prueba>());
            pruebasRepository.Setup(o => o.Count(It.IsAny<List<Prueba>>()))
                .Returns(It.IsAny<int>());

            var repositoryH = new Mock<IHistoriaClinicaRepository>();
            var repositoryP = new Mock<IPersonaRepository>();
            var repositoryS = new Mock<ISeguimientoRepostory>();

            var controller = new PruebaController
                (pruebasRepository.Object, repositoryH.Object, repositoryP.Object, repositoryS.Object);

            var view = controller.Index(It.IsAny<int>());
            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearUnaPruebaCorrectamente()
        {
            var pruebasRepository = new Mock<IPruebasRepository>();

            var repositoryH = new Mock<IHistoriaClinicaRepository>();
            repositoryH.Setup(o => o.FindHistoriaClinicaById(It.IsAny<int>()))
                .Returns(new HistoriaClinica());
            var repositoryP = new Mock<IPersonaRepository>();
            repositoryP.Setup(o => o.FindPersonaById(It.IsAny<int>()))
                .Returns(new Persona());
            var repositoryS = new Mock<ISeguimientoRepostory>();
            var controller = new PruebaController
                (pruebasRepository.Object, repositoryH.Object, repositoryP.Object, repositoryS.Object);

            var view = controller.Create(new Prueba());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void VerPruebasCorrectamente()
        {
            var pruebasRepository = new Mock<IPruebasRepository>();
            pruebasRepository.Setup(o => o.PruebaById(It.IsAny<int>())).Returns(new Prueba());
            var repositoryH = new Mock<IHistoriaClinicaRepository>();
            var repositoryP = new Mock<IPersonaRepository>();
            var repositoryS = new Mock<ISeguimientoRepostory>();

            var controller = new PruebaController
                (pruebasRepository.Object, repositoryH.Object, repositoryP.Object, repositoryS.Object);

            var view = controller.Ver(It.IsAny<int>());
            Assert.IsInstanceOf<ViewResult>(view);
        }
    }
}
