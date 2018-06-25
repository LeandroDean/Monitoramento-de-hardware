using AutenticacaoAspNet.Models;
using AutenticacaoAspNet.ViewModels;
using AutenticacaoAspNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace AutenticacaoAspNet.Controllers
{
    public class AutenticacaoController : Controller
    {
        private UsuariosContext db = new UsuariosContext();

        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuarioViewModel viewmodel)
        {    
            // galera essa primeira logica é pra ver se os dados que usuario
            // digitou são validos, caso não for ele vai retornar para a view para o usuario corrigir

            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }


            if (db.Usuarios.Count(u => u.Login.Equals(viewmodel.Login)) > 0)
            {
                ModelState.AddModelError("Login", "Esse login já está em uso");
                return View(viewmodel);
            }

            // Logica para criar um novo usuario com os dados vinda da model
            Usuario novoUsuario = new Usuario
            {
                Nome = viewmodel.Nome,
                Login = viewmodel.Login,
                Senha = Hash.GerarHash(viewmodel.Senha)   // magica da criptografia guys
            };

            // add e salva
            db.Usuarios.Add(novoUsuario);
            db.SaveChanges();

            TempData["Mensagem"] = "Cadastro realizado com sucesso. Efetue login.";

            // tudo ok, usuario está sendo direcionado para tela de Login
            return RedirectToAction("Login");
        }

        // Esse metodo faz voltar a tela que o usuario tenta entrar apos efetuar o login
        // essa parte eu explico pessoalmente, me perguntem em sala
        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            // localizar usuario digitado com o usuario do banco
          
            var usuario = db.Usuarios.FirstOrDefault(u => u.Login == viewmodel.Login);

            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(viewmodel);
            }

            if (usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(viewmodel);
            }


            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login)
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!String.IsNullOrWhiteSpace(viewmodel.UrlRetorno) || Url.IsLocalUrl(viewmodel.UrlRetorno))
                return Redirect(viewmodel.UrlRetorno);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Autenticacao");
        }
    }
}