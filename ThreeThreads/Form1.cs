using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Threads2
{
    public partial class Form1 : Form
    {
        private int _increment = 0;
        private static object _locker = new object();
        private System.Threading.Timer _timerOne;
        private System.Threading.Timer _timerTwo;
        private System.Threading.Timer _timerThree;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnThread1_Click(object sender, EventArgs e)
        {
            _timerOne = new System.Threading.Timer(WriteProcess, null, 0, 10000);
            _timerOne.Change(0, 10000);
        }

        private void WriteProcess(Object state)
        {
            var proc = Process.GetProcesses();

            lock (_locker)
            {
                using (FileStream fs = new FileStream("DataLog.txt", FileMode.Append, FileAccess.Write))
                {
                    string mess = string.Empty;
                    if (fs.CanWrite)
                    {
                        foreach (var item in proc.Select(p => p).OrderBy(p => p.Id))
                        {
                            mess = string.Format("Id: {0}," + Environment.NewLine + "Name: {1}" + Environment.NewLine,
                                item.Id.ToString(), item.ProcessName);
                            byte[] bytesWr = Encoding.Unicode.GetBytes(mess);
                            fs.Write(bytesWr, 0, bytesWr.Length);
                        }
                    }
                }
            }
            MessageBox.Show("1");
        }

        private void MakeScreenShot(Object state)
        {
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            printscreen.Save("Screen/printscreen" + ++_increment + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            MessageBox.Show("2");
        }

        private void btnThread2_Click(object sender, EventArgs e)
        {
            _timerTwo = new System.Threading.Timer(MakeScreenShot, null, 0, 10000);
            _timerTwo.Change(0, 10000);
        }

        private void btnThread3_Click(object sender, EventArgs e)
        {
            _timerThree = new System.Threading.Timer(GetInfoFiles, null, 0, 10000);
            _timerThree.Change(0, 10000);
        }

        private void GetInfoFiles(Object state)
        {
            string mess = string.Empty;
            string[] countLines;
            lock (_locker)
            {
                countLines = File.ReadAllLines("DataLog.txt");
            }
            
            string[] countPhotos = Directory.GetFiles("Screen");

            using (FileStream fs = new FileStream("DataLogFinal.txt", FileMode.Append, FileAccess.Write))
            {
                if (fs.CanWrite)
                {
                    mess = string.Format("CountPhotos.Length: {0}," + Environment.NewLine
                        + "CountLines.Length: {1}" + Environment.NewLine,
                        countPhotos.Length, countLines.Length);
                    byte[] bytesWr = Encoding.Unicode.GetBytes(mess);
                    fs.Write(bytesWr, 0, bytesWr.Length);
                }
            }
            MessageBox.Show("3");
        }
    }


}
