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

            return new { insertedCount = "success" };

        }

        public async Task<List<AmountDetails>> GetPaymentHistoryAsync(string id)
        {
            var payDetails = await _context.AmountDetails.Where(x => x.UserId == id).Select(pay => new AmountDetails() {
                Id = pay.Id,
                Amount = pay.Amount,
                UserId = pay.UserId,
                TransactionId=pay.TransactionId,

            }).ToListAsync();

            return payDetails;

        }

        public async Task<List<Payment>> GetBuyMenuAsync(string id)
        {
            var totalPays = await _context.Payments.Where(x => x.UserId == id).Select(pay => new Payment() {
                Id=pay.Id,
                UserId=pay.UserId,
                TransactionId= pay.TransactionId,
                MenuId=pay.MenuId,
                Menu = pay.Menu,
                
            }).ToListAsync();

            return totalPays;

        }
    }
}
