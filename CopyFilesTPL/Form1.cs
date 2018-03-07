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

namespace CopyFilesTPL
{
    public partial class MainForm : Form
    {
        private static readonly object _locker = new object();
        private FileInfo[] _fileInfo;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private string _pathFrom;
        private string _pathWhere;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbFrom.Text = fbd.SelectedPath;
                _pathFrom = fbd.SelectedPath;
                DirectoryInfo directoryInfo = new DirectoryInfo(_pathFrom);
                _fileInfo = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
                tbCountFiles.Text = _fileInfo.Length.ToString();
                progressBarWorker.Maximum = _fileInfo.Length;
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(tbFrom.Text))
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tbWhere.Text = fbd.SelectedPath;
                    _pathWhere = fbd.SelectedPath;
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                btnEnabled(false);
                var token = _tokenSource.Token;
                foreach (var item in _fileInfo)
                {
                    var task = new Task(() => CopyFiles(item.DirectoryName, _pathWhere, item.Name), token);
                    task.Start();
                    task.ContinueWith(t => this.Invoke(new Action(() => tbMessage.Text = Path.Combine(item.Name))),
                      TaskContinuationOptions.OnlyOnRanToCompletion);

                    task.ContinueWith(t => MessageBox.Show("Calc threw: " + t.Exception),
                                      TaskContinuationOptions.OnlyOnFaulted);

                    task.ContinueWith(t => this.Invoke(new Action(() => tbMessage.Text = Path.Combine(item.Name, " Wasn`t Copyes"))),
                                      TaskContinuationOptions.OnlyOnCanceled);
                }
            }
            catch (AggregateException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CopyFiles(string pathFrom, string pathWhere, string fileName)
        {
            var token = _tokenSource.Token;
            try
            {
                byte[] buffer = new byte[4096];
                int size = 0;
                using (var reader = new FileStream(Path.Combine(pathFrom, fileName), FileMode.Open, FileAccess.Read))
                {
                    using (FileStream writer = new FileStream(Path.Combine(pathWhere, fileName), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        do
                        {
                            size = reader.Read(buffer, 0, buffer.Length);
                            writer.Write(buffer, 0, size);
                        } while (size > 0);
                        token.ThrowIfCancellationRequested();
                    }
                    this.Invoke(new Action(() =>
                    {
                        tbCountDownload.Text = CountFiles(_pathWhere).ToString();
                        progressBarWorker.Increment(1);
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _tokenSource.Cancel();
        }

        private int CountFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles("*.*", SearchOption.AllDirectories).Count();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            progressBarWorker.Value = 0;
            tbCountDownload.Text = string.Empty;
            btnEnabled(true);
            DeleteFiles();
        }

        private void btnEnabled(bool value)
        {
            this.btnCopy.Enabled = value;
        }

        private void DeleteFiles()
        {
            try
            {
                lock (_locker)
                {
                    MessageBox.Show("Delete Files");
                    DirectoryInfo directoryInfo = new DirectoryInfo(_pathWhere);
                    FileInfo[] files = directoryInfo.GetFiles();
                    if (files.Length != 0)
                    {
                        foreach (var file in files)
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tbMessage.Text = ex.Message;
            }
        }
    }
}
