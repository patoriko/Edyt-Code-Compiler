using System;
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

        #region Events

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

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        private void inspectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Error - Not Added Yet");
        }

        #endregion

    }
}
