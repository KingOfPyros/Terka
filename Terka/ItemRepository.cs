using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terka
{
    public class ItemRepository
    {
        private string _connectionString;

        public ItemRepository()
        {
            _connectionString = "Data Source=SQL6030.site4now.net;Initial Catalog=db_a9b162_illiakursnew2;User Id=db_a9b162_illiakursnew2_admin;Password=qwerty123"; 
        }

        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Description, Image FROM Items";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Item item = new Item();
                        item.Id = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.Description = reader.GetString(2);
                        item.ImagePath = (byte[])reader.GetValue(3);
                        items.Add(item);
                    }
                }
            }

            return items;
        }
    }
}
