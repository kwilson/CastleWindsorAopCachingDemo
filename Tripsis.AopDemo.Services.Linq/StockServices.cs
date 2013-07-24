namespace Tripsis.AopDemo.Services.Linq
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;

    using Newtonsoft.Json;

    using Tripsis.AopDemo.Services.Linq.JSON;

    /// <summary>
    /// Implementation of <see cref="IStockServices"/>.
    /// </summary>
    public class StockServices : IStockServices
    {
        /// <summary>
        /// Gets the stock reading for the specified stock.
        /// </summary>
        /// <param name="symbol">The stock symbol.</param>
        /// <returns>
        /// An asynchronous stock reading value.
        /// </returns>
        public async Task<StockReading> GetStockReading(string symbol)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
            client.BaseAddress = new Uri("http://dev.markitondemand.com/Api/");

            var query = string.Format("Quote/json?symbol={0}", symbol);
            var response = client.GetAsync(query).Result;

            // If it fails - outputs the request string followed by the Http response
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = string.Format("{0}{1} \n{2}", client.BaseAddress, query, response);
                throw new HttpException(result);
            }

            // If it gets an OK status Outputs the json/xml recieved
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ReaderResultSet>(json);

            if (data == null || data.Data == null)
            {
                return null;
            }

            return new StockReading
                {
                    Name = data.Data.Name,
                    Symbol = data.Data.Symbol,
                    LastPrice = data.Data.LastPrice,
                    TimeStamp = DateTime.Now
                };
        }
    }
}
