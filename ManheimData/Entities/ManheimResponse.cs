using System;
using System.Collections.Generic;
using System.Text;

namespace ManheimData.Entities
{
    public class ManheimResponse
    {
        public List<SalesResponse> sales { get; set; }
        public string totalSales { get; set; }
    }
}
