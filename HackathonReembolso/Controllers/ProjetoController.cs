using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class ProjetoController : Controller
    {
        public List<ProjetoModel> GetProjetos()
        {
            var gerente = new GerenciaController().GetGerencias();

            return new List<ProjetoModel>
                {
                    new ProjetoModel { Id = 1, Nome = "Compras", Gerente = gerente.FirstOrDefault(w=>w.Nome == "Renato") },
                    new ProjetoModel { Id = 1, Nome = "Estoque", Gerente = gerente.FirstOrDefault(w=>w.Nome == "Renato") },
                    new ProjetoModel { Id = 1, Nome = "RBAC", Gerente = gerente.FirstOrDefault(w=>w.Nome == "Rafael") }
                };
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return Search(string.Empty, 1, int.MaxValue);
        }

        [HttpGet]
        public JsonResult Search(string query, int page, int records)
        {
            var result = new JsonResponse();
            try
            {
                result = new JsonResponse { Data = GetProjetos() };
            }
            catch (Exception ex)
            {
                result.Message = "Houve um erro ao processar sua requisição! Favor tente novamente!";
                result.Errors.Add(ex.ToString());
                result.Result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Cargo
        public ActionResult Index()
        {
            return View();
        }
    }
}