using System;
using System.Xml;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;

using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

using System.IO;

namespace edytApp
{
    public partial class edyt : Form
    {

        public edyt()
        {
            InitializeComponent();
        }

        #region Variables

        private bool isSaved = true;

        private string currentFile = string.Empty;

        #endregion 

        #region Events

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
                SaveDialog();
            }
                
            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDialog();
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

        private void csToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cSharp();
        }

        private void visualBasicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            visualBasic();
        }

        private void javaScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            javaScript();
        }

        private void htmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            html();
        }

        private void phpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            php();
        }

        private void xmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml();
        }

        private void luaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lua();
        }

        private void sqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sql();
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            custom();
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
            Compile();
        }

        #endregion

        #region Functions

        private void OpenDialog()
        {
            OpenFileDialog of = new OpenFileDialog();
            if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.CSharp)
                of.Filter = "C# File (*.cs)|*.cs|Any File|*.*";
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.VB)
                of.Filter = "VB File (*.vb)|*.vb|Any File|*.*";
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.JS)
                of.Filter = "JS File (*.js)|*.js|Any File|*.*";
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.HTML)
                of.Filter = "HTML File (*.html)|*.html|Any File|*.*";
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.PHP)
                of.Filter = "PHP File (*.php)|*.php|Any File|*.*";
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.XML)
                of.Filter = "XML File (*.xml)|*.xml|Any File|*.*";
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.Lua)
                of.Filter = "Lua File (*.lua)|*.lua|Any File|*.*";
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.SQL)
                of.Filter = "SQL File (*.sql)|*.sql|Any File|*.*";
            else
                of.Filter = "Any File|*.*";

            if (of.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(of.FileName);
                fastColoredTextBox.Text = sr.ReadToEnd();
                this.Text = of.FileName + " - Edyt";
                sr.Close();
            }
        }

        private void SaveDialog()
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

        private void LanguageCheck()
        {
            if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.CSharp)
            {
                this.languageLabel.Text = "C#";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.VB)
            {
                this.languageLabel.Text = "VisualBasic";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.JS)
            {
                this.languageLabel.Text = "JavaScript";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.HTML)
            {
                this.languageLabel.Text = "HTML";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.PHP)
            {
                this.languageLabel.Text = "PHP";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.XML)
            {
                this.languageLabel.Text = "XML";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.Lua)
            {
                this.languageLabel.Text = "Lua";
            }
            else if (fastColoredTextBox.Language == FastColoredTextBoxNS.Language.SQL)
            {
                this.languageLabel.Text = "SQL";
            }
            else
            {
                this.languageLabel.Text = "Custom";
            }
        }

        private void Compile()
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
                sf.Filter = "Executable File (*.exe)|*.exe";
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
                    string errorsText = "";
                    foreach (CompilerError CompError in results.Errors)
                    {
                        errorsText = "(" + CompError.ErrorNumber +
                                    ") Line " + CompError.Line +
                                    ",Column " + CompError.Column +
                                    ":" + CompError.ErrorText + "" +
                                    Environment.NewLine;
                    }

                    MessageBox.Show(errorsText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #endregion

        #region Data

        public static void WriteXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static T ReadXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        #endregion

        #region Languages

        private void cSharp()
        {
            csToolStripMenuItem.Checked = true;
            visualBasicToolStripMenuItem.Checked = false;
            javaScriptToolStripMenuItem.Checked = false;
            htmlToolStripMenuItem.Checked = false;
            phpToolStripMenuItem.Checked = false;
            xmlToolStripMenuItem.Checked = false;
            luaToolStripMenuItem.Checked = false;
            sqlToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.CSharp;
            LanguageCheck();
        }

        private void visualBasic()
        {
            csToolStripMenuItem.Checked = false;
            visualBasicToolStripMenuItem.Checked = true;
            javaScriptToolStripMenuItem.Checked = false;
            htmlToolStripMenuItem.Checked = false;
            phpToolStripMenuItem.Checked = false;
            xmlToolStripMenuItem.Checked = false;
            luaToolStripMenuItem.Checked = false;
            sqlToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.VB;
            LanguageCheck();
        }

        private void javaScript()
        {
            csToolStripMenuItem.Checked = false;
            visualBasicToolStripMenuItem.Checked = false;
            javaScriptToolStripMenuItem.Checked = true;
            htmlToolStripMenuItem.Checked = false;
            phpToolStripMenuItem.Checked = false;
            xmlToolStripMenuItem.Checked = false;
            luaToolStripMenuItem.Checked = false;
            sqlToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.JS;
            LanguageCheck();
        }

        private void html()
        {
            csToolStripMenuItem.Checked = false;
            visualBasicToolStripMenuItem.Checked = false;
            javaScriptToolStripMenuItem.Checked = false;
            htmlToolStripMenuItem.Checked = true;
            phpToolStripMenuItem.Checked = false;
            xmlToolStripMenuItem.Checked = false;
            luaToolStripMenuItem.Checked = false;
            sqlToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.HTML;
            LanguageCheck();
        }

        private void php()
        {
            csToolStripMenuItem.Checked = false;
            visualBasicToolStripMenuItem.Checked = false;
            javaScriptToolStripMenuItem.Checked = false;
            htmlToolStripMenuItem.Checked = false;
            phpToolStripMenuItem.Checked = true;
            xmlToolStripMenuItem.Checked = false;
            luaToolStripMenuItem.Checked = false;
            sqlToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.PHP;
            LanguageCheck();
        }

        private void xml()
        {
            csToolStripMenuItem.Checked = false;
            visualBasicToolStripMenuItem.Checked = false;
            javaScriptToolStripMenuItem.Checked = false;
            htmlToolStripMenuItem.Checked = false;
            phpToolStripMenuItem.Checked = false;
            xmlToolStripMenuItem.Checked = true;
            luaToolStripMenuItem.Checked = false;
            sqlToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.XML;
            LanguageCheck();
        }

        private void lua()
        {
            csToolStripMenuItem.Checked = false;
            visualBasicToolStripMenuItem.Checked = false;
            javaScriptToolStripMenuItem.Checked = false;
            htmlToolStripMenuItem.Checked = false;
            phpToolStripMenuItem.Checked = false;
            xmlToolStripMenuItem.Checked = false;
            luaToolStripMenuItem.Checked = true;
            sqlToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.Lua;
            LanguageCheck();
        }

        private void sql()
        {
            csToolStripMenuItem.Checked = false;
            visualBasicToolStripMenuItem.Checked = false;
            javaScriptToolStripMenuItem.Checked = false;
            htmlToolStripMenuItem.Checked = false;
            phpToolStripMenuItem.Checked = false;
            xmlToolStripMenuItem.Checked = false;
            luaToolStripMenuItem.Checked = false;
            sqlToolStripMenuItem.Checked = true;
            customToolStripMenuItem.Checked = false;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.SQL;
            LanguageCheck();
        }

        private void custom()
        {
            csToolStripMenuItem.Checked = false;
            visualBasicToolStripMenuItem.Checked = false;
            javaScriptToolStripMenuItem.Checked = false;
            htmlToolStripMenuItem.Checked = false;
            phpToolStripMenuItem.Checked = false;
            xmlToolStripMenuItem.Checked = false;
            luaToolStripMenuItem.Checked = false;
            sqlToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = true;

            fastColoredTextBox.Language = FastColoredTextBoxNS.Language.Custom;
            LanguageCheck();
        }

        #endregion

        #region Themes

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

        #endregion

        private void edyt_Load(object sender, EventArgs e)
        {
            LanguageCheck();
        }

        private void fastColoredTextBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            isSaved = false;
        }

        private void edyt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaved == false)
            {
                if (MessageBox.Show("You have unsaved changes, discard file?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
    }
}