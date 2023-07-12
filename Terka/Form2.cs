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
    public partial class Form2 : Form
    {
        private string connectionString = "Data Source=SQL6030.site4now.net;Initial Catalog=db_a9b162_illiakursnew2;User Id=db_a9b162_illiakursnew2_admin;Password=qwerty123";

        public Form2()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string description = DescriptionTextBox.Text;
            string name = NameTextBox.Text;

            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter the description and name.");
                return;
            }

            byte[] imageBytes = null;
            if (PictureBox.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    PictureBox.Image.Save(ms, PictureBox.Image.RawFormat);
                    imageBytes = ms.ToArray();
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand insertCommand = new SqlCommand("INSERT INTO Items (Name, Description, Image) VALUES (@Name, @Description, @Image)", connection);
                insertCommand.Parameters.AddWithValue("@Name", name);
                insertCommand.Parameters.AddWithValue("@Description", description);
                insertCommand.Parameters.AddWithValue("@Image", imageBytes ?? (object)DBNull.Value);
                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Product information saved successfully.");
                ResetForm();
            }
        }

        private void ResetForm()
        {
            NameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            PictureBox.Image = null;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PictureBox.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
