using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Sistema_TESIS.Repositories;

using Sistema_TESIS.Models;
using Sistema_TESIS.Services;
using System;

namespace Sistema_TESIS.Controllers
{
    public class CuadroClinicoController : Controller
    {
        private readonly IHistoriaClinicaRepository repositoryH;
        private readonly ICuadrosClinicosRepository repositoryC;
        private readonly ICookieAuthService cookieAuthService;
        private readonly IUsuarioRepository repositoryU;
        public CuadroClinicoController(IHistoriaClinicaRepository repositoryH, ICuadrosClinicosRepository repositoryC, ICookieAuthService cookieAuthService, IUsuarioRepository repositoryU)
        {
            this.repositoryH = repositoryH;
            this.repositoryC = repositoryC;
            this.cookieAuthService = cookieAuthService;
            this.repositoryU = repositoryU;
        }
        public IActionResult Index(int idUser)
        {
            ViewBag.Historia = idUser;
            var model = repositoryC.CadrosClinicosBD(idUser);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int personaId)
        {
            ViewBag.PersonaId = personaId;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CuadroClinico cuadroClinico, FuncionesVitales vitales, List<Sintomas> sintomas, List<Sintomas> alarma)
        {
            cookieAuthService.SetHttpContext(HttpContext);
            var usuario = cookieAuthService.UsuarioLogueado();
            cuadroClinico.Fecha = DateTime.Now;
            cuadroClinico.FuncionesVitales = JsonSerializer.Serialize(vitales).ToString();
            cuadroClinico.SignosSintomas = JsonSerializer.Serialize(sintomas);
            cuadroClinico.SintomasAlarma = JsonSerializer.Serialize(alarma);
            cuadroClinico.UsuarioId = usuario.UsuarioId;

            if (ModelState.IsValid) {
                repositoryC.CreateCuadroClinico(cuadroClinico);
                return RedirectToAction("Index","HistoriaClinica", new { id = cuadroClinico.PersonaId });
            }
            ViewBag.PersonaId = cuadroClinico.PersonaId;
            return View(cuadroClinico);
        }
        [HttpGet]
        public IActionResult Ver(int id)
        {
            var cuadroClinico = repositoryC.FindCuadroClinicoById(id);
            ViewBag.funcionesVitales = JsonSerializer.Deserialize<FuncionesVitales>(cuadroClinico.FuncionesVitales);
            ViewBag.Sintomas = JsonSerializer.Deserialize<List<Sintomas>>(cuadroClinico.SignosSintomas);
            ViewBag.SintomasAlarma = JsonSerializer.Deserialize<List<Sintomas>>(cuadroClinico.SintomasAlarma);
            return View(cuadroClinico);
        }
    }
}
