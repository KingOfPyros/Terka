using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terka
{
    public partial class Form3 : Form
    {
        private string connectionString = "Data Source=SQL6030.site4now.net;Initial Catalog=db_a9b162_illiakursnew2;User Id=db_a9b162_illiakursnew2_admin;Password=qwerty123";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadProductData();
        }

        private void LoadProductData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string productName = ProductNameTextBox.Text.Trim();

                SqlCommand selectCommand = new SqlCommand("SELECT Name, Description, Image FROM Items WHERE Name = @ProductName", connection);
                selectCommand.Parameters.AddWithValue("@ProductName", productName);
                SqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    string name = reader.GetString(0);
                    string description = reader.GetString(1);
                    byte[] imageBytes = reader.IsDBNull(2) ? null : (byte[])reader.GetValue(2);

                    NameTextBox.Text = name;
                    DescriptionTextBox.Text = description;

                    if (imageBytes != null)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            PictureBox.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        PictureBox.Image = null;
                    }
                }
                else
                {
                    MessageBox.Show("Item not found.");
                    ResetForm();
                }

                reader.Close();
            }
        }

        private void ResetForm()
        {
            NameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            PictureBox.Image = null;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            LoadProductData();
        }
    }
}
