using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UnicomTicManagementSystem.Repositories
{
    public static class DbConfig
    {
        private static string connectionString = "Data Source=UnicomTic.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(connectionString);
            conn.Open();
            return conn;
        }

    }
}
