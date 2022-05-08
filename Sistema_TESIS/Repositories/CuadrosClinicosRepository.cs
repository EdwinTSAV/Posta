
using Microsoft.EntityFrameworkCore;
using Sistema_TESIS.Models;
using Sistema_TESIS.Models.DB;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Text.Json;

namespace Sistema_TESIS.Repositories
{
    public interface ICuadrosClinicosRepository
    {
        public List<CuadroClinico> CadrosClinicosBD(int personaId);
        public void CreateCuadroClinico(CuadroClinico cuadroClinico);
        public CuadroClinico FindCuadroClinicoById(int Id);
    }
    public class CuadrosClinicosRepository : ICuadrosClinicosRepository
    {
        public readonly AppDbContext context;
        private readonly IWebHostEnvironment hosting;
        public CuadrosClinicosRepository(AppDbContext context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
        }
        public List<CuadroClinico> CadrosClinicosBD(int personaId)
        {
            var model = context.CuadrosClinicos.Where(o => o.PersonaId == personaId).ToList();

            return model;
        }

        public void CreateCuadroClinico(CuadroClinico cuadroClinico)
        {
            context.CuadrosClinicos.Add(cuadroClinico);
            context.SaveChanges();

            var persona = context.Personas.Where(o => o.PersonaId == cuadroClinico.PersonaId).FirstOrDefault();
            var funcionesVitales = JsonSerializer.Deserialize<FuncionesVitales>(cuadroClinico.FuncionesVitales);
            //var sintomas = JsonSerializer.Deserialize<List<Sintomas>>(cuadroClinico.SignosSintomas);
            //var sintomasAlarma = JsonSerializer.Deserialize<List<Sintomas>>(cuadroClinico.SintomasAlarma);

            var file = Path.Combine(hosting.WebRootPath, Path.Combine("Historia", cuadroClinico.Fecha.ToString("ddMMyyy") + cuadroClinico.CuadroClinicoId + ".pdf"));
            using (var archivo = new FileStream(file, FileMode.Create))
            {
                Document pdf = new Document(PageSize.A4, 25, 25, 25, 25);
                PdfWriter escribir = PdfWriter.GetInstance(pdf, archivo);
                pdf.Open();
                string html;
                using (var sr = new StreamReader(Path.Combine(hosting.WebRootPath, Path.Combine("Historia", "Historia.cshtml"))))
                {
                    html = sr.ReadToEnd();
                }
                html = html.Replace("APNOMPER", persona.Apellidos + " " + persona.Nombres);
                html = html.Replace("FECNAC", persona.FechaNacimiento.ToString("d"));
                html = html.Replace("DNIPER", persona.DNI);
                html = html.Replace("SEXPER", persona.Sexo);
                html = html.Replace("OCUPER", persona.Ocupacion);
                html = html.Replace("TELPER", persona.TelefonoEmergencia);
                html = html.Replace("RESPER", persona.Responsable);
                html = html.Replace("ESTPER", persona.EstadoCivil);
                html = html.Replace("FECPER", cuadroClinico.Fecha.ToString("d"));
                html = html.Replace("CELPER", persona.NroCelular);
                html = html.Replace("HORPER", cuadroClinico.Fecha.ToString("T"));
                html = html.Replace("OBSPER", cuadroClinico.Observaciones);
                html = html.Replace("ANTPER", cuadroClinico.Antecedentes);
                html = html.Replace("ALEPER", persona.Alergias);
                html = html.Replace("VACPER", persona.Vacunas);
                html = html.Replace("INTPER", persona.CondicionDeRiesgo);
                if (cuadroClinico.Examen == "Otro")
                    html = html.Replace("EXAPER", "Traslado a otro hospital");
                else
                    html = html.Replace("EXAPER", cuadroClinico.Examen);
                html = html.Replace("DIAPER", cuadroClinico.Diagnostico);
                html = html.Replace("PREARTPER", funcionesVitales.PresionArterialSistolica + "/" + funcionesVitales.PresionArterialDiastolica);
                html = html.Replace("TEMPER", funcionesVitales.Temperatura.ToString());
                html = html.Replace("FRESPER", funcionesVitales.FrecuencaRespiratoria.ToString());
                html = html.Replace("FREPER", funcionesVitales.FrecuenciaCardiaca.ToString());
                html = html.Replace("PESPER", persona.Peso.ToString());
                html = html.Replace("TALPER", persona.Talla.ToString());
                decimal imc = persona.Peso / (persona.Talla * persona.Talla);
                html = html.Replace("IMCPER", Math.Round(imc,2).ToString());

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Path.Combine(hosting.WebRootPath, Path.Combine("Historia", "Banner.jpg")));
                img.ScaleToFit(100, 80);
                img.SetAbsolutePosition(pdf.LeftMargin, pdf.Top - 60);
                pdf.Add(img);
                using (var sr = new StringReader(html))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(escribir, pdf, sr);
                }
                pdf.Close();
                archivo.Close();
            }

        }

        public CuadroClinico FindCuadroClinicoById(int Id)
        {
            var model = context.CuadrosClinicos.
                Include(o => o.Usuario).
                Include(o => o.Usuario.Tipo).
                FirstOrDefault(o => o.CuadroClinicoId == Id);
            return model;
        }
    }
}
