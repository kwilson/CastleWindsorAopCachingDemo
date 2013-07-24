namespace Tripsis.AopDemo.Services.Linq.JSON
{
    /// <summary>
    /// Model for a stock price reader result.
    /// </summary>
    public class ReaderResult
    {
        /// <summary>
        /// Gets or sets the stock name.
        /// </summary>
        /// <value>
        /// The stock name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        /// <value>
        /// The stock symbol.
        /// </value>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the last price of the stock.
        /// </summary>
        /// <value>
        /// The last price of the stock.
        /// </value>
        public decimal LastPrice { get; set; }
    }
}
