using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManheimData.Entities
{
    public class VehicleModel
    {

        [DynamoDBHashKey]
        [DynamoDBProperty("vin")]
        public string Vin { get; set; }
        [DynamoDBProperty("year")]
        public int Year { get; set; }
        [DynamoDBProperty("color")]
        public string Color { get; set; }
        [DynamoDBProperty("body_style")]
        public string BodyStyle { get; set; }
        [DynamoDBProperty("engine_type")]
        public string EngineType { get; set; }
        [DynamoDBProperty("miles")]
        public double Miles { get; set; }
        [DynamoDBProperty("model")]
        public string Model { get; set; }
        [DynamoDBProperty("trim")]
        public string Trim { get; set; }
        [DynamoDBProperty("seller")]
        public string Seller { get; set; }
        [DynamoDBProperty("doors")]
        public string Doors { get; set; }
        [DynamoDBProperty("transmission")]
        public string Transmission { get; set; }
        [DynamoDBProperty("make")]
        public string Make { get; set; }
        [DynamoDBProperty("base_price")]
        public double BasePrice { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey("sourceIndex")]
        [DynamoDBProperty("source")]
        public string Source { get; set; }
        [DynamoDBProperty("bronze_price")]
        public double BronzePrice { get; set; }
        [DynamoDBProperty("silver_price")]
        public double SilverPrice { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey("eventIndex")]
        [DynamoDBProperty("event")]
        public string Event { get; set; }
        [DynamoDBProperty("event_name")]
        public string EventName { get; set; }
        [DynamoDBGlobalSecondaryIndexRangeKey("sourceIndex")]
        [DynamoDBProperty("bucket")]
        public string Bucket { get; set; }
        [DynamoDBRangeKey]
        [DynamoDBProperty("timestamp")]
        public long TimeStamp { get; set; }
        [DynamoDBProperty("autocheck_pass")]
        public bool AutoCheckPass { get; set; }
        [DynamoDBProperty("run_number")]
        public int RunNumber { get; set; }
        [DynamoDBProperty("options")]
        public List<VehicleOptionsBusinessModel> Options { get; set; }
        [DynamoDBProperty("inspection_notes")]
        public List<VehicleInspectionNotes> InspectionNotes { get; set; }
        [DynamoDBProperty("update_time")]
        public DateTime UpdateTime { get; set; }
        [DynamoDBProperty("external_id")]
        public string ExternalId { get; set; }
        [DynamoDBProperty("lane_number")]
        public string LaneNumber { get; set; }
        //Members for Lists
        [DynamoDBGlobalSecondaryIndexHashKey("statusIndex")]
        [DynamoDBProperty("status")]
        public string Status { get; set; }
        [DynamoDBProperty("rejected_notes")]
        public List<string> RejectedReasons { get; set; }

        [DynamoDBProperty("intented_bid_price")]
        public double? IntentedBidPrice { get; set; }
        [DynamoDBProperty("final_bid_amount")]
        public double? FinalBidAmount { get; set; }
        [DynamoDBProperty("win_bid")]
        public bool? WinBid { get; set; }
        [DynamoDBProperty("loss_reason")]
        public List<string> LossReasons { get; set; }
        [DynamoDBProperty("win_bid_amount")]
        public double? WinBidAmount { get; set; }

        //Purchase Information
        [DynamoDBProperty("is_purchased")]
        public bool? IsPurchased { get; set; }
        [DynamoDBProperty("sent_discover")]
        public bool? SentDiscover { get; set; }
        [DynamoDBProperty("purchase_price")]
        public double? PurchasePrice { get; set; }
        [DynamoDBProperty("buying_fees")]
        public double? BuyingFees { get; set; }
        [DynamoDBProperty("post_sale_inspection")]
        public double? PostSaleInspection { get; set; }
        [DynamoDBProperty("auction_insurance")]
        public double? AuctionInsurance { get; set; }
        [DynamoDBProperty("miscellaneous_fees")]
        public double? MiscellaneousFees { get; set; }
        [DynamoDBProperty("available_for_pickup")]
        public long AvailableForPickUp { get; set; }//Timestamp date
        [DynamoDBProperty("inspection_dead_line")]
        public long InspectionDeatLine { get; set; }//Timestamp date
        [DynamoDBProperty("store")]
        public string Store { get; set; }
        [DynamoDBProperty("location")]
        public string Location { get; set; }
        [DynamoDBProperty("total_price")]
        public double? TotalPrice { get; set; }
        [DynamoDBProperty("grade")]
        public string Grade { get; set; }
        [DynamoDBProperty("crGrade")]
        public string CrGrade { get; set; }
        [DynamoDBProperty("mmr_price")]
        public double? Mmr { get; set; }
        [DynamoDBProperty("purchase_account_number")]
        public string PurchaseAccountNumber { get; set; }
        [DynamoDBProperty("purchase_vendor_number")]
        public string PurchaseVendorNumber { get; set; }


        [DynamoDBIgnore]
        public string[] ImageUri { get; set; }
    }
}
