using System;
using System.Xml;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using FastColoredTextBoxNS;
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

        public static class Prompt
        {
            public static string ShowDialog(string caption, string text)
            {
                Form prompt = new Form()
                {
                    Width = 220,
                    Height = 135,
                    FormBorderStyle = FormBorderStyle.FixedToolWindow,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 15, Top = 30, Text = text };
                TextBox textBox = new TextBox() { Left = 80, Top = 27, Width = 100 };
                Button confirmation = new Button() { Text = "Ok", Left = 35, Width = 55, Top = 60, DialogResult = DialogResult.OK };
                Button cancel = new Button() { Text = "Cancel", Left = 115, Width = 55, Top = 60, DialogResult = DialogResult.Cancel };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                cancel.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        #region Variables

        private bool isSaved = true;

        private string currentFile = string.Empty;

        private FastColoredTextBox GetFCTB()
        {
            FastColoredTextBox fctb = null;
            TabPage tp = tabControl.SelectedTab;

            if (tp != null)
            {
                fctb = tp.Controls[0] as FastColoredTextBox;
            }

            return fctb;
        }

        #endregion 

        #region Events

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            TabPage tp = new TabPage("New File");
            FastColoredTextBox fctb = new FastColoredTextBox();
            fctb.Dock = DockStyle.Fill;

            tp.Controls.Add(fctb);
            tabControl.TabPages.Add(tp);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenDialog();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(currentFile);
                sw.Write(GetFCTB().Text);
                sw.Close();
                isSaved = true;
            }
            catch
            {
                SaveDialog();
            }
        }

        private void closeToolStripMenu_Click(object sender, EventArgs e)
        {
            TabPage currTab = tabControl.SelectedTab;

            if (tabControl.SelectedTab == null)
            {
                ;
            }
            else
            {
                tabControl.TabPages.Remove(currTab);
            }            
        }

        private void closeAllToolStripButton_Click(object sender, EventArgs e)
        {
            tabControl.TabPages.Clear(); 
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(GetFCTB().Text, new Font("Segoe UI", 14, FontStyle.Regular), Brushes.Black, new PointF(100, 100));
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog.ShowDialog() == DialogResult.OK)
                printDocument.Print();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            GetFCTB().Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            GetFCTB().Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            GetFCTB().Paste();
        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            GetFCTB().Undo();
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            GetFCTB().Redo();
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
            saveToolStripButton.PerformClick();
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
/*
        private void lightThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lightTheme();
        }

        private void darkThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkTheme();
        }
*/
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                GetFCTB().Font = fd.Font;
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFCTB().ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFCTB().ShowReplaceDialog();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetFCTB().ShowGoToDialog();
        }

        private void dataBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            dataBrowser db = new dataBrowser();
            db.Show();

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
            if (GetFCTB().Language == FastColoredTextBoxNS.Language.CSharp)
                of.Filter = "C# File (*.cs)|*.cs|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.VB)
                of.Filter = "VB File (*.vb)|*.vb|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.JS)
                of.Filter = "JS File (*.js)|*.js|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.HTML)
                of.Filter = "HTML File (*.html)|*.html|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.PHP)
                of.Filter = "PHP File (*.php)|*.php|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.XML)
                of.Filter = "XML File (*.xml)|*.xml|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.Lua)
                of.Filter = "Lua File (*.lua)|*.lua|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.SQL)
                of.Filter = "SQL File (*.sql)|*.sql|Any File|*.*";
            else
                of.Filter = "Any File|*.*";

            if (of.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(of.FileName);
                TabPage tp = new TabPage(Path.GetFileName(of.FileName));
                FastColoredTextBox fctb = new FastColoredTextBox();                
                fctb.Dock = DockStyle.Fill;
                tp.Controls.Add(fctb);
                tabControl.TabPages.Add(tp);
                fctb.Text = sr.ReadToEnd();   
                isSaved = true;
                sr.Close();
            }
        }

        private void SaveDialog()
        {
            SaveFileDialog sf = new SaveFileDialog();
            if (GetFCTB().Language == FastColoredTextBoxNS.Language.CSharp)
                sf.Filter = "C# File (*.cs)|*.cs|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.VB)
                sf.Filter = "VB File (*.vb)|*.vb|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.JS)
                sf.Filter = "JS File (*.js)|*.js|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.HTML)
                sf.Filter = "HTML File (*.html)|*.html|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.PHP)
                sf.Filter = "PHP File (*.php)|*.php|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.XML)
                sf.Filter = "XML File (*.xml)|*.xml|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.Lua)
                sf.Filter = "Lua File (*.lua)|*.lua|Any File|*.*";
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.SQL)
                sf.Filter = "SQL File (*.sql)|*.sql|Any File|*.*";
            else
                sf.Filter = "Any File|*.*";

            if (sf.ShowDialog() == DialogResult.OK)
            { 
                StreamWriter sw = new StreamWriter(sf.FileName);
                tabControl.SelectedTab.Text = Path.GetFileName(sf.FileName);
                sw.Write(GetFCTB().Text);
                sw.Close();
                isSaved = true;
            }
        }

        private void LanguageCheck()
        {
            if (GetFCTB().Language == FastColoredTextBoxNS.Language.CSharp)
            {
                this.languageLabel.Text = "C#";
            }
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.VB)
            {
                this.languageLabel.Text = "Visual Basic";
            }
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.JS)
            {
                this.languageLabel.Text = "Java Script";
            }
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.HTML)
            {
                this.languageLabel.Text = "HTML";
            }
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.PHP)
            {
                this.languageLabel.Text = "PHP";
            }
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.XML)
            {
                this.languageLabel.Text = "XML";
            }
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.Lua)
            {
                this.languageLabel.Text = "Lua";
            }
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.SQL)
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
            if (GetFCTB().Language == FastColoredTextBoxNS.Language.HTML)
            {
                codePreview h = new codePreview(GetFCTB().Text);
                h.Show();
            }
            else if (GetFCTB().Language == FastColoredTextBoxNS.Language.CSharp)
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
                string[] sources = { GetFCTB().Text };

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
            GetFCTB().Language = Language.CSharp;
            LanguageCheck();
        }

        private void visualBasic()
        {
            GetFCTB().Language = Language.VB;
            LanguageCheck();
        }

        private void javaScript()
        {
            GetFCTB().Language = Language.JS;
            LanguageCheck();
        }

        private void html()
        {
            GetFCTB().Language = Language.HTML;
            LanguageCheck();
        }

        private void php()
        {
            GetFCTB().Language = Language.PHP;
            LanguageCheck();
        }

        private void xml()
        {
            GetFCTB().Language = Language.XML;
            LanguageCheck();
        }

        private void lua()
        {
            GetFCTB().Language = Language.Lua;
            LanguageCheck();
        }

        private void sql()
        {
            GetFCTB().Language = Language.SQL;
            LanguageCheck();
        }

        private void custom()
        {
            GetFCTB().Language = Language.Custom;
            LanguageCheck();
        }

        private void languagesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            csToolStripMenuItem.Checked = visualBasicToolStripMenuItem.Checked = javaScriptToolStripMenuItem.Checked = htmlToolStripMenuItem.Checked = phpToolStripMenuItem.Checked = xmlToolStripMenuItem.Checked = luaToolStripMenuItem.Checked = sqlToolStripMenuItem.Checked = customToolStripMenuItem.Checked = false;
        }

        #endregion

        #region Themes
/*
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

            fastColoredTextBox1.BackColor = Color.White;
            fastColoredTextBox1.ForeColor = Color.Black;
            fastColoredTextBox1.LineNumberColor = Color.Turquoise;
            fastColoredTextBox1.IndentBackColor = Color.WhiteSmoke;
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

            fastColoredTextBox1.BackColor = Color.Black;
            fastColoredTextBox1.ForeColor = Color.White;
            fastColoredTextBox1.LineNumberColor = Color.Turquoise;
            fastColoredTextBox1.IndentBackColor = Color.Black;
        }
*/
        #endregion

        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
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

        private void renameTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage currTab = tabControl.SelectedTab;
            string promptValue = Prompt.ShowDialog("Rename Selected Tab", "New Name:");

            if (promptValue == null || currTab == null)
            {
                ;
            }
            else
            {
                currTab.Text = promptValue;
            }
        }
    }
}