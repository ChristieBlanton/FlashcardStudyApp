using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Interfaces
{
    public interface ICardDao
    {
        Card AddCard(string cardFront, string cardBack, int userId);

        Card GetCard(int cardId);


    }
}
