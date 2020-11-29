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
    public partial class Logon : Form
    {

        private GMHelp helpScreen;
        public Logon()
        {
            InitializeComponent();
        }

        private void Logon_Load(object sender, EventArgs e)
        {

        }
        //
        // Once this button is clicked, we will attempt to connect to the database with 1 of 4 possible results:
        // 1.) Successful logon of a user with administrator rights. We will then enable the main form and hide the logon form.
        // 2.) Successful logon of a user without administrator rights. We will then keep the main form inactive and tell the user they are not an admin.
        // 3.) Unsuccessful login.  We will then keep the main form inactive and tell the user that We did not recognize the user/password combination
        // 4.) Database connection failure.We will then keep the main form inactive and tell the user we encountered an issue talking to the database.
        // 
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string passWord = textBox2.Text;
            (bool isUser, bool isAdmin, bool isConnect) logonTuple;

            logonTuple = CheckLogon(userName, passWord);
            Console.WriteLine(logonTuple.isUser.ToString() + " " + logonTuple.isAdmin.ToString() + " " + logonTuple.isConnect.ToString());
            if (logonTuple.isUser == true && logonTuple.isAdmin == true)
            {
                label3.Text = "Welcome Admin";
                this.Owner.Enabled = true;
                this.Hide();
            }
            else if (logonTuple.isUser == true && logonTuple.isAdmin == false)
            {
                label3.Text = "Our records show that you are not an admin.";
            }
            else if (logonTuple.isUser == false && logonTuple.isConnect == true)
            {
                label3.Text = "We did not recognize the user/password combination you entered. Please try again!";
            }
            else if (logonTuple.isConnect == false)
            {
                label3.Text = "We encountered an issue talking to the database.";
            }
        }
        // Method to check logon
        //
        public (bool isUser, bool isAdmin, bool isConnect) CheckLogon(string userName, string passWord)
        {
            bool isUser = false;
            bool isAdmin = false;
            bool isConnect = true;
            (bool isUser, bool isAdmin, bool isConnect) returnTuple;
            SqlConnectionStringBuilder gameBuilder = new SqlConnectionStringBuilder();
            gameBuilder.DataSource = "LAPTOP-6OC8EFQJ";
            gameBuilder.UserID = "GMClient";
            gameBuilder.Password = "GMClient";
            gameBuilder.InitialCatalog = "TermProject";

            try
            {
                // Connect to the SQL Server game database with the admin account to verify we can get connection(database is up)
                //
                using (SqlConnection gameConnection = new SqlConnection(gameBuilder.ConnectionString))
                {
                    gameConnection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT UserName, Password, IS_GM FROM [TermProject].[dbo].[account]", gameConnection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0).ToString() + " " + reader.GetString(1).ToString() + " " + reader.GetInt32(2).ToString());
                            // Check to see if the username/password Combination is valid 
                            //
                            if (reader.GetString(0).ToString() == userName && reader.GetString(1).ToString() == passWord)
                            {
                                isUser = true;
                                //  Check to see if the valid username/password Combination is a Admin
                                //
                                if (reader.GetInt32(2) == 1)
                                {
                                    isAdmin = true;
                                }
                            }
                        }
                    }
                    gameConnection.Close();
                }
            }
            catch (Exception errorText)
            {
                MessageBox.Show("Cannot connect to the database: " + errorText.ToString());
                isConnect = false;
            }
            // Populate the return tuple of bools
            //
            returnTuple.isUser = isUser;
            returnTuple.isAdmin = isAdmin;
            returnTuple.isConnect = isConnect;
            return returnTuple;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            System.Windows.Forms.Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            helpScreen = new GMHelp();
            // Make sure to set the background color to the color of the main form(form1)
            helpScreen.BackColor = this.BackColor;
            helpScreen.Show();
            helpScreen.TopMost = true;
        }
    }
}
