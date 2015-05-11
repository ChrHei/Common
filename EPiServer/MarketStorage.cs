using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests.EPiServer
{
    public class MarketStorage : ICurrentMarket
    {
        [ThreadStatic]
        private static MarketId currentMarketId;
        
        private IMarketService marketService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketStorage"/> class.
        /// </summary>
        public MarketStorage()
            : this(ServiceLocator.Current.GetInstance<IMarketService>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketStorage"/> class.
        /// </summary>
        /// <param name="marketService">The market service.</param>
        public MarketStorage(IMarketService marketService)
        {
            this.marketService = marketService;
        }

        public IMarket GetCurrentMarket()
        {
            if (currentMarketId == null)
                currentMarketId = MarketId.Default;

            return marketService.GetMarket(currentMarketId);
        }

        public void SetCurrentMarket(MarketId marketId)
        {
            currentMarketId = marketId;
        }
    }
}
