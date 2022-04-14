using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardDao cardDao;

        public CardController(ICardDao _cardDao)
        {
            cardDao = _cardDao;
        }

        [HttpPost]
        public ActionResult<Card> AddCard(Card newCard)
        {
            ActionResult result;

            Card card = cardDao.AddCard(newCard.CardFront, newCard.CardBack, newCard.UserId, newCard.DeckId, newCard.Tags);

            if (card != null)
            {
                result = Created(card.CardFront, card);
            }
            else
            {
                result = BadRequest(new { message = "An error occured and card was not created." });
            }

            return result;
        }

        [HttpGet("{deckId}")]
        public ActionResult<List<Card>> GetCardsInDeck(int deckId)
        {
            ActionResult result;

            List<Card> cards = cardDao.GetCardsInDeck(deckId);

            if (cards != null)
            {
                result = Ok(cards);
            }
            else
            {
                result = BadRequest(new { message = "Unable to retrieve cards." });
            }

            return result;
        }

        [HttpGet("mycards/{userId}")]
        public ActionResult<List<Card>> GetCardsByUser(int userId)
        {
            ActionResult result;

            List<Card> cards = cardDao.GetCardsByUser(userId);

            if (cards != null)
            {
                result = Ok(cards);
            }
            else
            {
                result = BadRequest(new { message = "Unable to retrieve cards." });
            }

            return result;
        }

        [HttpDelete("{deckId}/{cardId}")]
        public ActionResult DeleteCardFromDeck(int cardId, int deckId)
        {
            bool result = cardDao.DeleteCardFromDeck(cardId, deckId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "An error occured. Unable to remove card from deck." });
            }
        }

        [HttpPut]
        public ActionResult<Card> UpdateCard(Card cardToUpdate)
        {
            Card card = cardDao.UpdateCard(cardToUpdate.CardFront, cardToUpdate.CardBack, cardToUpdate.CardId, cardToUpdate.Tags);
            if(card != null)
            {
                return Ok(card);
            }
            else
            {
                return BadRequest(new { message = "An error occured. Unable to update card." });
            }
        }


    }
}
