using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICareAlz.Helpers
{
    public class Response
    {

        public bool Successfully { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

    }
}