using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Noterat
{
    public partial class Untiteled : Form
    {
        public Untiteled()
        {
            InitializeComponent();
            Text = "Untiteled - Noterat";
        }

        private void open(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "Documents\\";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                        Textbox.Text = fileContent;
                        this.Text = Path.GetFileName($"{filePath} - Noterat");
                    }
                }
            }
        }

        private void Textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Untiteled - Noterat";
            Textbox.Clear();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string subPath = "Documents//Noterate";
            string filename = RandomString(10);
            string filepath = "${subPath}//{filename}";
            bool exists = System.IO.Directory.Exists(subPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(subPath);
            File.Create(filepath);
            File.WriteAllText(filepath, Textbox.Text);

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Textbox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Textbox.Redo();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|Word document (*.docx)|*.docx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    Textbox.SaveFile(Path.GetFullPath(myStream.ToString()));
                }
                myStream.Close();
                }
            }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            Textbox.SelectAll();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Textbox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Textbox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Textbox.Paste();
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            status_label.Text = "Typing...";
            System.Threading.Thread.Sleep(1000);
        }

        private void statusBar_Enter(object sender, EventArgs e)
        {

        }

        private void Textbox_KeyUp(object sender, KeyEventArgs e)
        {
            status_label.Text = "Ready";
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Textbox.Clear();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Textbox.Refresh();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var culture = new CultureInfo("en-In");
            string datetime = DateTime.Now.ToString();
            Textbox.AppendText(datetime);
        }
    }
    }
