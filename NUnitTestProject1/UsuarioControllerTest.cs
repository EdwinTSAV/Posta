
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sistema_TESIS.Controllers;
using Sistema_TESIS.Models;
using Sistema_TESIS.Repositories;
using Sistema_TESIS.Services;
using System;

namespace PruebasUnitarias
{
    class UsuarioControllerTest
    {
        [Test]
        public void CrearUsuarioCorrectamente()
        {
            var usuarioMock = new Mock<IUsuarioRepository>();
            var controller = new UsuarioController(usuarioMock.Object, null);
            var view = controller.Create(new Usuario());

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
        [Test]
        public void UsuarioLogueadoCorrectamente()
        {
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(o => o.FindUserLogin("admin", It.IsAny<String>()))
                .Returns(new Usuario { NombreUsuario="admin", Password="admin" });

            var cookie = new Mock<ICookieAuthService>();
            var controller = new UsuarioController(mockRepository.Object, cookie.Object);
            var view = controller.Login("admin", "admin");

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void UsuarioLogueadoFallido()
        {
            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(o => o.FindUserLogin("admin", It.IsAny<String>()));
            var cookie = new Mock<ICookieAuthService>();
            var controller = new UsuarioController(mockRepository.Object, cookie.Object);
            var view = controller.Login("admin", "admin");

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void ActualizarUsuarioCorrectamente()
        {
            var mockRepository = new Mock<IUsuarioRepository>();
            var cookie = new Mock<ICookieAuthService>();
            var controller = new UsuarioController(mockRepository.Object, cookie.Object);
            var view = controller.Update(new Usuario ());
            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }

        [Test]
        public void ActualizarUsuarioFallido()
        {
            var mockRepository = new Mock<IUsuarioRepository>();
            var cookie = new Mock<ICookieAuthService>();
            var controller = new UsuarioController(mockRepository.Object, cookie.Object);
            var view = controller.Update(null);

            Assert.IsInstanceOf<ViewResult>(view);
        }
        [Test]
        public void LogOutCorrecto()
        {
            var mockRepository = new Mock<IUsuarioRepository>();
            var cookie = new Mock<ICookieAuthService>();
            var controller = new UsuarioController(mockRepository.Object, cookie.Object);
            var view = controller.Logout();

            Assert.IsInstanceOf<RedirectToActionResult>(view);
        }
    }
}
