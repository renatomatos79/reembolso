﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonReembolso.Framework.Mvc.Interfaces
{
    public interface IAddButton : IHtmlElement
    {
        string Databind { get; set; }

        string Text { get; set; }
    }
}
