namespace Tripsis.AopDemo
{
    using System;

    /// <summary>
    /// Model for a stock value reading.
    /// </summary>
    public class StockReading
    {
        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        /// <value>
        /// The stock symbol.
        /// </value>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the stock name.
        /// </summary>
        /// <value>
        /// The stock name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last price of the stock.
        /// </summary>
        /// <value>
        /// The last price of the stock.
        /// </value>
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Gets or sets the time stamp of the last pricing.
        /// </summary>
        /// <value>
        /// The time stamp of the last pricing.
        /// </value>
        public DateTime TimeStamp { get; set; }
    }
}
