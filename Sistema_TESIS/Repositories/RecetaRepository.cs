
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Hosting;
using Sistema_TESIS.Models;
using Sistema_TESIS.Models.DB;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sistema_TESIS.Repositories
{
    public interface IRecetaRepository
    {
        public void AñadirRecetas(Receta receta);
        public Receta GetReceta(int RecetaId);
        public int EliminarReceta(int RecetaId);
        public List<Receta> GetRecetasByCuadroClinico(int CuadroClinicoId);
    }
    public class RecetaRepository : IRecetaRepository
    {
        public readonly AppDbContext context;
        private readonly IWebHostEnvironment hosting;
        public List<Receta> GetRecetasByCuadroClinico(int CuadroClinicoId)
        {
            var recetas = context.Recetas.Where(o => o.CuadroClinicoId == CuadroClinicoId).ToList();

            var cuadro = context.CuadrosClinicos.Where(o => o.CuadroClinicoId == CuadroClinicoId).FirstOrDefault();
            var persona = context.Personas.Where(o => o.PersonaId == cuadro.PersonaId).FirstOrDefault();

            var file = Path.Combine(hosting.WebRootPath, Path.Combine("Recetas", persona.PersonaId + CuadroClinicoId + ".pdf"));
            using (var archivo = new FileStream(file, FileMode.Create))
            {
                Document pdf = new Document(PageSize.A4, 25, 25, 25, 25);
                PdfWriter escribir = PdfWriter.GetInstance(pdf, archivo);
                pdf.Open();
                string html;
                using (var sr = new StreamReader(Path.Combine(hosting.WebRootPath, Path.Combine("Recetas", "Recetas.cshtml"))))
                {
                    html = sr.ReadToEnd();
                }

                string filaser = string.Empty;
                foreach (var item in recetas)
                {
                    filaser += "<tr>"
                    + "<td>&nbsp;" + item.Medicamento + "</td>"
                    + "<td>&nbsp;" + item.Dosis + "</td>"
                    + "<td>&nbsp;" + item.Duracion + "</td>"
                    + "<td>&nbsp;" + item.Cantidad + "</td>"
                    + "</tr>";
                }

                html = html.Replace("MEDICAMENTO", filaser);
                html = html.Replace("APENOM", persona.Apellidos + " " + persona.Nombres);

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Path.Combine(hosting.WebRootPath, Path.Combine("Recetas", "Banner.jpg")));
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

            return recetas;
        }
        public RecetaRepository(AppDbContext context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
        }
        public void AñadirRecetas(Receta receta)
        {
            context.Recetas.Add(receta);
            context.SaveChanges();
        }
        public Receta GetReceta(int RecetaId)
        {
            return context.Recetas
                .Where(o => o.RecetaId == RecetaId)
                .FirstOrDefault();
        }
        public int EliminarReceta(int RecetaId)
        {

            var receta = context.Recetas.Where(o => o.RecetaId == RecetaId).First();
            var cuadroId = receta.CuadroClinicoId;
            context.Recetas.Remove(receta);
            context.SaveChanges();
            return cuadroId;
        }
    }
}
