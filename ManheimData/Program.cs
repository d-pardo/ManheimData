using AutoMapper;
using ManheimData.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ManheimData
{
    class Program
    {
        private static IMapper _mapper;
        private static List<EventModel> _savedEvents;

        static void Main(string[] args)
        {
            _mapper = Mapper.CreateMapper();

            RetrieveData();
        }

        public static void RetrieveData()
        {
            Console.WriteLine("ManheimAPI -> RetrieveData -> sales: Start");
            RetrieveSales();
            Console.WriteLine("ManheimAPI -> RetrieveData -> sales: End");

            Console.WriteLine("ManheimAPI -> RetrieveData -> listings: Start");
            RetrieveListings();
            Console.WriteLine("ManheimAPI -> RetrieveData -> listings: End");
        }

        protected static void RetrieveSales()
        {
            ToExcel _eventstoExcel = new ToExcel();
            try
            {
                var pageSize = 5000;
                var responseAPIManheim = "";
                var numberOfPages = 0.0;

                var currentPage = 0;
                do
                {
                    currentPage++;
                    Console.WriteLine($"RetrieveSales Page:{currentPage} of {numberOfPages}");

                    try
                    {
                        responseAPIManheim = GetConsumeAPI("isws-basic/sales", "SALE_END_DATETIME", pageSize, currentPage);
                        if (string.IsNullOrEmpty(responseAPIManheim))
                        {
                            Console.WriteLine("Variable responseAPIManheim (sales) retrieved null/empty for page ${currentPage}");
                            continue;
                        }

                        if (!string.IsNullOrEmpty(responseAPIManheim))
                        {
                            if (numberOfPages == 0.0)
                            {
                                var responseSales = JObject.Parse(responseAPIManheim);
                                var totalSales = (double)responseSales["totalSales"];
                                numberOfPages = Math.Ceiling(totalSales / pageSize);
                            }

                            var startCurrentDate = DateUtils.CurrentDate()["startCurrentDate"];
                            var endCurrentDate = DateUtils.CurrentDate()["endCurrentDate"];

                            var response = JsonConvert.DeserializeObject<ManheimResponse>(responseAPIManheim);
                            if (response == null || response.sales == null)
                            {
                                Console.WriteLine("Response from Manheim is empty (sales) for page ${currentPage}");
                                continue;
                            }

                            //ToExcel
                            _eventstoExcel.SalesData("AllSales", "", response.sales);

                            var responseEvents = response?.sales
                                .Where(x => !string.IsNullOrEmpty(x.saleDateTime))
                                .Where(sales => Convert.ToDateTime(sales.saleDateTime) >= startCurrentDate && Convert.ToDateTime(sales.saleDateTime) <= endCurrentDate)
                                .Where(x => !string.IsNullOrEmpty(x.uniqueId))
                                .ToList();

                            //ToExcel
                            _eventstoExcel.SalesData("Filtered", "Filter: no empty saleDateTime, only events in current mont, no empty uniqueId", responseEvents);

                            //group by name & date of event
                            var responseEventsMapped = responseEvents
                            .GroupBy(x => new { x.auctionName, x.saleDateTime })
                            .Select(g => new SalesResponse
                            {
                                auctionName = g.FirstOrDefault().auctionName,
                                eventSaleName = g.FirstOrDefault().eventSaleName,
                                saleDate = g.FirstOrDefault().saleDate,
                                saleDateTime = g.FirstOrDefault().saleDateTime,
                                saleYear = g.FirstOrDefault().saleYear,
                                uniqueIds = g.Select(x => x.uniqueId).Distinct().ToList()
                            })
                            //.Select(sr => _mapper.Map<EventModel>(sr)) // mapper comented just to have the same column names in excel file
                            .ToList();

                            //ToExcel
                            _eventstoExcel.SalesData("Grouped", "Grouped by auctionName&saleDateTime", responseEventsMapped);

                            //save result into variable to use with vehicles
                            _savedEvents = responseEventsMapped.Select(sr => _mapper.Map<EventModel>(sr)).ToList();
                            _eventstoExcel.EventsData("mappedEvents", "", _savedEvents);

                            //await _eventsService.PutItems(responseEventsMapped);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                } while (currentPage < numberOfPages);

                _eventstoExcel.Save("./1 manheimEvents.xlsx");

                Console.WriteLine("End events...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("End with exeption");
                Console.ReadLine();
            }
        }

        protected static void RetrieveListings()
        {
            ToExcel _allListingsToExcel = new ToExcel();
            ToExcel _filteredListingsToExcel = new ToExcel();
            ToExcel _groupedVehiclesToExcel = new ToExcel();
            ToExcel _vehiclesWithEventsToExcel = new ToExcel();

            var pageSize = 5000;
            var responseAPIManheim = "";
            var numberOfPages = 0.0;

            var currentPage = 0;
            do
            {
                currentPage++;
                Console.WriteLine($"RetrieveListings Page:{currentPage} of {numberOfPages}");

                //Retrieve API response
                try
                {
                    responseAPIManheim = GetConsumeAPI("isws-basic/listings", "UPDATE_TIMESTAMP", pageSize, currentPage);
                    if (string.IsNullOrEmpty(responseAPIManheim))
                    {
                        Console.WriteLine($"Variable responseAPIManheim retrieved null/empty for page: {currentPage}");
                        continue;
                    }

                    if (numberOfPages == 0.0)
                    {
                        var responseSales = JObject.Parse(responseAPIManheim);
                        var totalListings = (double)responseSales["totalListings"];
                        numberOfPages = Math.Ceiling(totalListings / pageSize);
                    }

                    var response = JsonConvert.DeserializeObject<ManheimOpenListingsResponse>(responseAPIManheim);
                    if (response == null || response.listings == null)
                    {
                        Console.WriteLine($"Response from Manheim is empty (listings) for page {currentPage}");
                        continue;
                    }

                    //ToExcel
                    _allListingsToExcel.ListingsData("AllListings_" + currentPage, "", response.listings);

                    //Filter valid records
                    var responseListing = response?.listings
                        .Where(x => (x.VehicleInformation.adjMmr > 0.0 && x.VehicleInformation.adjMmr <= 20000.0))
                        .Where(x => !string.IsNullOrEmpty(x.SaleInformation.uniqueId))
                        .Where(x => !string.IsNullOrEmpty(x.VehicleInformation.vin) && FieldsValidation.IsAlphanumeric(x.VehicleInformation.vin))
                        .Where(x => !string.IsNullOrEmpty(x.VehicleInformation.updateTimestamp)
                            && DateTime.TryParseExact(
                                x.VehicleInformation.updateTimestamp,
                                "yyyyMMddHHmmss",
                                null,
                                DateTimeStyles.None,
                                out DateTime parsedDate)
                            )
                        .ToList();

                    //ToExcel
                    _filteredListingsToExcel.ListingsData("Filtered_" + currentPage, "Filter: adjMmr(>0&<=20000), no empty uniqueId, no empty vin&IsAlphanumeric, no invalid updateTimestamp", responseListing);

                    if (!responseListing.Any())
                    {
                        continue;
                    }

                    //Map data and take the newest record from a group of Vins
                    var vehicleResponseMapped = responseListing
                        .Select(sr => _mapper.Map<VehicleModel>(sr))
                        .GroupBy(x => x.Vin)
                        .Select(
                            x => x.OrderByDescending(y => y.UpdateTime)
                                .FirstOrDefault()
                        )
                        .ToList();
                    //ToExcel
                    _groupedVehiclesToExcel.VehiclesData("Grouped_"+currentPage, "Group: by VIN, sortedDesc by UpdateTime and take firts", vehicleResponseMapped);

                    //Filter Vehicles that are related with an Event
                    List<VehicleModel> vehicleWithEvent = SyncUtils.RelateVehiclesWithEvents(vehicleResponseMapped, _savedEvents);
                    //ToExcel
                    _vehiclesWithEventsToExcel.VehiclesData("WithEvents_" + currentPage, "", vehicleWithEvent);

                    if (vehicleWithEvent.Any())
                    {
                        //await _vehicleService.PutItems(vehicleWithEvent);
                    }
                }
               catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            } while (currentPage < numberOfPages);

            Console.WriteLine("Saving manheimAllListings");
            _allListingsToExcel.Save("./2 manheimAllListings.xlsx");

            Console.WriteLine("Saving manheimDataFiltered");
            _filteredListingsToExcel.Save("./3 manheimDataFiltered.xlsx");

            Console.WriteLine("Saving manheimDataGrouped");
            _groupedVehiclesToExcel.Save("./4 manheimDataGrouped.xlsx");

            Console.WriteLine("Saving manheimDataWithEvents");
            _vehiclesWithEventsToExcel.Save("./5 manheimDataWithEvents.xlsx");

            Console.WriteLine("End vehicles...");
            Console.ReadKey();
        }

        protected static string GetConsumeAPI(string resource, string sort, int pageSize, int pageNumber = 1)
        {
            var endPoint = "https://api.manheim.com";
            var client = new RestClient(endPoint);
            var request = new RestRequest(resource, Method.POST);
            request.AddParameter("api_key", "7x5f8eg8fu6rtaaw5dqrqqrx");
            request.AddParameter("sort", sort);
            request.AddParameter("sortDirection", "forward");
            request.AddParameter("pageSize", pageSize);
            request.AddParameter("pageNumber", pageNumber);
            var response = client.Execute(request);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                Console.WriteLine("Manheim error: " + response.ErrorMessage);
            }

            if (response.ErrorException != null)
            {
                Console.WriteLine("Manheim error: " + response.ErrorException.ToString());
            }

            return response.IsSuccessful ? response.Content : null;
        }
    }
}
