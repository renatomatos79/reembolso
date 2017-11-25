using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class TipoDespesaController : Controller
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
                var response = new List<TipoDespesaModel>
                {
                    new TipoDespesaModel { Id = 1, Nome = "Taxi", RequerAutorizacao = true, RequerComprovante = true, RequerTrajeto = true },
                    new TipoDespesaModel { Id = 2, Nome = "Transporte Público", RequerAutorizacao = true, RequerComprovante = false, RequerTrajeto = true },
                    new TipoDespesaModel { Id = 3, Nome = "Estacionamento", RequerAutorizacao = true, RequerComprovante = false, RequerTrajeto = false }
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

        public ActionResult Index()
        {
            return View();
        }
    }
}