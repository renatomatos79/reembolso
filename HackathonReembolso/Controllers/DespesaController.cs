using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class DespesaController : Controller
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
                var categoria  = new CategoriaController().GetAll();
                var ccusto = new CentroCustoController().GetCCustos();
                var usuario = new UsuarioController().GetUsers();
                var gerencia = new GerenciaController().GetGerencias();
                var projeto = new ProjetoController().GetProjetos();
                var status = new DespesaStatusController().GetStatus();

                var response = new List<DespesaModel>
                {
                    new DespesaModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Categoria = categoria.FirstOrDefault(w=>w.Descricao=="Uber"),
                        CentroCusto = ccusto.FirstOrDefault(w=>w.Nome=="SSI"),
                        Usuario = usuario.FirstOrDefault(w=>w.Nome=="Renato"),
                        Gerencia = gerencia.FirstOrDefault(w=>w.Nome == "Rubens"),
                        Projeto = projeto.FirstOrDefault(w=>w.Nome=="Estoque"),
                        Status = status.FirstOrDefault(w=>w.Sigla=="C"),
                        Placa="HTX-1222",
                        Descricao="teste renato",
                        DataDespesa = DateTime.Now.ToString("dd/MM/yyyy"),
                        Valor = 200,
                        DataCompetencia = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"),
                        DataRealizacao=string.Empty
                    }
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