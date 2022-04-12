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

            Card card = cardDao.AddCard(newCard.CardFront, newCard.CardBack, newCard.UserId);

            if(card != null)
            {
                result = Created(card.CardFront,card);
            }
            else
            {
                result = BadRequest(new { message = "An error occured and card was not created." });
            }

            return result;
        }

      

        
    }
}
