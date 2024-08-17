using AspRestaurant.Data;
using AspRestaurant.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspRestaurant.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly AspRestaurantContext _context;

        public CardRepository(AspRestaurantContext context)
        {
            _context = context;
        }

        //Get all Cards
        public async Task<List<Card>> GetCardsAsync()
        {
            var cards = await _context.Cards.Select(card => new Card()
            {
                Id = card.Id,
                UserId = card.UserId,
                MenuId = card.MenuId,
                User = card.User,
                Menu = card.Menu,
            }).ToListAsync();
            return cards;

        }

        //GetCardsByUser
        public async Task<List<Card>> GetCardByUserAsync(string userId)
        {
            var cards = await _context.Cards.Where( x => x.UserId == userId).Select(card => new Card()
            {
                Id = card.Id,
                UserId = card.UserId,
                MenuId = card.MenuId,
                User = card.User,
                Menu = card.Menu,
            }).ToListAsync();

            return cards;
        }

        //AddCard 
        public async Task<Card> AddCardAsync(CardModel cardModel)
        {
            var newCard = new Card()
            {
                UserId = cardModel.UserId,
                MenuId= cardModel.MenuId,
            };

            _context.Cards.Add(newCard);
            await _context.SaveChangesAsync();
            return newCard;

        }

        //Delete Cards by userId
        public async Task<object> DeleteCardsAsync(string userId)
        {
            var deleteCards = await _context.Cards.Where(x => x.UserId == userId).ToListAsync();
            _context.Cards.RemoveRange(deleteCards);
            await _context.SaveChangesAsync();
            return new{DeletedCount = deleteCards.Count };
        }

        //Delete Card by cardid
        public async Task<object> DeleteCardByIdAsync(int cardId)
        {
            var deleteCard = new Card()
            {
                Id= cardId,
            };

            _context.Cards.Remove(deleteCard);
            await _context.SaveChangesAsync();
            return new { deletedCount = true };
        }


    }
}
