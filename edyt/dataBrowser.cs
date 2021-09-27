using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace edytApp
{
    public partial class dataBrowser : Form
    {
        public dataBrowser()
        {
            InitializeComponent();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        private void browserButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/patoriko/Edyt-Code-Compiler");
        }
    }
}
