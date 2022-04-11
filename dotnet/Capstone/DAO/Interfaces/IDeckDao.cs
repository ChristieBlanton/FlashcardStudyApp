using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Interfaces
{
    public interface IDeckDao
    {
        Deck AddDeck(int userId, string deckName);

        Deck GetDeck(int deckId);

        List<Deck> MyDecks(int userId);
       
    }
}
