
using Microsoft.AspNetCore.Mvc;
using Sistema_TESIS.Repositories;

namespace Sistema_TESIS.Controllers
{
    public class HistoriaClinicaController : Controller
    {
        private readonly IHistoriaClinicaRepository repositoryH;

        public HistoriaClinicaController(IHistoriaClinicaRepository repositoryH)
        {
            this.repositoryH = repositoryH;
        }
        public IActionResult Index(int id)
        {
            var model = repositoryH.Persona(id);
            return View(model);
        }
         
    }
}