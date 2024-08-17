using AspRestaurant.Data;
using AspRestaurant.Models;
using AspRestaurant.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AspRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> MakePayment([FromBody] float payAmount)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = (long)payAmount * 100,
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });
            
            return Ok(new {clientSecret = paymentIntent.ClientSecret});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> StorePayment(PaymentModel payment)
        {
            var storedPayment = await _paymentRepository.StorePaymentAsync(payment);
            return Ok(storedPayment);
        }
        
        //Get all  payment info
        //[HttpPost("")]
       // public 



        

    }
}
