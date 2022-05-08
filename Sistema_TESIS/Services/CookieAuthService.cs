
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http; 
using System.Security.Claims;
using Sistema_TESIS.Models;
using Sistema_TESIS.Extensions;
using System.Linq;
using Sistema_TESIS.Models.DB;

namespace Sistema_TESIS.Services
{
    public interface ICookieAuthService
    {
        public void SetHttpContext(HttpContext httpContext);
        //public void SetSessionContext(Usuario usuariodb);
        public void Login(ClaimsPrincipal claim);
        public Usuario UsuarioLogueado();
        public void LogOut();
    }

    public class CookieAuthService : ICookieAuthService
    {
        private HttpContext httpContext;
        private readonly AppDbContext context;

        public CookieAuthService(AppDbContext context)
        {
            this.context = context;
        }

        public void SetHttpContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }
        public void Login(ClaimsPrincipal claim)
        {
            httpContext.SignInAsync(claim);
        }
        public Usuario UsuarioLogueado()
        {
            var claim = httpContext.User.Claims.FirstOrDefault();
            return context.Usuarios.Where(o => o.NombreUsuario == claim.Value).FirstOrDefault();
        }
        public void LogOut()
        {
            httpContext.SignOutAsync();
        }
    }
}
