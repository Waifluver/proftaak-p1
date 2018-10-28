using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Proftaak
{
    class Database
    {
        private MySqlConnection connection;

        public void OpenConnection()
        {
            string connectionString = "Server=localhost; Database=syb; Uid=root; Pwd=Kufverzippe8;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }


        public User Authenticate(string _username, string _password)
        {
            string queryString = "SELECT * " +
                                 "FROM syb.user u " +
                                 "WHERE u.username = @username AND u.password = @password";

            MySqlCommand command = new MySqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@username", _username);
            command.Parameters.AddWithValue("@password", _password);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    User user = new User(id, username);

                    return user;
                }

                return null;
            }
        }

        public List<Drink> GetAvailableDrinks()
        {
            string queryString = "SELECT d.id, d.name, d.image, d.distance, count(*) as amount " +
                                 "FROM syb.drink d, syb.pairing p " +
                                 "WHERE d.id = p.drinkId " +
                                 "GROUP BY d.name;";
            MySqlCommand command = new MySqlCommand(queryString, connection);

            List<Drink> drinks = new List<Drink>();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader[1].ToString();
                    string image = reader[2].ToString();
                    int distance = 0;

                    if (!reader.IsDBNull(3))
                    {
                        distance = reader.GetInt32(3);
                    }

                    Drink drink = new Drink(id, name, image, 0, distance);
                    drinks.Add(drink);
                }
            }

            return drinks;
        }

        public List<Drink> GetAllDrinks()
        {
            string queryString = "SELECT * FROM syb.drink";
            MySqlCommand command = new MySqlCommand(queryString, connection);

            List<Drink> drinks = new List<Drink>();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader[1].ToString();
                    string image = reader[2].ToString();
                    int distance = 0;

                    if (!reader.IsDBNull(3))
                    {
                        distance = reader.GetInt32(3);
                    }

                    Drink drink = new Drink(id, name, image, 0, distance);
                    drinks.Add(drink);
                }
            }

            return drinks;
        }

        public List<Drink> GetPairings(int drinkId)
        {
            string queryString = "SELECT p.pairingId, d.name, d.image, p.score, d.distance " +
                                 "FROM syb.drink d, syb.pairing p " +
                                 "WHERE d.id = p.pairingId AND p.drinkId = " + drinkId + " " +
                                 "ORDER BY p.score DESC;";
            MySqlCommand command = new MySqlCommand(queryString, connection);

            List<Drink> drinks = new List<Drink>();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader[1].ToString();
                    string image = reader[2].ToString();
                    int score = reader.GetInt32(3);
                    int distance = 0;

                    if (!reader.IsDBNull(4))
                    {
                        distance = reader.GetInt32(4);
                    }

                    Drink drink = new Drink(id, name, image, score, distance);
                    drinks.Add(drink);
                }
            }

            return drinks;
        }


        public void AddFavorite(int userId, int drink1Id, int drink2Id)
        {
            string queryString = $"INSERT INTO syb.favorites (`userId`, `drink1Id`, `drink2Id`) VALUES ({userId}, {drink1Id}, {drink2Id});";
            MySqlCommand command = new MySqlCommand(queryString, connection);
            command.ExecuteReader();
        }


        public List<Pairing> GetFavorites(int userId)
        {
            string queryString = "SELECT d.id, d.name, d.image, d.distance " +
                                 "FROM syb.favorites f, syb.drink d " +
                                 "WHERE f.userId = @userId AND d.id = f.drink1Id OR d.id = f.drink2Id "; 
                                 
            MySqlCommand command = new MySqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@userId", userId);
            

            List<Drink> drinks = new List<Drink>();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader[1].ToString();
                    string image = reader[2].ToString();
                    int distance = 0;

                    if (!reader.IsDBNull(3))
                    {
                        distance = reader.GetInt32(3);
                    }

                    Drink drink = new Drink(id, name, image, 0, distance);
                    drinks.Add(drink);
                }
            }
            List<Pairing> pairings = new List<Pairing>();
            for (int i = 0; i < drinks.Count; i += 2)
            {
                pairings.Add(new Pairing
                {
                    Drink1 = drinks[i],
                    Drink2 = drinks[i + 1]
                });
            }

            return pairings;
        }
    }
}
