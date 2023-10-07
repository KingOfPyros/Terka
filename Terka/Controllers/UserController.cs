using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terka.Models;

namespace Terka.Controllers
{
    internal class UserController
    {
        private UserModel? model;
        private Form1? view;
        private string connectionString = "Data Source=SQL8005.site4now.net;Initial Catalog=db_a9c6db_illiakurslog;User Id=db_a9c6db_illiakurslog_admin;Password=qwerty123";


        public UserController(UserModel model, Form1 view)
        {
            this.model = model;
            this.view = view;

            view.LoginClicked += OnLoginClicked;
            view.RegisterClicked += OnRegisterClicked;
        }
        private void OnLoginClicked(string username, string password)
        {
            if (IsAuthenticated(username, password))
            {
                MessageBox.Show("Авторизация успешна!");
            }
            else
            {
                MessageBox.Show("Ошибка авторизации!");
            }
        }
        private void OnRegisterClicked(string username, string password, string role)
        {
            string username1 = view.UsernameTextBox.Text;
            string password1 = view.PasswordTextBox.Text;
            string? role1 = view.RoleComboBox.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username1) || string.IsNullOrEmpty(password1) || string.IsNullOrEmpty(role1))
            {
                MessageBox.Show("Please enter all the required information.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand checkUserCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", connection);
                checkUserCommand.Parameters.AddWithValue("@Username", username1);
                int userCount = (int)checkUserCommand.ExecuteScalar();

                if (userCount > 0)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.");
                    return;
                }

                SqlCommand registerCommand = new SqlCommand("INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)", connection);
                registerCommand.Parameters.AddWithValue("@Username", username1);
                registerCommand.Parameters.AddWithValue("@Password", password1);
                registerCommand.Parameters.AddWithValue("@Role", role1);
                registerCommand.ExecuteNonQuery();

                MessageBox.Show("Registration successful. You can now log in.");
            }
        }
        private bool IsAuthenticated(string username, string password)
        {
            
            string username1 = view.UsernameTextBox.Text;
            string password1 = view.PasswordTextBox.Text;

            if (string.IsNullOrEmpty(username1) || string.IsNullOrEmpty(password1))
            {
                MessageBox.Show("Please enter your username and password.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand loginCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password", connection);
                loginCommand.Parameters.AddWithValue("@Username", username1);
                loginCommand.Parameters.AddWithValue("@Password", password1);
                int userCount = (int)loginCommand.ExecuteScalar();

                if (userCount > 0)
                {
                    SqlCommand roleCommand = new SqlCommand("SELECT Role FROM Users WHERE Username = @Username", connection);
                    roleCommand.Parameters.AddWithValue("@Username", username1);
                    string? role1 = roleCommand.ExecuteScalar().ToString();

                    if (role1 == "admin")
                    {
                        Form2 form2 = new Form2();
                        form2.ShowDialog();
                        return username == username1 && password == password1;
                    }
                    else if (role1 == "user")
                    {
                        Form3 form3 = new Form3();
                        form3.ShowDialog();
                        return username == username1 && password == password1;
                    }
                    return username == username1 && password == password1;
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                    return false;
                }
            }
        }
    }
}
