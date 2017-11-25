using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class BancoController : Controller
    {
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
                var response = new List<BancoModel>
                {
                    new BancoModel { Id = 1, Codigo = "001", Nome = "Banco do Brasil" },
                    new BancoModel { Id = 2, Codigo = "341", Nome = "Banco Itau" },
                    new BancoModel { Id = 3, Codigo = "333", Nome = "Banco Santander" },
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

        // GET: Banco
        public ActionResult Index()
        {
            return View();
        }
    }
}