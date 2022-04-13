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
    public class DeckController : ControllerBase
    {
        private readonly IDeckDao deckDao;

        public DeckController(IDeckDao _deckDao)
        {
            deckDao = _deckDao;
        }

        [HttpGet("{deckId}")]
        public ActionResult<Deck> GetDeck(int deckId)
        {
            Deck deck = deckDao.GetDeck(deckId);

            if (deck != null)
            {
                return Ok(deck);
            }
            else
            {
                return BadRequest(new { message = "Unable to retrieve deck." });
            }
        }

        [HttpPost]
        public ActionResult<Deck> AddDeck(Deck newDeck)
        {
            ActionResult result;

            Deck deck = deckDao.AddDeck(newDeck.UserId, newDeck.DeckName, newDeck.DeckDescription);

            if (deck != null)
            {
                result = Created(deck.DeckName, deck.DeckName);
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and deck was not created." });
            }
            return result;
        }

        [HttpGet("mydecks/{id}")]
        public ActionResult<List<Deck>> MyDecks(int id)
        {
            //int? userId = GetCurrentUserId();
            //if (!userId.HasValue)
            //{
            //    return BadRequest();
            //}

            ActionResult result;

            List<Deck> myDecks = deckDao.MyDecks(id);

            if (myDecks != null)
            {
                result = Ok(myDecks);
            }
            else
            {
                result = BadRequest(new { message = "Unable to retrieve decks." });
            }

            return result;
        }

        [HttpPut("mydecks")]
        public ActionResult<Deck> UpdateDeck(Deck updatedDeck)
        {
            ActionResult result;

            Deck deck = deckDao.UpdateDeck(updatedDeck.DeckId, updatedDeck.DeckName, updatedDeck.DeckDescription );

            if(deck != null)
            {
                result = Ok(deck);
            }
            else
            {
                result = BadRequest(new { message = "An error occured. Unable to update deck." });
            }

            return result;
        }

        [HttpDelete("mydecks/{deckId}")]
        public ActionResult DeleteDeck(int deckId)
        {

            bool result = deckDao.DeleteDeck(deckId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "An error occured. Unable to delete deck." });
            }
        }

        private int? GetCurrentUserId()
        {
            string userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrWhiteSpace(userId)) return null;
            int.TryParse(userId, out int userIdInt);
            return userIdInt;
        }
    }
}
