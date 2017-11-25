using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class CentroCustoController : Controller
    {
        [HttpGet]
        public JsonResult Search(string query, int page, int records)
        {
            var result = new JsonResponse();
            try
            {
                var response = new List<CentroCustoModel>
                {
                    new CentroCustoModel { Id = 1, Descricao = "SSI", CodigoExterno="SSI" },
                    new CentroCustoModel { Id = 2, Descricao = "Saúde", CodigoExterno="SAU" },
                    new CentroCustoModel { Id = 3, Descricao = "Contábil", CodigoExterno="CTBL" },
                    new CentroCustoModel { Id = 4, Descricao = "Inovação", CodigoExterno="INOVA" }
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

        // GET: Cargo
        public ActionResult Index()
        {
            return View();
        }
    }
}