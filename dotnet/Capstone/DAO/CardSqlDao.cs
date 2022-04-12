using Capstone.DAO.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CardSqlDao : ICardDao
    {
        private readonly string connectionString;

        public CardSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        string sqlAddCard = "INSERT INTO card (card_front, card_back, user_id) OUTPUT inserted.card_id VALUES(@card_front, @card_back, @user_id);";
        string sqlAddCardToDeck = "INSERT INTO card_deck (card_id, deck_id) VALUES (@card_id, @deck_id);";
        string sqlGetCard = "SELECT * FROM card WHERE card_id = @card_id";
        string sqlGetCardsInDeck = "SELECT * FROM card WHERE card_id IN (SELECT card_id FROM card_deck WHERE deck_id = @deck_id);";
        public Card AddCard(string cardFront, string cardBack, int userId, int deckId)
        {
            int cardId = -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAddCard, conn);
                    cmd.Parameters.AddWithValue("@card_front", cardFront);
                    cmd.Parameters.AddWithValue("@card_back", cardBack);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cardId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch(SqlException)
            {
                throw;
            }
            AddCardToDeck(cardId, deckId);

            return GetCard(cardId);
        }

        public bool AddCardToDeck(int cardId, int deckId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAddCardToDeck, conn);
                    cmd.Parameters.AddWithValue("@card_id", cardId);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (SqlException)
            {
                throw;
            }
            
        }

        public List<Card> GetCardsInDeck(int deckId)
        {
            List<Card> cards = new List<Card>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetCardsInDeck, conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Card card = GetCardFromReader(reader);
                        cards.Add(card);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return cards;
        }

        public Card GetCard(int cardId)
        {
            Card card = new Card();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetCard, conn);
                    cmd.Parameters.AddWithValue("@card_id", cardId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        card = GetCardFromReader(reader);
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return card;
        }

        private Card GetCardFromReader(SqlDataReader reader)
        {
            Card card = new Card()
            {
                CardId = Convert.ToInt32(reader["card_id"]),
                CardFront = Convert.ToString(reader["card_front"]),
                CardBack = Convert.ToString(reader["card_back"]),
                UserId = Convert.ToInt32(reader["user_id"])
            };
            return card;


        }
    }
}
