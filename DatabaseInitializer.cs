using System;
using System.Data.SQLite;

namespace AISTex
{
    internal class DatabaseInitializer
    {
        private const string ConnectionString = "Data Source=db.db;Version=3;";

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string createEmployeesTable = @"
                    CREATE TABLE IF NOT EXISTS Employees (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        position TEXT NOT NULL,
                        office_location TEXT NOT NULL
                    );";

                string createEquipmentTable = @"
                    CREATE TABLE IF NOT EXISTS Equipment (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        serial_number TEXT NOT NULL,
                        inventory_number TEXT NOT NULL,
                        purchase_date DATE,
                        warranty_period INTEGER
                    );";

                string createOrdersTable = @"
                    CREATE TABLE IF NOT EXISTS Orders (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        employee_id INTEGER,
                        equipment_id INTEGER,
                        order_date DATE,
                        status TEXT,
                        FOREIGN KEY (employee_id) REFERENCES Employees(id),
                        FOREIGN KEY (equipment_id) REFERENCES Equipment(id)
                    );";

                string createMaintenanceTable = @"
                    CREATE TABLE IF NOT EXISTS Maintenance (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        equipment_id INTEGER,
                        maintenance_date DATE,
                        details TEXT,
                        FOREIGN KEY (equipment_id) REFERENCES Equipment(id)
                    );";

                using (var command = new SQLiteCommand(createEmployeesTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createEquipmentTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createOrdersTable, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createMaintenanceTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}