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
    public partial class codePreview : Form
    {
        public codePreview(string file)
        {
            InitializeComponent();
            webBrowser.DocumentText = file;    
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        private void browserButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/patoriko/Edyt-Code-Compiler");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
