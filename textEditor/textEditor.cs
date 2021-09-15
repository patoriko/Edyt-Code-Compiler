using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace textEditor
{
    public partial class textEditor : Form
    {
        public textEditor()
        {
            InitializeComponent();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }

        private void OpenDialog()
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Text File|*.txt|Any File|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(of.FileName);
                richTextBox.Text = sr.ReadToEnd();
                sr.Close();
                this.Text = of.FileName;
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenDialog();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(this.Text);
                sw.Write(richTextBox.Text);
                sw.Close();
            }
            catch
            {
                OpenDialog();
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Redo();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newToolStripButton.PerformClick();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveDialog()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text File|*.txt|Any File|*.*";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sr = new StreamWriter(sf.FileName);
                sr.Write(richTextBox.Text);
                sr.Close();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undoToolStripButton.PerformClick();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redoToolStripButton.PerformClick();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutToolStripButton.PerformClick();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyToolStripButton.PerformClick();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteToolStripButton.PerformClick();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lightThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lightTheme();
        }

        private void darkThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkTheme();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox.Font = fd.Font;
            }
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        // themes
        private void lightTheme()
        {

        }

        private void darkTheme()
        {

        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
