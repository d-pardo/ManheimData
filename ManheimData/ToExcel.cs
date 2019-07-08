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

        public void EventsData(string tableName, string legend, List<SalesResponse> eventsData)
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

            foreach (var item in eventsData)
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
            totalCell.Value = "Total rows: " + eventsData.Count.ToString();

            var legendCell = ws.Cell("A2");
            legendCell.Value = legend;
        }

        public void Save(string path)
        {
            workbook.SaveAs(path);
        }
    }
}
