namespace Tripsis.AopDemo.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Tripsis.AopDemo.Services;

    public class HomeController : Controller
    {
        /// <summary>
        /// The stock services
        /// </summary>
        private readonly IStockServices stockServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="stockServices">The stock services.</param>
        public HomeController(IStockServices stockServices)
        {
            this.stockServices = stockServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string symbol)
        {
            if (!string.IsNullOrWhiteSpace(symbol))
            {
                var normalisedSymbol = symbol.ToLower();
                var data = await this.stockServices.GetStockReading(normalisedSymbol);

                if (data != null)
                {
                    return this.View(data);
                }

                // Value wasn't empty but still wasn't found
                TempData["Message"] = string.Format("Sorry, nothing found for '{0}'.", symbol);
            }

            return RedirectToAction("Index");
        }
    }
}
