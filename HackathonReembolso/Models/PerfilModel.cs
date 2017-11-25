using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HackathonReembolso.Mvc.Models
{
    [Serializable]
    public class PerfilModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string SiglaInternaSistema { get; set; }
        [DataMember]
        public string SiglaGerencia { get; set; }
        [DataMember]
        public bool Aprovador { get; set; }
    }
}