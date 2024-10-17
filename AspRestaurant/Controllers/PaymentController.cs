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
                Amount = (long)(payAmount * 100),
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
        [HttpGet("userId")]
        public async Task<IActionResult> GetPaymentHistory([FromQuery] string id)
        {   var res = await _paymentRepository.GetPaymentHistoryAsync(id);
            return Ok(res);
        }

        //Getll all item pay info
        [HttpGet("allitems/userId")]
        public async Task<IActionResult> GetBuyItems([FromQuery] string id)
        {
            var res = await _paymentRepository.GetBuyMenuAsync(id);
            return Ok(res);
        }


        

    }
}
