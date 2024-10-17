using AspRestaurant.Data;
using AspRestaurant.Models;

namespace AspRestaurant.Repository
{
    public interface IPaymentRepository
    {
        Task<object> StorePaymentAsync(PaymentModel payment);
        Task<List<AmountDetails>> GetPaymentHistoryAsync(string id);
        Task<List<Payment>> GetBuyMenuAsync(string id);
    }
}
