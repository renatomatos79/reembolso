using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonReembolso.Mvc.Models
{
    [Serializable]
    public class ProjetoModel
    {
        public int Id { get; set; }
        public GerenciaModel Gerente { get; set; }
        public string Nome { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
    }
}