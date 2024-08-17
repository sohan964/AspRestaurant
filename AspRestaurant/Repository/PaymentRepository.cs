using AspRestaurant.Data;
using AspRestaurant.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AspRestaurant.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AspRestaurantContext _context;

        public PaymentRepository(AspRestaurantContext context)
        {
            _context = context;
        }

        //store Payment
        public async Task<object> StorePaymentAsync(PaymentModel payment)
        {
            var menuIds = payment.ItemsId;
            var newPaymentList = new List<Payment>();
            menuIds.ForEach(menuId =>
            {
                var newPayment = new Payment()
                {
                    UserId = payment.UserId,
                    TransactionId = payment.TransactionId,
                    MenuId = menuId,

                };
                newPaymentList.Add(newPayment);
            });

            var amountDetails = new AmountDetails()
            { 
                UserId = payment.UserId,
                TransactionId = payment.TransactionId,
                Amount = payment.TotalAmount,

            };

            await _context.Payments.AddRangeAsync(newPaymentList);
            await _context.AmountDetails.AddAsync(amountDetails);
            await _context.SaveChangesAsync();

            return new { indertedCount = "success" };

        }
    }
}
