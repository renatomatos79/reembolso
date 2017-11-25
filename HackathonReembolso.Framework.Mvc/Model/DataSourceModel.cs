using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonReembolso.Framework.Mvc.Model
{
    public class DataSourceModel
    {
        public virtual string CdDataSource { get; set; }
        public virtual List<DataSourceItemModel> Items { get; set; }
    }
}
