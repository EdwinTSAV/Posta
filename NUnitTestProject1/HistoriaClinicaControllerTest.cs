
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sistema_TESIS.Controllers;
using Sistema_TESIS.Repositories;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PruebasUnitarias
{
    class HistoriaClinicaControllerTest
    {
        [Test]
        public void Index()
        {
            var repositoryH = new Mock<IHistoriaClinicaRepository>();

            var controller = new HistoriaClinicaController(repositoryH.Object);

            var view = controller.Index(It.IsAny<int>());

            Assert.IsInstanceOf<ViewResult>(view);
        }
    }
}
