using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using AutenticacaoAspNet.Models;

namespace AutenticacaoAspNet.Controllers
{
    public class VersionController : Controller
    {
        HttpClient client;
        // GET: Version
        public ActionResult Version()
        {
            return View();
        }

        public String GetValor()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("http://restapiteste10020180609075137.azurewebsites.net");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = client.GetAsync("/api/Leituras/2").Result;


            //Random rn = new Random();
            //var teste = rn.Next(0, 50);

            string teste = response.ToString();
            return response.ToString();
           

        }




    }
}