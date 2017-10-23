﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebApi.Models
{
    public class defectOfTSInTI
    {
        public string month { get; set; }
        public string year { get; set; }
        public string devManuf { get; set; }
        public string system { get; set; }
        public string subprojectDelivery { get; set; }
        public int qtyDetectableInTS { get; set; }
        public int qtyTotal { get; set; }
        public double percDetectableInTS { get; set; }
    }
}