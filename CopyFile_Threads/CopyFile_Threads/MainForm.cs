using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFile_Threads
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var open = new OpenFileDialog();
            open.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                tbFrom.Text = open.FileName;
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            var save = new SaveFileDialog();
            save.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                tbWhere.Text = save.FileName;
            }
        }

        //public static async Task CopyFileAsync(string sourceFile, string destinationFile)
        //{
        //    using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
        //    using (var destinationStream = new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
        //        await sourceStream.CopyToAsync(destinationStream);
        //}

        public static void CopyFile(string sourceFile, string destinationFile)
        {
            using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
            using (var destinationStream = new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
                sourceStream.CopyTo(destinationStream);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFrom.Text) || string.IsNullOrEmpty(tbWhere.Text))
            {
                return;
            }
            
            pbProgress.Enabled = true;
            new Thread(() => CopyFile(tbFrom.Text, tbWhere.Text)).Start();
            //CopyFileAsync(tbFrom.Text, tbWhere.Text);
            Thread.Sleep(2000);
            pbProgress.Enabled = false;
            MessageBox.Show("Final");
        }
    }
}
