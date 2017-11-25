using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class FontePagadoraController : Controller
    {
        [HttpGet]
        public JsonResult Search(string query, int page, int records)
        {
            var result = new JsonResponse();
            try
            {
                var response = new List<FontePagadoraModel>
                {
                    new FontePagadoraModel { Id = 1, Nome = "Programador", Prioridade = 10 },
                    new FontePagadoraModel { Id = 2, Nome = "Analista de Requisitos", Prioridade = 11 },
                    new FontePagadoraModel { Id = 3, Nome = "Analista de Negócios", Prioridade = 12 },
                    new FontePagadoraModel { Id = 4, Nome = "Analista de Sistemas", Prioridade = 13 }
                };
                result = new JsonResponse { Data = response };
            }
            catch (Exception ex)
            {
                result.Message = "Houve um erro ao processar sua requisição! Favor tente novamente!";
                result.Errors.Add(ex.ToString());
                result.Result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: FontePagadora
        public ActionResult Index()
        {
            return View();
        }
    }
}