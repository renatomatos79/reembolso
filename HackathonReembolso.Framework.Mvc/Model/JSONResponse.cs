using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HackathonReembolso.Framework.Mvc.Model
{
    [Serializable]
    public class JsonResponse
    {
        [DataMember]
        public object Data { get; set; }
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public int Pages { get; set; }
        [DataMember]
        public int Records { get; set; }
        [DataMember]
        public int RecordsPerPage { get; set; }
        [DataMember]
        public List<string> Errors { get; set; }

        public JsonResponse()
        {
            Result = true;
            Data = null;
            Pages = 0;
            Records = 0;
            RecordsPerPage = 0;
            Message = string.Empty;
            Errors = new List<string>();
        }

        public JsonResponse(Exception ex, string path)
            : this()
        {
            Message = ex.Message;
        }
    }
}