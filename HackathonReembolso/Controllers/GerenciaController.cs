using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class GerenciaController : Controller
    {
        public List<GerenciaModel> GetGerencias()
        {
            return new List<GerenciaModel>
                {
                    new GerenciaModel { Id = 1, Nome = "Renato" },
                    new GerenciaModel { Id = 2, Nome = "Rodrigo" },
                    new GerenciaModel { Id = 3, Nome = "Rafael" },
                    new GerenciaModel { Id = 4, Nome = "Rubens" },
                    new GerenciaModel { Id = 5, Nome = "Armando" },
                    new GerenciaModel { Id = 6, Nome = "Arnaldo" },
                    new GerenciaModel { Id = 7, Nome = "Eduardo" }
                };
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return Search(string.Empty, 1, int.MaxValue);
        }

        [HttpGet]
        public JsonResult GetGerenciaList(string nome)
        {
            var result = new JsonResponse();
            try
            {
                result = new JsonResponse { Data = GetGerencias().Where(w => w.Nome.ToUpper().Contains(nome.ToUpper())).ToList() };
            }
            catch (Exception ex)
            {
                result.Message = "Houve um erro ao processar sua requisição! Favor tente novamente!";
                result.Errors.Add(ex.ToString());
                result.Result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Search(string query, int page, int records)
        {
            var result = new JsonResponse();
            try
            {
                var response = new List<GerenciaModel>
                {
                    new GerenciaModel { Id = 1, Nome = "Presidência", Setores=new List<GerenciaSetorModel>
                    {
                        new GerenciaSetorModel { Id=1,Nome="Financeiro" }
                    } },
                    new GerenciaModel { Id = 2, Nome = "SSI", GerenciaSuperiorId=1, Setores=new List<GerenciaSetorModel>
                    {
                        new GerenciaSetorModel { Id=1,Nome="Contábil" },
                        new GerenciaSetorModel { Id=2,Nome="Financeiro" },
                        new GerenciaSetorModel { Id=3,Nome="RH" }
                    } },
                    new GerenciaModel { Id = 3, Nome = "Inovação", GerenciaSuperiorId=1, Setores=new List<GerenciaSetorModel>
                    {
                        new GerenciaSetorModel { Id=1,Nome="Contábil" },
                        new GerenciaSetorModel { Id=2,Nome="Financeiro" },
                    } },
                    new GerenciaModel { Id = 3, Nome = "Educação", GerenciaSuperiorId=1, Setores=new List<GerenciaSetorModel>
                    {
                        new GerenciaSetorModel { Id=1,Nome="Contábil" },
                        new GerenciaSetorModel { Id=1,Nome="Financeiro" },
                    } },
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

        // GET: Gerencia
        public ActionResult Index()
        {
            return View();
        }
    }
}