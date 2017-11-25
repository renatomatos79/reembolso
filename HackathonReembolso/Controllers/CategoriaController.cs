using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class CategoriaController : Controller
    {
        [HttpGet]
        public JsonResult GetCategoriaList(int tipoDespesaId)
        {
            return Search(tipoDespesaId, string.Empty, 1, int.MaxValue);
        }

        [HttpGet]
        public JsonResult Search(int tipoDespesaId, string query, int page, int records)
        {
            var result = new JsonResponse();
            try
            {
                var tipoDespesa = new TipoDespesaController().GetList();

                var response = new List<CategoriaModel>
                {
                    new CategoriaModel { Id = 1, Descricao = "Uber", ValorFixo=false, TipoDespesa = tipoDespesa.FirstOrDefault(w=>w.Nome == "Taxi") },
                    new CategoriaModel { Id = 2, Descricao = "Particular", ValorFixo=true, Valor=(decimal)25.0, TipoDespesa = tipoDespesa.FirstOrDefault(w=>w.Nome == "Taxi") },
                    new CategoriaModel { Id = 3, Descricao = "Estacionamento", ValorFixo=false, TipoDespesa = tipoDespesa.FirstOrDefault(w=>w.Nome == "Estacionamento") }
                };

                var filter = new List<CategoriaModel>();
                if (tipoDespesaId > 0)
                {
                    filter.AddRange(response.Where(w => w.TipoDespesa.Id == tipoDespesaId).ToList());
                }
                else
                {
                    filter.AddRange(response);
                }

                result = new JsonResponse { Data = filter };
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