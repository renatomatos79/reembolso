﻿using HackathonReembolso.Framework.Mvc.Model;
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
        public List<CentroCustoModel> GetCCustos()
        {
            return new List<CentroCustoModel>
                {
                    new CentroCustoModel { Id = 1, Nome = "SSI", CodigoExterno="SSI" },
                    new CentroCustoModel { Id = 2, Nome = "Saúde", CodigoExterno="SAU" },
                    new CentroCustoModel { Id = 3, Nome = "Contábil", CodigoExterno="CTBL" },
                    new CentroCustoModel { Id = 4, Nome = "Inovação", CodigoExterno="INOVA" }
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
                
                result = new JsonResponse { Data = GetCCustos() };
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