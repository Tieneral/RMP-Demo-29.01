using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Data.SQLite;

namespace RMP_Demo_29._01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private const string STATUS_IN_PROGRESS = "В процессе";
        private const string STATUS_COMPLETED = "Завершён";

        public static void OrderView(DataGridView dgv, string dbPath = "..\\..\\..\\!_DB.db")
        {
            string connectionString = $"Data Source={dbPath};Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT \r\n    o.ord_id,\r\n    c.name AS client_name,\r\n    o.disc AS description,\r\n    o.start_date,\r\n    o.order_status,\r\n    d.type AS device_type,\r\n    e.name AS employee_name\r\nFROM Orders o\r\nJOIN Clients c ON o.cl_id = c.cl_id\r\nJOIN Devices d ON o.dev_id = d.dev_id\r\nJOIN Employees e ON o.emp_id = e.emp_id\r\nORDER BY o.ord_id;";
                using (var adapter = new SQLiteDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgv.DataSource = dataTable;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderView(dgv);
        }
    }
}
