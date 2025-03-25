using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseTech.Repositories.Interfaces;
using WarehouseTech.Repositories;
using WarehouseTech.Services.Interfaces;
using WarehouseTech.Services;
using Microsoft.Extensions.DependencyInjection;
using WarehouseTech.Models;

namespace WarehouseTech
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static User CurrentUser { get; set; }
        [STAThread]

        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var dbConnection = new DatabaseConnection();
            var userRepo = new UserRepository(dbConnection);
            var authService = new AuthService(userRepo);
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            Application.Run(new LoginForm(authService));
        }
    }
}
