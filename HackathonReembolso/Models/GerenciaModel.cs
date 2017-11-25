using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HackathonReembolso.Mvc.Models
{
    [Serializable]
    public class GerenciaModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public int? GerenciaSuperiorId { get; set; }
        [DataMember]
        public List<GerenciaSetorModel> Setores { get; set; }
    }

    [Serializable]
    public class GerenciaSetorModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
    }
}