using Braintree;

namespace Vanguard.Services.Interfaces;

public interface IBraintreeService
{
    IBraintreeGateway CreateGateway();
    IBraintreeGateway GetGateway();
}
