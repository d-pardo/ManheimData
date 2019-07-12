using System;
using System.Collections.Generic;
using System.Text;

namespace ManheimData.Entities
{
    public class ManheimOpenListingsResponse
    {
        public List<ListingsResponse> listings { get; set; }
        public string totalListings { get; set; }
    }
}
