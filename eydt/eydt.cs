using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;

using System.IO;

namespace textEditor
{
    public partial class eydt : Form
    {
        public eydt()
        {
            InitializeComponent();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.Clear();
        }

        private void OpenDialog()
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Text File|*.txt|Any File|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(of.FileName);
                fastColoredTextBox.Text = sr.ReadToEnd();
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
                sw.Write(fastColoredTextBox.Text);
                sw.Close();
            }
            catch
            {
                OpenDialog();
            }
        }
                 
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
                e.Graphics.DrawString(fastColoredTextBox.Text, new Font("Segoe UI", 14, FontStyle.Regular), Brushes.Black, new PointF(100, 100));
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
                    printDocument.Print();
        }
        
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.Paste();
        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.Undo();
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.Redo();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/patoriko/textEditor");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newToolStripButton.PerformClick();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(this.Text);
                sw.Write(fastColoredTextBox.Text);
                sw.Close();
            }
            catch
            {
                OpenDialog();
            }
        }

        private void saveDialog()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text File|*.txt|Any File|*.*";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sr = new StreamWriter(sf.FileName);
                sr.Write(fastColoredTextBox.Text);
                sr.Close();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
                printDocument.Print();
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
                fastColoredTextBox.Font = fd.Font;
            }
        }

        private void lightTheme()
        {
            darkThemeToolStripMenuItem.Checked = false;
            lightThemeToolStripMenuItem.Checked = true;

            fileToolStripMenuItem.ForeColor = Color.Black;
            editToolStripMenuItem.ForeColor = Color.Black;
            toolsToolStripMenuItem.ForeColor = Color.Black;
            helpToolStripMenuItem.ForeColor = Color.Black;

            menuStrip.BackColor = SystemColors.Control;

            toolStrip.BackColor = SystemColors.Control;

            fastColoredTextBox.BackColor = Color.White;
            fastColoredTextBox.ForeColor = Color.Black;
            fastColoredTextBox.LineNumberColor = Color.Turquoise;
            fastColoredTextBox.IndentBackColor = Color.WhiteSmoke;
        }

        private void darkTheme()
        {
            lightThemeToolStripMenuItem.Checked = false;
            darkThemeToolStripMenuItem.Checked = true;

            fileToolStripMenuItem.ForeColor = Color.White;
            editToolStripMenuItem.ForeColor = Color.White;
            toolsToolStripMenuItem.ForeColor = Color.White;
            helpToolStripMenuItem.ForeColor = Color.White;

            menuStrip.BackColor = Color.Black;

            toolStrip.BackColor = Color.Black;

            fastColoredTextBox.BackColor = Color.Black;
            fastColoredTextBox.ForeColor = Color.White;
            fastColoredTextBox.LineNumberColor = Color.Turquoise;
            fastColoredTextBox.IndentBackColor = Color.Black;
        }        
    }
}
