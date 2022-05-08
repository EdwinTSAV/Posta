
using Microsoft.EntityFrameworkCore;
using Sistema_TESIS.Models;
using Sistema_TESIS.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Sistema_TESIS.Repositories
{
    public interface IPersonaRepository
    {
        public List<Persona> ListaPersonasBD();
        public void AgregarPersonas(Persona persona);
        public List<Persona> BuscarPersonas(string nombre, string dni);
        public Persona FindPersonaById(int id);
        public void ActualizarPersona(Persona persona);
    }
    public class PersonaRepository : IPersonaRepository
    {
        public readonly AppDbContext context;

        public PersonaRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<Persona> ListaPersonasBD()
        {
            var personas = context.Personas.ToList();

            return personas;
        }
        public void AgregarPersonas(Persona persona)
        {
            context.Personas.Add(persona);
            context.SaveChangesAsync().Wait();
        }
        public List<Persona> BuscarPersonas(string nombre, string dni)
        {
            
            if (!String.IsNullOrEmpty(nombre)  )
            {
                var model = context.Personas.Where(o => o.Nombres.Contains(nombre)).ToList();
                return model;
            }
            if (!String.IsNullOrEmpty(dni))
            {
                var model = context.Personas.Where(o => o.DNI.Contains(dni)).ToList();
                return model;
            }
            return context.Personas.OrderByDescending(o => o.PersonaId).ToList();
        }

        public Persona FindPersonaById(int id)
        {
            var model = context.Personas.FirstOrDefault(o => o.PersonaId == id);
            return model;
        }

        public void ActualizarPersona(Persona persona)
        {
            context.Entry(persona).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
