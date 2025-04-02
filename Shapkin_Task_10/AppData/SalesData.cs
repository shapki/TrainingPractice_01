using System;

namespace Shapkin_Task_10.Classes
{
    public class SalesData
    {
        public int SaleCode { get; set; }
        public DateTime AuctionDate { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal? FinalPrice { get; set; }
        public bool Sold { get; set; }
        public string BuyerLastName { get; set; }
        public string ItemName { get; set; }
    }
}
