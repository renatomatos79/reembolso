using System;

namespace HackathonReembolso.Framework.Mvc.Model
{
    [Serializable]
    public class BaseModel
    {
        public BaseModel()
        {
            this.ResultCode = null;
        }

        public virtual int? ResultCode { get; set; }
    }
}
