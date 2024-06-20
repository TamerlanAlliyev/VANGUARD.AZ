using Braintree;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Services.Interfaces;

namespace Vanguard.Controller;

public class PaymentController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IBraintreeService _braintreeService;

    public PaymentController(IBraintreeService braintreeService)
    {
        _braintreeService = braintreeService;
    }

}

