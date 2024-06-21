using Braintree;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Services.Interfaces;

namespace Vanguard.Controllers
{
    public class CheckOutController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IBraintreeService _brain;

        public CheckOutController(IBraintreeService brain)
        {
            _brain = brain;
        }

        public IActionResult Index()
        {
            var gateway = _brain.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;
            return View();
        }

        

        [HttpPost]
        public IActionResult Index(IFormCollection collection)
        {
            Random rnd = new Random();
            string nonceFormtheClient = collection["payment_method_nonce"];
            var request = new TransactionRequest
            {
                Amount = rnd.Next(1,100),
                PaymentMethodNonce = nonceFormtheClient,
                OrderId = "55501",
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                }
            };

            var gatway = _brain.GetGateway();
            Result<Transaction> result = gatway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                var transaction = result.Target;
                TempData["Success"] = "Transaction was successful. Transaction ID: " + transaction.Id + ", Amount: $" + transaction.Amount;
            }
            else
            {
                TempData["Error"] = "Transaction failed: " + result.Message;
            }

            return RedirectToAction("Index");
        }

    }
}
