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

        [HttpPost]
        public ActionResult<Deck> AddDeck(Deck newDeck)
        {
            ActionResult result;

            Deck deck = deckDao.AddDeck(newDeck.UserId, newDeck.DeckName);

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
    }
}
