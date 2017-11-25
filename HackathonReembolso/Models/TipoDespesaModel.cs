using HackathonReembolso.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HackathonReembolso.Mvc.Models
{
    [Serializable]
    public class TipoDespesaModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public bool RequerComprovante { get; set; }
        [DataMember]
        public bool RequerTrajeto { get; set; }
        [DataMember]
        public bool RequerAutorizacao { get; set; }
    }
}