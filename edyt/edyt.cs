using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;

using System.IO;

namespace edytApp
{
    public partial class edyt : Form
    {
        #region 

        public bool isSaved = true;

        private string currentFile = string.Empty;

        #endregion

        public edyt()
        {
            InitializeComponent();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox.Text != string.Empty)
            {
                if ((DialogResult.OK == MessageBox.Show("This file will be lost", "continue?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)))
                {
                    currentFile = string.Empty;

                    fastColoredTextBox.Text = string.Empty;
                }
            }
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
                this.Text = of.FileName + " - Edyt";
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
            System.Diagnostics.Process.Start("https://github.com/patoriko/Edyt-Code-Compiler");
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
                isSaved = true;
                StreamWriter sw = new StreamWriter(this.Text);
                sw.Write(fastColoredTextBox.Text);
                sw.Close();
            }
            catch
            {
                isSaved = false;
                saveDialog();
            }
                
            
        }

        private void saveDialog()
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Text File|*.txt|Any File|*.*";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                isSaved = true;
                this.Text = sf.FileName + " - Edyt";
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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("error");
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.ShowReplaceDialog();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fastColoredTextBox.ShowGoToDialog();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.HTML) 
            {
                codePreview h = new codePreview(fastColoredTextBox.Text);
                h.Show();
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.PHP)
            {
                codePreview p = new codePreview(fastColoredTextBox.Text);
                p.Show();
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.CSharp) 
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Executable File|*.exe";
                string OutPath = "?.exe";
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    OutPath = sf.FileName;
                }

                CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters(new string[] { "System.dll" });
                parameters.GenerateExecutable = true;
                parameters.OutputAssembly = OutPath;
                string[] sources = { fastColoredTextBox.Text };

                CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, sources);

                if (results.Errors.Count > 0)
                {
                    string errsText = "";
                    foreach (CompilerError CompErr in results.Errors)
                    {
                        errsText = "(" + CompErr.ErrorNumber +
                                    ")Line " + CompErr.Line +
                                    ",Column " + CompErr.Column +
                                    ":" + CompErr.ErrorText + "" +
                                    Environment.NewLine;
                    }

                    MessageBox.Show(errsText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    System.Diagnostics.Process.Start(OutPath);
                }
            }
            else
            {
                MessageBox.Show("Cannot run this file");
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

        private void fastColoredTextBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            isSaved = false;
        }

        private void edyt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaved == false)
            {
                if  (MessageBox.Show("You have unsaved changes, discard file?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void edyt_Load(object sender, EventArgs e)
        {
            if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.HTML)
            {
                this.languageLabel.Text = "HTML";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.PHP)
            {
                this.languageLabel.Text = "PHP";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.CSharp)
            {
                this.languageLabel.Text = "C#";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.JS)
            {
                this.languageLabel.Text = "JavaScript";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.Lua)
            {
                this.languageLabel.Text = "Lua";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.SQL)
            {
                this.languageLabel.Text = "SQL";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.VB)
            {
                this.languageLabel.Text = "VisualBasic";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.XML)
            {
                this.languageLabel.Text = "XML";
            }
            else
            {
                this.languageLabel.Text = "Custom";
            }
        }
    }
}
