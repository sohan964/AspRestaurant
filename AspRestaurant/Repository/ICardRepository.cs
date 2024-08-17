using AspRestaurant.Data;
using AspRestaurant.Models;

namespace AspRestaurant.Repository
{
    public interface ICardRepository
    {
        Task<List<Card>> GetCardsAsync();
        Task<List<Card>> GetCardByUserAsync(string userId);
        Task<Card> AddCardAsync(CardModel cardModel);
        Task<object> DeleteCardsAsync(string userId);
        Task<object> DeleteCardByIdAsync(int cardId);
    }
}
