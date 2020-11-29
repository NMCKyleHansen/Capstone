using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone
{
    public partial class GMHelp : Form
    {
        public GMHelp()
        {
            InitializeComponent();
        }

        private void GMHelp_Load(object sender, EventArgs e)
        {
           richTextBox1.LoadFile("Data/The Game Master Admin Tool documentation.rtf", RichTextBoxStreamType.RichText); 
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
