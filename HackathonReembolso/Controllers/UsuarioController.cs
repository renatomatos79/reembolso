using HackathonReembolso.Framework.Mvc.Model;
using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonReembolso.Mvc.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public JsonResult GetUsuarioList(string nome)
        {
            return Search(nome, 1, int.MaxValue);
        }

        [HttpGet]
        public JsonResult Search(string query, int page, int records)
        {
            var result = new JsonResponse();
            try
            {
                var response = new List<UsuarioModel>
                {
                    new UsuarioModel { Id = 1, Nome = "Renato", Matricula="1234", Cpf="79363610306" },
                    new UsuarioModel { Id = 2, Nome = "Rodrigo", Matricula="1235", Cpf="62116215315"  },
                    new UsuarioModel { Id = 3, Nome = "Rafael", Matricula="5432", Cpf=""  },
                    new UsuarioModel { Id = 4, Nome = "Rubens", Matricula="0001", Cpf=""  },
                    new UsuarioModel { Id = 5, Nome = "Armando", Matricula="0022", Cpf=""  },
                    new UsuarioModel { Id = 6, Nome = "Arnaldo", Matricula="3322", Cpf=""  },
                    new UsuarioModel { Id = 7, Nome = "Eduardo", Matricula="6699", Cpf="14254125487"  }
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