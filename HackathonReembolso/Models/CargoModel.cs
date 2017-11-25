using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HackathonReembolso.Mvc.Models
{
    public class CargoModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descricao { get; set; }
    }
}