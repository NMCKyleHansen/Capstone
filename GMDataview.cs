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
    public partial class GMDataview : Form
    {
        SqlCommand gameSqlCommand;
        SqlDataAdapter gameSqlAdapter;
        SqlCommandBuilder gameSqlBuilder;
        DataSet gameDateSet;
        DataTable gameDataTable;
        public GMDataview()
        {
            InitializeComponent();
        }

        private void GMDataview_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.;Initial Catalog=TermProject;Integrated Security=True";
            string sql = "SELECT * FROM Account";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            gameSqlCommand = new SqlCommand(sql, connection);
            gameSqlAdapter = new SqlDataAdapter(gameSqlCommand);
            gameSqlBuilder = new SqlCommandBuilder(gameSqlAdapter);
            gameDateSet = new DataSet();
            gameSqlAdapter.Fill(gameDateSet, "Account");
            gameDataTable = gameDateSet.Tables["Account"];
            connection.Close();
            dataGridView1.DataSource = gameDateSet.Tables["Account"];
            dataGridView1.ReadOnly = true;
            save_btn.Enabled = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void save_Click(object sender, EventArgs e)
        {
            gameSqlAdapter.Update(gameDataTable);
            dataGridView1.ReadOnly = true;
            save_btn.Enabled = false;
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            save_btn.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
