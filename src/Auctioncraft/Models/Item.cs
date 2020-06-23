namespace Auctioncraft.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public string SubClass { get; set; }
        public long VendorBuy { get; set; }
        public long VendorSell { get; set; }
        public long MarketValue { get; set; }
        public long MinBuyout { get; set; }
        public int Quantity { get; set; }
        public int NumAuctions { get; set; }
        public long HistoricalPrice { get; set; }
        public long RegionMarketAvg { get; set; }
        public long RegionMinBuyoutAvg { get; set; }
        public int RegionQuantity { get; set; }
        public long RegionHistoricalPrice { get; set; }
        public long RegionSaleAvg { get; set; }
        public double RegionAvgDailySold { get; set; }
        public double RegionSaleRate { get; set; }

    }
}
