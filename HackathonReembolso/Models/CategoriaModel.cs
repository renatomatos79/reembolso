using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HackathonReembolso.Mvc.Models
{
    [Serializable]
    public class CategoriaModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public TipoDespesaModel TipoDespesa { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public bool ValorFixo { get; set; }
        [DataMember]
        public decimal Valor { get; set; }
    }
}