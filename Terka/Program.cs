using Terka.Controllers;
using Terka.Models;

namespace Terka
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            UserModel userModel = new UserModel();
            Form1 loginForm = new Form1();
            UserController userController = new UserController(userModel, loginForm);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}