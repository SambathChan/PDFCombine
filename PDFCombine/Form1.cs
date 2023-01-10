using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PDFCombine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            textBox2.Text = path;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox1.Text = fbd.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox2.Text = fbd.SelectedPath;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(textBox1.Text);

            PdfDocument outputDocument = new PdfDocument();

            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    PdfPage page = inputDocument.Pages[idx];
                    outputDocument.AddPage(page);
                }
            }
            const string filename = "Mergedfile.pdf";
            outputDocument.Save(Path.Combine(textBox2.Text, filename));
            Process.Start(filename);
        }
    }
}
