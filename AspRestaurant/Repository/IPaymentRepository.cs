using AspRestaurant.Data;
using AspRestaurant.Models;

namespace AspRestaurant.Repository
{
    public interface IPaymentRepository
    {
        Task<object> StorePaymentAsync(PaymentModel payment);
    }
}
