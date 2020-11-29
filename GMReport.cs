using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Capstone
{
    public partial class GMReport : Form
    {
       
        SqlCommand gameSqlCommand;
        SqlDataAdapter gameSqlAdapter;
        SqlCommandBuilder gameSqlBuilder;
        DataSet gameDateSet;
        DataTable gameDataTable;
        public GMReport(String reportSQL)
        {
            InitializeComponent();
            string connectionString = "Data Source=.;Initial Catalog=TermProject;Integrated Security=True";
            string sql = reportSQL;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            gameSqlCommand = new SqlCommand(sql, connection);
            gameSqlAdapter = new SqlDataAdapter(gameSqlCommand);
            gameSqlBuilder = new SqlCommandBuilder(gameSqlAdapter);
            gameDateSet = new DataSet();
            gameSqlAdapter.Fill(gameDateSet, "Report");
            gameDataTable = gameDateSet.Tables["Report"];
            connection.Close();
            dataGridView1.DataSource = gameDateSet.Tables["Report"];
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void GMReport_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
