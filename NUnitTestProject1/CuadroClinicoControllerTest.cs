
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sistema_TESIS.Controllers;
using Sistema_TESIS.Models;
using Sistema_TESIS.Repositories;
using Sistema_TESIS.Services;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    class CuadroClinicoControllerTest
    {
        [Test]
        public void MostrarCuadroClinicoCorrectamente()
        {
            var repositoryH = new Mock<IHistoriaClinicaRepository>();
            var repositoryC = new Mock<ICuadrosClinicosRepository>();
            var cookieAuthService = new Mock<ICookieAuthService>();
            var repositoryU = new Mock<IUsuarioRepository>();

            var cuadroClinicoController = new CuadroClinicoController
                (repositoryH.Object, repositoryC.Object, cookieAuthService.Object, repositoryU.Object);

            var view = cuadroClinicoController.Index(It.IsAny<int>());

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void CrearUnCuadroClinicoCorrectamente()
        {
            var repositoryH = new Mock<IHistoriaClinicaRepository>();
            repositoryH.Setup(o => o.FindHistoriaClinicaById(It.IsAny<int>())).Returns(new HistoriaClinica());

            var repositoryC = new Mock<ICuadrosClinicosRepository>();
            repositoryC.Setup(o => o.CreateCuadroClinico(It.IsAny<CuadroClinico>()));

            var cookieAuthService = new Mock<ICookieAuthService>();
            cookieAuthService.Setup(o => o.UsuarioLogueado()).Returns(new Usuario());

            var repositoryU = new Mock<IUsuarioRepository>();

            var cuadroClinicoController = new CuadroClinicoController
                (repositoryH.Object, repositoryC.Object, cookieAuthService.Object, repositoryU.Object);

            var redirectToActionResul = cuadroClinicoController.Create
                (new CuadroClinico(), new FuncionesVitales(), new List<Sintomas>(), new List<Sintomas>());

            Assert.IsInstanceOf<RedirectToActionResult>(redirectToActionResul);
        }

        [Test]
        public void VerCuadroClinicoCorrectamente()
        {
            var repositoryH = new Mock<IHistoriaClinicaRepository>();
            var repositoryC = new Mock<ICuadrosClinicosRepository>();

            var cuadroClinico = new CuadroClinico();

            cuadroClinico.FuncionesVitales = "{\"PresionArterialSistolica\":75,\"PresionArterialDiastolica\":100,\"FrecuenciaCardiaca\":87,\"FrecuencaRespiratoria\":69,\"Temperatura\":36}";
            cuadroClinico.SignosSintomas = "[{\"Sintoma\":\"Tos\",\"EstaMarcado\":false},{\"Sintoma\":\"Dolor de Garganta\",\"EstaMarcado\":false},{\"Sintoma\":\"Congestion nasal\",\"EstaMarcado\":false},{\"Sintoma\":\"Fiebre\",\"EstaMarcado\":false},{\"Sintoma\":\"Malestar general\",\"EstaMarcado\":true},{\"Sintoma\":\"Dificultad respiratoria\",\"EstaMarcado\":false},{\"Sintoma\":\"Diarrea\",\"EstaMarcado\":false},{\"Sintoma\":\"Nausea/Vomito\",\"EstaMarcado\":false},{\"Sintoma\":\"Cefalea\",\"EstaMarcado\":false},{\"Sintoma\":\"Irritabilidad/Confusion\",\"EstaMarcado\":false},{\"Sintoma\":\"Dolor muscular\",\"EstaMarcado\":false},{\"Sintoma\":\"Dolor abdominal\",\"EstaMarcado\":false},{\"Sintoma\":\"Dolor pecho\",\"EstaMarcado\":false},{\"Sintoma\":\"Dolor articulaciones\",\"EstaMarcado\":false},{\"Sintoma\":null,\"EstaMarcado\":false}]";
            cuadroClinico.SintomasAlarma = "[{\"Sintoma\":\"Disnea\",\"EstaMarcado\":true},{\"Sintoma\":\"Taquipnea(\\u003E=22rpm)\",\"EstaMarcado\":false},{\"Sintoma\":\"Saturacion de oxigeno \\u003C 92%\",\"EstaMarcado\":false},{\"Sintoma\":\"Alteracion de la conciencia\",\"EstaMarcado\":false},{\"Sintoma\":\"Ningun Signo de alarma\",\"EstaMarcado\":false}]";

            repositoryC.Setup(o => o.FindCuadroClinicoById(It.IsAny<int>())).Returns(cuadroClinico);

            var cookieAuthService = new Mock<ICookieAuthService>();
            var repositoryU = new Mock<IUsuarioRepository>();
            repositoryU.Setup(o => o.UserByCuadroClinico(It.IsAny<CuadroClinico>())).Returns(new Usuario());

            var cuadroClinicoController = new CuadroClinicoController
                (repositoryH.Object, repositoryC.Object, cookieAuthService.Object, repositoryU.Object);

            var view = cuadroClinicoController.Ver(It.IsAny<int>());

            Assert.IsInstanceOf<ViewResult>(view);

        }
    }
}
