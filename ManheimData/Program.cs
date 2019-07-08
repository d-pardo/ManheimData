using AutoMapper;
using ManheimData.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ManheimData
{
    class Program
    {
        private static IMapper _mapper;
        private static ToExcel _toExcel;

        static void Main(string[] args)
        {
            _mapper = Mapper.CreateMapper();
            _toExcel = new ToExcel();

            RetrieveData();
        }

        public static void RetrieveData()
        {
            Console.WriteLine("ManheimAPI -> RetrieveData -> sales: Start");
            RetrieveSales();
            Console.WriteLine("ManheimAPI -> RetrieveData -> sales: End");

            Console.WriteLine("ManheimAPI -> RetrieveData -> listings: Start");
            //await RetrieveListings();
            Console.WriteLine("ManheimAPI -> RetrieveData -> listings: End");
        }

        protected static void RetrieveSales()
        {
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
                            _toExcel.EventsData("AllData", "", response.sales);

                            var responseEvents = response?.sales
                                .Where(x => !string.IsNullOrEmpty(x.saleDateTime))
                                .Where(sales => Convert.ToDateTime(sales.saleDateTime) >= startCurrentDate && Convert.ToDateTime(sales.saleDateTime) <= endCurrentDate)
                                .Where(x => !string.IsNullOrEmpty(x.uniqueId))
                                .ToList();

                            //ToExcel
                            _toExcel.EventsData("Filtered", "Filter: no empty saleDateTime, only events in current mont, no empty uniqueId", responseEvents);

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
                            _toExcel.EventsData("Grouped", "Grouped by auctionName&saleDateTime", responseEventsMapped);

                            //await _eventsService.PutItems(responseEventsMapped);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                } while (currentPage < numberOfPages);

                _toExcel.Save("./manheimData.xlsx");

                Console.WriteLine("End...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("End with ex...");
                Console.ReadLine();
            }
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
