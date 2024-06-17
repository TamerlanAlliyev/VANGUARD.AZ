using Braintree;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Services.Interfaces;
using Vanguard.ViewModels.Book;

namespace Vanguard.Controller;

public class PaymentController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IBraintreeService _braintreeService;

    public PaymentController(IBraintreeService braintreeService)
    {
        _braintreeService = braintreeService;
    }

    public IActionResult Index()
    {
        var gateway = _braintreeService.GetGateway();
        var clientToken = gateway.ClientToken.Generate();  //Genarate a token
        ViewBag.ClientToken = clientToken;

        var data = new BookPurchaseVM
        {
            Id = 2,
            Description = "Hellow man",
            Author = "Me",
            Thumbnail = "This is thumbnail",
            Title = "This is title",
            Price = "230",
            Nonce = ""
        };

        return View(data);
    }


    [HttpPost]
    public IActionResult Create(BookPurchaseVM model)
    {
        var gateway = _braintreeService.GetGateway();
        var request = new TransactionRequest
        {
            Amount = Convert.ToDecimal("250"),
            PaymentMethodNonce = model.Nonce,
            Options = new TransactionOptionsRequest
            {
                SubmitForSettlement = true
            }
        };

        Result<Transaction> result = gateway.Transaction.Sale(request);

        if (result.IsSuccess())
        {
            return View("Success");
        }
        else
        {
            return View("Failure");
        }
    }
}

