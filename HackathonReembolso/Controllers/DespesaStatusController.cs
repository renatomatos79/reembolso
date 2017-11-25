using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class DespesaStatusController : Controller
    {
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = new JsonResponse();
            try
            {
                var response = new List<DespesaStatus>
                {
                    new DespesaStatus { Id = 1, Sigla = "C", Descricao = "Cadastro" },
                    new DespesaStatus { Id = 2, Sigla = "R", Descricao = "Rejeitada" },
                    new DespesaStatus { Id = 3, Sigla = "A", Descricao = "Aprovada" },
                    new DespesaStatus { Id = 4, Sigla = "K", Descricao = "Cancelada" }
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
    }
}