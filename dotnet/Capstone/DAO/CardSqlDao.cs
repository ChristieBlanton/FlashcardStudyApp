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
        string sqlGetCardTags = "SELECT tag_name FROM tag t JOIN card_tag ct ON t.tag_id = ct.tag_id WHERE ct.card_id = @card_id";
        string sqlGetCardsInDeck = "SELECT * FROM card WHERE card_id IN (SELECT card_id FROM card_deck WHERE deck_id = @deck_id);";
        string sqlFindTagId = "SELECT tag_id FROM tag WHERE tag_name = @tag_name";
        string sqlAddTagToCard = "INSERT INTO card_tag (card_id, tag_id) VALUES (@card_id, @tag_id)";
        string sqlAddTagToTags = "INSERT INTO tag (tag_name) OUTPUT inserted.tag_id VALUES (@tag_name)";
        string sqlDeleteCardFromDeck = "DELETE FROM card_deck WHERE card_id = @card_id AND deck_id = @deck_id;";
        string sqlUpdateCard = "UPDATE card SET card_front = @card_front, card_back = @card_back WHERE card_id = @card_id;";
        string sqlDeleteCardTags = "DELETE FROM card_tag WHERE card_id = @card_id;";
        public Card AddCard(string cardFront, string cardBack, int userId, int deckId, string[] tags)
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
            catch (SqlException)
            {
                throw;
            }
            AddCardToDeck(cardId, deckId);
            AddTagsToCard(tags, cardId);

            return GetCard(cardId);
        }

        private bool AddTagsToCard(string[] tags, int cardId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    for (int i = 0; i < tags.Length; i++)
                    {
                        SqlCommand cmd = new SqlCommand(sqlFindTagId, conn);
                        cmd.Parameters.AddWithValue("@tag_name", tags[i].ToLower());

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int tagId = Convert.ToInt32(reader["tag_id"]);
                            conn.Close();
                            conn.Open();
                            SqlCommand cmd2 = new SqlCommand(sqlAddTagToCard, conn);
                            cmd2.Parameters.AddWithValue("@card_id", cardId);
                            cmd2.Parameters.AddWithValue("@tag_id", tagId);

                            cmd2.ExecuteNonQuery();
                        }
                        else
                        {
                            conn.Close();
                            conn.Open();
                            SqlCommand cmd4 = new SqlCommand(sqlAddTagToTags, conn);
                            cmd4.Parameters.AddWithValue("@tag_name", tags[i].ToLower());

                            int newId = Convert.ToInt32(cmd4.ExecuteScalar());
                            conn.Close();
                            conn.Open();
                            SqlCommand cmd3 = new SqlCommand(sqlAddTagToCard, conn);
                            cmd3.Parameters.AddWithValue("@card_id", cardId);
                            cmd3.Parameters.AddWithValue("@tag_id", newId);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return true;
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

        public bool DeleteCardFromDeck(int cardId, int deckId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlDeleteCardFromDeck, conn);

                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    cmd.Parameters.AddWithValue("@card_id", cardId);

                    int lines = cmd.ExecuteNonQuery();
                    return lines > 0;

                }
            }
            catch
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
            List<string> tags = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetCard, conn);
                    cmd.Parameters.AddWithValue("@card_id", cardId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        card = GetCardFromReader(reader);
                    }
                    conn.Close();
                    conn.Open();

                    cmd = new SqlCommand(sqlGetCardTags, conn);
                    cmd.Parameters.AddWithValue("@card_id", cardId);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tags.Add(Convert.ToString(reader["tag_name"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            card.Tags = tags.ToArray();
            return card;
        }

        public Card UpdateCard(string cardFront, string cardBack, int cardId, string[] tags)
        {


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateCard, conn);
                    cmd.Parameters.AddWithValue("@card_front", cardFront);
                    cmd.Parameters.AddWithValue("@card_back", cardBack);
                    cmd.Parameters.AddWithValue("@card_id", cardId);


                    cmd.ExecuteNonQuery();


                }
                DeleteTagsFromCard(cardId);
                AddTagsToCard(tags, cardId);
            }
            catch (SqlException)
            {
                throw;
            }
            return GetCard(cardId);

        }

        public bool DeleteTagsFromCard(int cardId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {


                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlDeleteCardTags, conn);
                    cmd.Parameters.AddWithValue("@card_id", cardId);

                    int lines = cmd.ExecuteNonQuery();
                    return lines > 0;


                }
            }
            catch (SqlException)
            {
                throw;
            }
            
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
