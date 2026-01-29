using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RMP_Demo_29._01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static void OrderView(DataGridView dgv, string status = "")
        {
            string query = @"
                SELECT 
                    o.ord_id,
                    c.name,
                    o.disc,
                    o.start_date,
                    o.order_status,
                    d.type,
                    e.name
                FROM Orders o
                JOIN Clients c ON o.cl_id = c.cl_id
                JOIN Devices d ON o.dev_id = d.dev_id
                JOIN Employees e ON o.emp_id = e.emp_id";

            if (status == "В процессе" || status == "Завершён")
            {
                query += $" WHERE o.order_status = '{status}'";
            }

            query += " ORDER BY o.ord_id;";

            using (var connection = new SQLiteConnection($"Data Source=..\\..\\..\\!_DB.db;Version=3;"))
            {
                connection.Open();
                using (var adapter = new SQLiteDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) => OrderView(dgv);

        private void button2_Click(object sender, EventArgs e) => OrderView(dgv, "В процессе");

        private void button3_Click(object sender, EventArgs e) => OrderView(dgv, "Завершён");

    }
}
