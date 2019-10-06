using System;
using System.Collections.Generic;
using System.Text;
using Attiva.Communication.Enums;

namespace Attiva.Communication
{
    public class Error
    {
        public ErrorType Type { get; set; }
        public string Exception { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
    }
}
