using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public JsonResult GetUsuarioList(string nome)
        {
            var result = new JsonResponse();
            try
            {
                var response = new List<UsuarioModel>
                {
                    new UsuarioModel { Id = 1, Nome = "Renato" },
                    new UsuarioModel { Id = 2, Nome = "Rodrigo" },
                    new UsuarioModel { Id = 3, Nome = "Rafael" },
                    new UsuarioModel { Id = 4, Nome = "Rubens" },
                    new UsuarioModel { Id = 5, Nome = "Armando" },
                    new UsuarioModel { Id = 6, Nome = "Arnaldo" },
                    new UsuarioModel { Id = 7, Nome = "Eduardo" }
                };
                result = new JsonResponse { Data = response.Where(w=>w.Nome.ToUpper().Contains(nome.ToUpper())).ToList() };
            }
            catch (Exception ex)
            {
                result.Message = "Houve um erro ao processar sua requisição! Favor tente novamente!";
                result.Errors.Add(ex.ToString());
                result.Result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}