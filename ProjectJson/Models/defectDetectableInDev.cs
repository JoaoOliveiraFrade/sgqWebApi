﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class defectDetectableInDev
    {
        public string date { get; set; }
        public string devManuf { get; set; }
        public string system { get; set; }
        public string project { get; set; }
        public string subproject { get; set; }
        public string delivery { get; set; }
        public int qtyTotal { get; set; }
        public int qtyDetectable { get; set; }
        public double percentReference { get; set; }
        public double qtyReference { get; set; }
    }
}