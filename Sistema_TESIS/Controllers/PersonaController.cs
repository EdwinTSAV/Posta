
using Microsoft.AspNetCore.Mvc;
using Sistema_TESIS.Models;
using Sistema_TESIS.Repositories;
using System.Linq;

namespace Sistema_TESIS.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaRepository repositoryP;

        public PersonaController(IPersonaRepository repositoryP)
        {
            this.repositoryP = repositoryP;
        }

        public IActionResult Index(string nombre, string dni)
        {
            var model = repositoryP.BuscarPersonas(nombre, dni);

            if (model == null)
            {
                ModelState.AddModelError("Resultado", "No se encontro ningun resultado");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Persona());
        }

        [HttpPost]
        public IActionResult Create(Persona persona)
        {
            if (repositoryP.ListaPersonasBD().Where(o => o.DNI == persona.DNI).Count() > 0)
            {
                ModelState.AddModelError("Paciente", "Ya existe un paciente con ese DNI");
            }
            if (persona == null)
            {
                ModelState.AddModelError("Datos Nulos","Los datos no deben ser Nulos");
            }

            if (ModelState.IsValid)
            {
                repositoryP.AgregarPersonas(persona);
                return RedirectToAction("Index", "Usuario");
            }
            return View(persona);
        }

        [HttpGet]
        public IActionResult Update(int PersonaId)
        {
            var persona = repositoryP.FindPersonaById(PersonaId);
            return View(persona);
        }

        [HttpPost]
        public IActionResult Update(Persona persona)
        {
            if (persona == null)
            {
                ModelState.AddModelError("Datos Nulos", "Los datos no deben ser Nulos");
            }
            if (ModelState.IsValid)
            {
                repositoryP.ActualizarPersona(persona);
                return RedirectToAction("Index", "Usuario");
            }
            return View(persona);
        }


    }
}
