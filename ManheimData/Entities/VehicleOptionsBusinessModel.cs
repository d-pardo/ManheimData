using System.Collections.Generic;

namespace ManheimData.Entities
{
    public class VehicleOptionsBusinessModel
    {
        public string TransmisionType { get; set; }
        public string TrimLevel { get; set; }//VinResults
        public int Doors { get; set; }//VinResults
        public string BodyStyle { get; set; }//VinResults
        public List<VehicleOptionDetailModel> VehicleOptions { get; set; }//Vehicle Options
        public string Mileage { get; set; }//Jdb API does not return
        public string Color { get; set; }//Jdb API does not return
    }
}