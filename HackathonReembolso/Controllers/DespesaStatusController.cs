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
        public List<DespesaStatusModel> GetStatus()
        {
            return new List<DespesaStatusModel>
                {
                    new DespesaStatusModel { Id = 1, Sigla = "C", Descricao = "Cadastro" },
                    new DespesaStatusModel { Id = 2, Sigla = "R", Descricao = "Rejeitada" },
                    new DespesaStatusModel { Id = 3, Sigla = "A", Descricao = "Aprovada" },
                    new DespesaStatusModel { Id = 4, Sigla = "K", Descricao = "Cancelada" }
                };
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = new JsonResponse();
            try
            {
                
                result = new JsonResponse { Data = GetStatus() };
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