using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;  
using Microsoft.Owin.Security.Cookies;  // biblioteca responsavel pela autenticação
using System.Web.Helpers;

[assembly: OwinStartup(typeof(AutenticacaoAspNet.Startup))]

namespace AutenticacaoAspNet
{
    public class Startup
    {

        // time aqui estou utilizando a biblioteca OWIN,
        // Quando usuario tenta entrar em alguma area restrista sem estar logado
        // o OWIN vai redirecionar automaticamente para  /Autenticacao/Login
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Autenticacao/Login")
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";
        }
    }
}
