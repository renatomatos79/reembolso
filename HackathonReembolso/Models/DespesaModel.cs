using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HackathonReembolso.Mvc.Models
{
    [Serializable]
    public class DespesaModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public CategoriaModel Categoria { get; set; }
        [DataMember]
        public CentroCustoModel CentroCusto { get; set; }
        [DataMember]
        public UsuarioModel Usuario { get; set; }
        [DataMember]
        public GerenciaModel Gerencia { get; set; }
        [DataMember]
        public ProjetoModel Projeto { get; set; }
        [DataMember]
        public DespesaStatusModel Status { get; set; }
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public string Placa { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public string DataDespesa { get; set; }
        [DataMember]
        public decimal Valor { get; set; }
        [DataMember]
        public string Observacao  { get; set; }
        [DataMember]
        public string DataCompetencia { get; set; }
        [DataMember]
        public string DataRealizacao { get; set; }
    }
}