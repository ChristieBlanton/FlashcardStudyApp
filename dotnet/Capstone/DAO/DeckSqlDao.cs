﻿using Capstone.DAO.Interfaces;
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

        string sqlAddDeck = "INSERT INTO deck (user_id, deck_name, deck_description) OUTPUT inserted.deck_id VALUES(@user_id, @deck_name, @deck_description) ";
        string sqlGetDeck = "SELECT * FROM deck WHERE deck_id = @deck_id";
        string sqlMyDecks = "SELECT * FROM deck WHERE user_id = @user_id";
        string sqlUpdateDeck = "UPDATE deck SET deck_name = @deck_name, deck_description = @deck_description WHERE deck_id = @deck_id AND isPublic = 0";
        string sqlDeleteDeck = "DELETE FROM card_deck WHERE deck_id = @deck_id; DELETE FROM deck WHERE deck_id = @deck_id";
        public Deck AddDeck (int userId, string deckName, string deckDescription)
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
                    cmd.Parameters.AddWithValue("@deck_description", deckDescription);
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

        public List<Deck> MyDecks(int userId)
        {
            List<Deck> decks = new List<Deck>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlMyDecks, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Deck deck = GetDeckFromReader(reader);
                        decks.Add(deck);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return decks;
        }

        public Deck UpdateDeck(int deckId, string deckName, string deckDescription)
        {
 
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateDeck, conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    cmd.Parameters.AddWithValue("@deck_name", deckName);
                    cmd.Parameters.AddWithValue("@deck_description", deckDescription);

                    cmd.ExecuteNonQuery();

           
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return GetDeck(deckId);
        }

        public bool DeleteDeck(int deckId)
        {
            
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlDeleteDeck, conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);

                    int lines = cmd.ExecuteNonQuery();
                    return lines > 0;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private Deck GetDeckFromReader(SqlDataReader reader)
        {
            Deck d = new Deck()
            {
                DeckId = Convert.ToInt32(reader["deck_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                DeckName = Convert.ToString(reader["deck_name"]),
                DeckDescription = Convert.ToString(reader["deck_description"])
            };
            return d;
        }
    }
}