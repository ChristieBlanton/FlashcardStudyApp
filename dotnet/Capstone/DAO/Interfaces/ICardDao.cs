using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Interfaces
{
    public interface ICardDao
    {
        Card AddCard(string cardFront, string cardBack, int userId, int deckId, string[] tags);

        Card GetCard(int cardId);

        bool AddCardToDeck(int cardId, int deckId);

        List<Card> GetCardsInDeck(int deckId);

    }
}
