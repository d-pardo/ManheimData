using System;
using System.Collections.Generic;
using System.Text;

namespace ManheimData.Entities
{
    public class SalesResponse
    {
        public int vehicleCount { get; set; }
        public string saleDate { get; set; }
        public string uniqueId { get; set; }
        public List<string> uniqueIds { get; set; }
        public string auctionId { get; set; }
        public string auctionName { get; set; }
        public string laneNumber { get; set; }
        public string saleYear { get; set; }
        public string eventSaleName { get; set; }
        public string consignor { get; set; }
        public string saleDateTime { get; set; }
        public string channel { get; set; }
    }
}
