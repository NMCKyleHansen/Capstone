using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace Capstone
{
    /************************************
   Title: (title)
   Application Type: WinForms
   Description: This program is a Winforms front-end admin tools for a game database that I designed as my final project in CIT178.
   Author:         Kyle Hansen
   Date Created:   11/14/2020
   Last Modified:  11/22/2020
   ************************************/


   
    public partial class Form1 : Form
    {
        private Welcome welcomeScreen;
        private Logon logonScreen;
        bool isAdmin = false;
        string userName;
        string passWord;
        (bool isUser, bool isAdmin, bool isConnect) logonTuple;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
       

        public Form1()
        {
            welcomeScreen = new Welcome();
            welcomeScreen.Show();
            welcomeScreen.TopMost = true;
            InitializeComponent();
            /*
             userName = "Bill";
            passWord = "Bill";
            logonTuple = CheckLogon(userName,passWord);
            */
            logonScreen = new Logon();
            logonScreen.Show(this);
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            SqlConnectionStringBuilder gameBuilder = new SqlConnectionStringBuilder();
            gameBuilder.DataSource = "LAPTOP-6OC8EFQJ";
            gameBuilder.UserID = "GMClient";
            gameBuilder.Password = "GMClient";
            gameBuilder.InitialCatalog = "TermProject";

            try
            {

                // Connect to the SQL Server game database with the admin account to verify we can get connection(database is up)
                using (SqlConnection gameConnection = new SqlConnection(gameBuilder.ConnectionString))
                {
                    gameConnection.Open();
                    MessageBox.Show("Connection is Open");
                    gameConnection.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to the database");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Show colorDialog1
            DialogResult result = colorDialog1.ShowDialog();
            // if GM hit OK.
            if (result == DialogResult.OK)
            {
                // Set background 
                this.BackColor = colorDialog1.Color;
            }
        }

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

        }

}
