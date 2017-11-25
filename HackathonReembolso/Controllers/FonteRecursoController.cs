using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class FonteRecursoController : Controller
    {
        [HttpGet]
        public JsonResult Search(string query, int page, int records)
        {
            var result = new JsonResponse();
            try
            {
                var response = new List<FonteRecursoModel>
                {
                    new FonteRecursoModel { Id = 1, Descricao = "Programador" },
                    new FonteRecursoModel { Id = 2, Descricao = "Analista de Requisitos" },
                    new FonteRecursoModel { Id = 3, Descricao = "Analista de Negócios" },
                    new FonteRecursoModel { Id = 4, Descricao = "Analista de Sistemas" }
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