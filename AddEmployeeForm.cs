using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace AISTex
{
    public partial class AddEmployeeForm : Form
    {
        private const string ConnectionString = "Data Source=db.db;Version=3;";

        public AddEmployeeForm()
        {
            InitializeComponent();
            InitializeTableSelector();
            LoadEmployees(); 
        }

        private void InitializeTableSelector()
        {
            comboBoxTableSelector.Items.Add("Athletes");
            comboBoxTableSelector.Items.Add("Competitions");
            comboBoxTableSelector.Items.Add("Sports");
            comboBoxTableSelector.SelectedIndex = 0;
            comboBoxTableSelector.SelectedIndexChanged += ComboBoxTableSelector_SelectedIndexChanged;
        }

        private void ComboBoxTableSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string selectedTable = comboBoxTableSelector.SelectedItem.ToString();
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM {selectedTable}";
                    using (var adapter = new SQLiteDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }

        private void LoadEmployees()
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void zap1_Click(object sender, EventArgs e)
        {
            ShowHighestRatedTournament();
        }

        private void ShowHighestRatedTournament()
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"SELECT * FROM Competitions 
                    WHERE CompetitionName = 'Открытый чемпионат Киева' 
                    AND Location = 'Киев' 
                    AND CompetitionDate BETWEEN '2000-01-01' AND '2000-12-31' 
                    AND SportID = (SELECT SportID FROM Sports WHERE SportName = 'Шахматы');";

                    using (var adapter = new SQLiteDataAdapter(query, connection))
                    {
                        DataTable resultsTable = new DataTable();
                        adapter.Fill(resultsTable);
                        dataGridViewResults.DataSource = resultsTable; 
                    }
                }
            }
            finally { }
    }

        private void zap2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"SELECT a.FullName 
                    FROM Athletes a 
                    JOIN Competitions c ON a.AthleteID = c.AthleteID 
                    GROUP BY a.AthleteID 
                    HAVING COUNT(DISTINCT c.SportID) > 3;";

                    using (var adapter = new SQLiteDataAdapter(query, connection))
                    {
                        DataTable resultsTable = new DataTable();
                        adapter.Fill(resultsTable);
                        dataGridViewResults.DataSource = resultsTable;
                    }
                }
            }
            finally { }
        }

        private void zap3_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"SELECT a.FullName 
                FROM Athletes a 
                JOIN Competitions c ON a.AthleteID = c.AthleteID 
                JOIN Sports s ON c.SportID = s.SportID 
                WHERE c.Result < s.WorldRecord;";

                    using (var adapter = new SQLiteDataAdapter(query, connection))
                    {
                        DataTable resultsTable = new DataTable();
                        adapter.Fill(resultsTable);
                        dataGridViewResults.DataSource = resultsTable;
                    }
                }
            }
            finally { }
        }

        private void zap4_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"SELECT MIN(c.Result) AS BestResult 
                    FROM Competitions c 
                    JOIN Athletes a ON c.AthleteID = a.AthleteID 
                    WHERE a.FullName = 'Александр Караваев' 
                    AND c.SportID = (SELECT SportID FROM Sports WHERE SportName = 'Бег');";

                    using (var adapter = new SQLiteDataAdapter(query, connection))
                    {
                        DataTable resultsTable = new DataTable();
                        adapter.Fill(resultsTable);
                        dataGridViewResults.DataSource = resultsTable;
                    }
                }
            }
            finally { }
        }
    }
}
