using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Terka.Controllers;
using Terka.Models;

namespace Terka
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=SQL8005.site4now.net;Initial Catalog=db_a9c6db_illiakurslog;User Id=db_a9c6db_illiakurslog_admin;Password=qwerty123";

        public Form1()
        {
            InitializeComponent();
        }

        private void LoginButton_Click_1(object sender, EventArgs e, FormController formController)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your username and password.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand loginCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password", connection);
                loginCommand.Parameters.AddWithValue("@Username", username);
                loginCommand.Parameters.AddWithValue("@Password", password);
                int userCount = (int)loginCommand.ExecuteScalar();

                if (userCount > 0)
                {
                    SqlCommand roleCommand = new SqlCommand("SELECT Role FROM Users WHERE Username = @Username", connection);
                    roleCommand.Parameters.AddWithValue("@Username", username);
                    string role = roleCommand.ExecuteScalar().ToString();

                    if (role == "admin")
                    {
                        Form2 form2 = new Form2();
                        form2.ShowDialog();
                    }
                    else if (role == "user")
                    {
                        FormController formController1 = new FormController();
                        formController1.ShowView();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
        }

        private void RegisterButton_Click_1(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            string role = RoleComboBox.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please enter all the required information.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand checkUserCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", connection);
                checkUserCommand.Parameters.AddWithValue("@Username", username);
                int userCount = (int)checkUserCommand.ExecuteScalar();

                if (userCount > 0)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.");
                    return;
                }

                SqlCommand registerCommand = new SqlCommand("INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)", connection);
                registerCommand.Parameters.AddWithValue("@Username", username);
                registerCommand.Parameters.AddWithValue("@Password", password);
                registerCommand.Parameters.AddWithValue("@Role", role);
                registerCommand.ExecuteNonQuery();

                MessageBox.Show("Registration successful. You can now log in.");
            }
        }
    }
}