using System;
using System.Collections.Generic;

namespace ProjectWebApi.Models
{
    public class TrgFilter
    {
        public Release release { get; set; }
        public IList<string> systems { get; set; }
    }
}