

using Microsoft.EntityFrameworkCore;
using Sistema_TESIS.Models;
using Sistema_TESIS.Models.DB;
using System.Collections.Generic;
using System.Linq;


namespace Sistema_TESIS.Repositories
{
    public interface IHistoriaClinicaRepository
    {
        public Persona Persona(int id);
        public List<CuadroClinico> GetCuadroClinicos(int idUser);
    }
    public class HistoriaClinicaRepository : IHistoriaClinicaRepository
    {
        private readonly AppDbContext context;

        public HistoriaClinicaRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Persona Persona(int id)
        {
            return context.Personas.Where(o => o.PersonaId == id)
                .FirstOrDefault();
        }
        public List<CuadroClinico> GetCuadroClinicos(int idUser)
        {
            return context.CuadrosClinicos.Where(o => o.PersonaId == idUser).ToList();
        }
    }
}
