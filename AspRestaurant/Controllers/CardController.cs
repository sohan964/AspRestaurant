using AspRestaurant.Models;
using AspRestaurant.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;

        public CardController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _cardRepository.GetCardsAsync();

            return Ok(cards);
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetCardsByUserId([FromQuery] string id)
        {
            var cards = await _cardRepository.GetCardByUserAsync(id);
            return Ok(cards);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewCard([FromBody] CardModel cardModel)
        {
            var addCard = await _cardRepository.AddCardAsync(cardModel);
            return Ok(addCard);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteCards([FromRoute] string userId)
        {
            var deleteCards = await _cardRepository.DeleteCardsAsync(userId);
            return Ok(deleteCards);
        }

        [HttpDelete("Delete/{cardId}")]
        public async Task<IActionResult> DeleteCardById([FromRoute] int cardId)
        {
            var deleteCard = await _cardRepository.DeleteCardByIdAsync(cardId);
            return Ok(deleteCard);
        }
    }
}
