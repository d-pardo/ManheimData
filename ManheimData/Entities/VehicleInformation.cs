using System.Collections.Generic;

namespace ManheimData.Entities
{
    public class VehicleInformation
    {
        public string typeCode { get; set; }
        public string year { get; set; }
        public string model { get; set; }
        public List<string> sellerTypes { get; set; }
        public string vin { get; set; }
        public double adjMmr { get; set; }
        public string pickupLocation { get; set; }
        public string pickupLocationState { get; set; }
        public string pickupLocationZip { get; set; }
        public string make { get; set; }
        public int mileage { get; set; }
        public bool certified { get; set; }
        public string updateTimestamp { get; set; }
        public bool asIs { get; set; }
        public bool salvage { get; set; }
        public string locationZip { get; set; }
        public string comments { get; set; }
        public string transmission { get; set; }
        public string fuelType { get; set; }
        public string frameDamage { get; set; }
        public string priorPaint { get; set; }
        public string buyerGroupId { get; set; }
        public string pickupRegion { get; set; }
        public bool hasEcr { get; set; }
        public string odometerUnits { get; set; }
        public bool hasAirConditioning { get; set; }
        public string exteriorColor { get; set; }
        public string titleStatus { get; set; }
        public string titleState { get; set; }
        public string extendedModel { get; set; }
        public List<VehicleImage> images { get; set; }
        public string buyNowPrice { get; set; }
        public string currentBidPrice { get; set; }
        public string vdpURL { get; set; }
        public string mobileVdpURL { get; set; }
        public string offsiteFlag { get; set; }
        public string bodyStyle { get; set; }
        public string engine { get; set; }
        public string trim { get; set; }
        public int doorCount { get; set; }
        public double conditionGradeNumDecimal { get; set; }
        public List<string> vehicleOptions { get; set; }

    }
}