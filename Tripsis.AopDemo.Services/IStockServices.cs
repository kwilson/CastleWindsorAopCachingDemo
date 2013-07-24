namespace Tripsis.AopDemo.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for a service to read stock data.
    /// </summary>
    public interface IStockServices
    {
        /// <summary>
        /// Gets the stock reading for the specified stock.
        /// </summary>
        /// <param name="symbol">The stock symbol.</param>
        /// <returns>An asynchronous stock reading value.</returns>
        Task<StockReading> GetStockReading(string symbol);
    }
}
