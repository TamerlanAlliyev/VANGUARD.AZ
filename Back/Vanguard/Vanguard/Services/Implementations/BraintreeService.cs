using Braintree;
using Microsoft.Extensions.Options;
using Vanguard.Helpers;
using Vanguard.Services.Interfaces;

namespace Vanguard.Services.Implementations
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IConfiguration _config;
        public BrainTreeSettings Options { get; set; }
        private IBraintreeGateway BraintreeGateway { get; set; }
        public BraintreeService(IOptions<BrainTreeSettings> options)
        {
            Options = options.Value;
        }

        public IBraintreeGateway CreateGateway()
        {
            return new BraintreeGateway(Options.Environment, Options.MerchantId, Options.PublicKey, Options.PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if (BraintreeGateway == null)
            {
                BraintreeGateway = CreateGateway();
            }
            return BraintreeGateway;
        }
    }
}
