using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManheimData.Entities
{
    [DynamoDBTable("event")]
    public class EventModel
    {
        [DynamoDBHashKey]
        [DynamoDBGlobalSecondaryIndexHashKey("timeIndex")]
        [DynamoDBProperty("id")]
        public string Id { get; set; }

        [DynamoDBProperty("external_id")]
        public List<string> ExternalIds { get; set; }

        [DynamoDBProperty("sale_date")]
        public string SaleDate { get; set; }

        [DynamoDBProperty("name")]//auction_name
        public string Name { get; set; }

        [DynamoDBProperty("sale_year")]
        public string SaleYear { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey("timeIndex")]
        [DynamoDBProperty("timestamp")]//sale_date_time
        public long TimeStamp { get; set; }

        [DynamoDBRangeKey]
        [DynamoDBProperty("zip_code")]
        public string ZipCode { get; set; }

        [DynamoDBProperty("type")]
        public string Type { get; set; }

        [DynamoDBProperty("has_vehicles")]
        public int HasVehicles { get; set; }
    }
}
