using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HackathonReembolso.Mvc.Models
{
    [Serializable]
    public class UsuarioModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public CentroCustoModel CentroCusto { get; set; }
        [DataMember]
        public GerenciaModel Gerencia { get; set; }
        [DataMember]
        public PerfilModel Perfil { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Matricula { get; set; }
        [DataMember]
        public string Cpf { get; set; }
        [DataMember]
        public string Telefone { get; set; }
        [DataMember]
        public CargoModel Cargo { get; set; }
        [DataMember]
        public BancoModel Banco { get; set; }
        [DataMember]
        public string Agencia { get; set; }
        [DataMember]
        public string ContaCorrente { get; set; }
    }
}