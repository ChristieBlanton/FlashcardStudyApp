using Capstone.DAO.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class DeckSqlDao : IDeckDao
    {
        private readonly string connectionString;

        public DeckSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        string sqlAddDeck = "INSERT INTO decks (user_id, deck_name) OUTPUT inserted.deck_id VALUES(@user_id, @deck_name) ";
        string sqlGetDeck = "SELECT * FROM decks WHERE deck_id = @deck_id";
        public Deck AddDeck (int userId, string deckName)
        {
            int deckId = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlAddDeck, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@deck_name", deckName);
                    deckId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetDeck(deckId);
        }

        public Deck GetDeck(int deckId)
        {
            Deck deck = new Deck();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetDeck, conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        deck = GetDeckFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return deck;
        }

        private Deck GetDeckFromReader(SqlDataReader reader)
        {
            Deck d = new Deck()
            {
                DeckId = Convert.ToInt32(reader["deck_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                DeckName = Convert.ToString(reader["deck_name"])
            };
            return d;
        }
    }
}