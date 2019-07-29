using ClosedXML.Excel;
using ManheimData.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ManheimData
{
    public class ToExcel
    {
        XLWorkbook workbook;
        public ToExcel()
        {
            workbook = new XLWorkbook();
        }

        public void SalesData(string tableName, string legend, List<SalesResponse> salesData)
        {
            DataTable _table = new DataTable
            {
                TableName = tableName
            };

            _table.Columns.Add("vehicleCount", typeof(int));
            _table.Columns.Add("saleDate", typeof(string));
            _table.Columns.Add("uniqueId", typeof(string));
            _table.Columns.Add("auctionId", typeof(string));
            _table.Columns.Add("auctionName", typeof(string));
            _table.Columns.Add("laneNumber", typeof(string));
            _table.Columns.Add("saleYear", typeof(string));
            _table.Columns.Add("eventSaleName", typeof(string));
            _table.Columns.Add("consignor", typeof(string));
            _table.Columns.Add("saleDateTime", typeof(string));
            _table.Columns.Add("channel", typeof(string));
            _table.Columns.Add("uniqueIds", typeof(string));

            foreach (var item in salesData)
            {
                _table.Rows.Add
                (
                    item.vehicleCount,
                    item.saleDate,
                    item.uniqueId,
                    item.auctionId,
                    item.auctionName,
                    item.laneNumber,
                    item.saleYear,
                    item.eventSaleName,
                    item.consignor,
                    item.saleDateTime,
                    item.channel,
                    item.uniqueIds == null ? "" : string.Join(", ", item.uniqueIds)
                 );
            }

            DataTable table = _table;
            var ws = workbook.Worksheets.Add(table);

            //add rows
            ws.Range("A1:L1").InsertRowsAbove(2);

            //add legends
            var totalCell = ws.Cell("A1");
            totalCell.Value = "Total rows: " + salesData.Count.ToString();

            var legendCell = ws.Cell("A2");
            legendCell.Value = legend;
        }

        public void EventsData(string tableName, string legend, List<EventModel> eventsData)
        {
            DataTable _table = new DataTable
            {
                TableName = tableName
            };

            _table.Columns.Add("Id", typeof(string));
            _table.Columns.Add("Name", typeof(string));
            _table.Columns.Add("HasVehicles", typeof(string));
            _table.Columns.Add("ExternalIds", typeof(string));
            _table.Columns.Add("SaleDate", typeof(string));
            _table.Columns.Add("SaleYear", typeof(string));
            _table.Columns.Add("TimeStamp", typeof(string));
            _table.Columns.Add("Type", typeof(string));
            _table.Columns.Add("ZipCode", typeof(string));

            foreach (var item in eventsData)
            {
                _table.Rows.Add
                (
                    item.Id,
                    item.Name,
                    item.HasVehicles,
                    item.ExternalIds == null ? "" : string.Join(", ", item.ExternalIds),
                    item.SaleDate,
                    item.SaleYear,
                    item.TimeStamp,
                    item.Type,
                    item.ZipCode
                 );
            }

            DataTable table = _table;
            var ws = workbook.Worksheets.Add(table);

            //add rows
            ws.Range("A1:L1").InsertRowsAbove(2);

            //add legends
            var totalCell = ws.Cell("A1");
            totalCell.Value = "Total rows: " + eventsData.Count.ToString();

            var legendCell = ws.Cell("A2");
            legendCell.Value = legend;
        }


        public void ListingsData(string tableName, string legend, List<ListingsResponse> ListingsData)
        {
            DataTable _table = new DataTable
            {
                TableName = tableName
            };

            _table.Columns.Add("uniqueId", typeof(string));
            _table.Columns.Add("adjMmr", typeof(double));
            _table.Columns.Add("vin", typeof(string));
            _table.Columns.Add("updateTimestamp", typeof(string));
            _table.Columns.Add("auctionId", typeof(string));
            _table.Columns.Add("auctionStartDate", typeof(string));
            _table.Columns.Add("auctionEndDate", typeof(string));
            _table.Columns.Add("auctionLocation", typeof(string));
            //_table.Columns.Add("channel", typeof(string));
            _table.Columns.Add("laneNumber", typeof(string));
            _table.Columns.Add("runNumber", typeof(string));
            _table.Columns.Add("saleDate", typeof(string));
            _table.Columns.Add("saleYear", typeof(string));
            //_table.Columns.Add("vehicleSaleURL", typeof(string));
            _table.Columns.Add("sellerName", typeof(string));

            //_table.Columns.Add("asIs", typeof(string));
            //_table.Columns.Add("bodyStyle", typeof(string));
            _table.Columns.Add("buyerGroupId", typeof(string));
            _table.Columns.Add("buyNowPrice", typeof(string));
            _table.Columns.Add("certified", typeof(string));
            _table.Columns.Add("comments", typeof(string));
            _table.Columns.Add("conditionGradeNumDecimal", typeof(string));
            _table.Columns.Add("currentBidPrice", typeof(string));
            _table.Columns.Add("doorCount", typeof(string));
            _table.Columns.Add("engine", typeof(string));
            _table.Columns.Add("extendedModel", typeof(string));
            _table.Columns.Add("exteriorColor", typeof(string));
            _table.Columns.Add("frameDamage", typeof(string));
            _table.Columns.Add("fuelType", typeof(string));
            _table.Columns.Add("hasAirConditioning", typeof(string));
            _table.Columns.Add("hasEcr", typeof(string));
            _table.Columns.Add("locationZip", typeof(string));
            _table.Columns.Add("make", typeof(string));
            _table.Columns.Add("mileage", typeof(string));
            //_table.Columns.Add("mobileVdpURL", typeof(string));
            _table.Columns.Add("model", typeof(string));
            _table.Columns.Add("odometerUnits", typeof(string));
            //_table.Columns.Add("offsiteFlag", typeof(string));
            //_table.Columns.Add("pickupLocation", typeof(string));
            //_table.Columns.Add("pickupLocationState", typeof(string));
            //_table.Columns.Add("pickupLocationZip", typeof(string));
            //_table.Columns.Add("pickupRegion", typeof(string));
            //_table.Columns.Add("priorPaint", typeof(string));
            //_table.Columns.Add("salvage", typeof(string));
            //_table.Columns.Add("sellerTypes", typeof(string));
            //_table.Columns.Add("titleState", typeof(string));
            //_table.Columns.Add("titleStatus", typeof(string));
            //_table.Columns.Add("transmission", typeof(string));
            //_table.Columns.Add("trim", typeof(string));
            //_table.Columns.Add("typeCode", typeof(string));
            //_table.Columns.Add("vdpURL", typeof(string));
            //_table.Columns.Add("vehicleOptions", typeof(string));
            _table.Columns.Add("year", typeof(string));

            foreach (var item in ListingsData)
            {
                _table.Rows.Add
                (
                    item.SaleInformation.uniqueId,
                    item.VehicleInformation.adjMmr,
                    item.VehicleInformation.vin,
                    item.VehicleInformation.updateTimestamp,
                    item.SaleInformation.auctionId,
                    item.SaleInformation.auctionStartDate,
                    item.SaleInformation.auctionEndDate,
                    item.SaleInformation.auctionLocation,
                    //item.SaleInformation.channel,
                    item.SaleInformation.laneNumber,
                    item.SaleInformation.runNumber,
                    item.SaleInformation.saleDate,
                    item.SaleInformation.saleYear,
                    //item.SaleInformation.vehicleSaleURL,
                    item.SellerInformation.sellerName,


                    //item.VehicleInformation.asIs,
                    //item.VehicleInformation.bodyStyle,
                    item.VehicleInformation.buyerGroupId,
                    item.VehicleInformation.buyNowPrice,
                    item.VehicleInformation.certified,
                    item.VehicleInformation.comments,
                    item.VehicleInformation.conditionGradeNumDecimal,
                    item.VehicleInformation.currentBidPrice,

                    item.VehicleInformation.doorCount,
                    item.VehicleInformation.engine,
                    item.VehicleInformation.extendedModel,
                    item.VehicleInformation.exteriorColor,
                    item.VehicleInformation.frameDamage,
                    item.VehicleInformation.fuelType,
                    item.VehicleInformation.hasAirConditioning,
                    item.VehicleInformation.hasEcr,
                    //item.VehicleInformation.images,
                    item.VehicleInformation.locationZip,
                    item.VehicleInformation.make,
                    item.VehicleInformation.mileage,
                    //item.VehicleInformation.mobileVdpURL,
                    item.VehicleInformation.model,
                    item.VehicleInformation.odometerUnits,
                    //item.VehicleInformation.offsiteFlag,
                    //item.VehicleInformation.pickupLocation,
                    //item.VehicleInformation.pickupLocationState,
                    //item.VehicleInformation.pickupLocationZip,
                    //item.VehicleInformation.pickupRegion,
                    //item.VehicleInformation.priorPaint,
                    //item.VehicleInformation.salvage,
                    //item.VehicleInformation.sellerTypes == null ? "" : string.Join(", ", item.VehicleInformation.sellerTypes),
                    //item.VehicleInformation.titleState,
                    //item.VehicleInformation.titleStatus,
                    //item.VehicleInformation.transmission,
                    //item.VehicleInformation.trim,
                    //item.VehicleInformation.typeCode,
                    //item.VehicleInformation.vdpURL,
                    //item.VehicleInformation.vehicleOptions == null ? "" : string.Join(", ", item.VehicleInformation.vehicleOptions),
                    item.VehicleInformation.year
                 );
            }

            DataTable table = _table;
            var ws = workbook.Worksheets.Add(table);
            //ws.Sort("vin, D Desc");

            //add rows
            ws.Range("A1:BA1").InsertRowsAbove(2);

            //add legends
            var totalCell = ws.Cell("A1");
            totalCell.Value = "Total rows: " + ListingsData.Count.ToString();

            var legendCell = ws.Cell("A2");
            legendCell.Value = legend;
        }

        public void VehiclesData(string tableName, string legend, List<VehicleModel> vehicleData)
        {
            DataTable _table = new DataTable
            {
                TableName = tableName
            };

            _table.Columns.Add("Event", typeof(string));
            _table.Columns.Add("EventName", typeof(string));
            _table.Columns.Add("ExternalId", typeof(string));
            _table.Columns.Add("UpdateTime", typeof(string));
            _table.Columns.Add("Vin", typeof(string));
            _table.Columns.Add("AuctionInsurance", typeof(string));
            _table.Columns.Add("AutoCheckPass", typeof(string));
            _table.Columns.Add("AvailableForPickUp", typeof(string));
            _table.Columns.Add("BasePrice", typeof(string));
            _table.Columns.Add("BodyStyle", typeof(string));
            _table.Columns.Add("BronzePrice", typeof(string));
            _table.Columns.Add("Bucket", typeof(string));
            _table.Columns.Add("BuyingFees", typeof(string));
            _table.Columns.Add("Color", typeof(string));
            _table.Columns.Add("CrGrade", typeof(string));
            _table.Columns.Add("Doors", typeof(string));
            _table.Columns.Add("EngineType", typeof(string));
            _table.Columns.Add("FinalBidAmount", typeof(string));
            _table.Columns.Add("Grade", typeof(string));
            //_table.Columns.Add("ImageUri", typeof(string));
            _table.Columns.Add("InspectionDeatLine", typeof(string));
            //_table.Columns.Add("InspectionNotes", typeof(string));
            _table.Columns.Add("IntentedBidPrice", typeof(string));
            _table.Columns.Add("IsPurchased", typeof(string));
            _table.Columns.Add("LaneNumber", typeof(string));
            _table.Columns.Add("Location", typeof(string));
            //_table.Columns.Add("LossReasons", typeof(string));
            _table.Columns.Add("Make", typeof(string));
            _table.Columns.Add("Miles", typeof(string));
            _table.Columns.Add("MiscellaneousFees", typeof(string));
            _table.Columns.Add("Mmr", typeof(string));
            _table.Columns.Add("Model", typeof(string));
            //_table.Columns.Add("Options", typeof(string));
            _table.Columns.Add("PostSaleInspection", typeof(string));
            _table.Columns.Add("PurchaseAccountNumber", typeof(string));
            _table.Columns.Add("PurchasePrice", typeof(string));
            _table.Columns.Add("PurchaseVendorNumber", typeof(string));
            //_table.Columns.Add("RejectedReasons", typeof(string));
            _table.Columns.Add("RunNumber", typeof(string));
            _table.Columns.Add("Seller", typeof(string));
            _table.Columns.Add("SentDiscover", typeof(string));
            _table.Columns.Add("SilverPrice", typeof(string));
            _table.Columns.Add("Source", typeof(string));
            _table.Columns.Add("Status", typeof(string));
            _table.Columns.Add("Store", typeof(string));
            _table.Columns.Add("TimeStamp", typeof(string));
            _table.Columns.Add("TotalPrice", typeof(string));
            _table.Columns.Add("Transmission", typeof(string));
            _table.Columns.Add("Trim", typeof(string));
            _table.Columns.Add("WinBid", typeof(string));
            _table.Columns.Add("WinBidAmount", typeof(string));
            _table.Columns.Add("Year", typeof(string));

            foreach (var item in vehicleData)
            {
                _table.Rows.Add
                (
                    item.Event,
                    item.EventName,
                    item.ExternalId,
                    item.UpdateTime,
                    item.Vin,
                    item.AuctionInsurance,
                    item.AutoCheckPass,
                    item.AvailableForPickUp,
                    item.BasePrice,
                    item.BodyStyle,
                    item.BronzePrice,
                    item.Bucket,
                    item.BuyingFees,
                    item.Color,
                    item.CrGrade,
                    item.Doors,
                    item.EngineType,
                    item.FinalBidAmount,
                    item.Grade,
                    //item.ImageUri,
                    item.InspectionDeatLine,
                    //item.InspectionNotes,
                    item.IntentedBidPrice,
                    item.IsPurchased,
                    item.LaneNumber,
                    item.Location,
                    //item.LossReasons,
                    item.Make,
                    item.Miles,
                    item.MiscellaneousFees,
                    item.Mmr,
                    item.Model,
                    //item.Options,
                    item.PostSaleInspection,
                    item.PurchaseAccountNumber,
                    item.PurchasePrice,
                    item.PurchaseVendorNumber,
                    //item.RejectedReasons,
                    item.RunNumber,
                    item.Seller,
                    item.SentDiscover,
                    item.SilverPrice,
                    item.Source,
                    item.Status,
                    item.Store,
                    item.TimeStamp,
                    item.TotalPrice,
                    item.Transmission,
                    item.Trim,
                    item.WinBid,
                    item.WinBidAmount,
                    item.Year
                 );
            }

            DataTable table = _table;
            var ws = workbook.Worksheets.Add(table);
            //ws.Sort("A");

            //add rows
            ws.Range("A1:AV1").InsertRowsAbove(2);

            //add legends
            var totalCell = ws.Cell("A1");
            totalCell.Value = "Total rows: " + vehicleData.Count.ToString();

            var legendCell = ws.Cell("A2");
            legendCell.Value = legend;
        }

        public void Save(string path)
        {
            string folder = Environment.GetEnvironmentVariable("FILES_FOLDER");
            if (folder == null)
            {
                folder = "./";
            }
            workbook.SaveAs(folder + path);
            workbook.Dispose();
        }
    }
}
