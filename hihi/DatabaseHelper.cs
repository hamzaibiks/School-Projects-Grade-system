using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hihi
{
    internal class DatabaseHelper
    {
        private static string dbFileName = "JuryGradingDB.sqlite";
        public static string ConnectionString = $"Data Source={dbFileName};Version=3;";

        public static void InitializeDatabase()
        {
            if (!System.IO.File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string createTableQuery = @"
    CREATE TABLE IF NOT EXISTS Projects (
        ID INTEGER PRIMARY KEY AUTOINCREMENT,
        GroupID TEXT,
        ProjectName TEXT,
        J1_C1 REAL, J1_C2 REAL, J1_C3 REAL, J1_C4 REAL, J1_C5 REAL, Jury1_Moyen REAL,
        J2_C1 REAL, J2_C2 REAL, J2_C3 REAL, J2_C4 REAL, J2_C5 REAL, Jury2_Moyen REAL,
        J3_C1 REAL, J3_C2 REAL, J3_C3 REAL, J3_C4 REAL, J3_C5 REAL, Jury3_Moyen REAL,
        J4_C1 REAL, J4_C2 REAL, J4_C3 REAL, J4_C4 REAL, J4_C5 REAL, Jury4_Moyen REAL,
        MoyenneGenerale REAL,
        EvaluationDate TEXT
    )";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}


