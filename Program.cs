using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;


namespace Capstone
{
    static class Program
    {
        
    /************************************
    Title: Game Administrator
    Application Type: WinForms
    Description: This program will also game administrator to perform admin functions 
    Author: (your name)
    Date Created: (current date)
    Last Modified:
    ************************************/
       
        [STAThread]
        static void Main()
        {
    
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
