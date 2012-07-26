using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace ftpupload
{
    public partial class Form1 : Form
    {

        void UpdateView(string file, long fileSize, long doneSize, long totalSize, TimeSpan elapsed)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(delegate() { UpdateView(file, fileSize, doneSize, totalSize, elapsed); }));
                return;
            }

            elapsed.Subtract(TimeSpan.FromMilliseconds(elapsed.Milliseconds));
            long speed = doneSize / ((long)elapsed.TotalSeconds == 0 ? 1 : (long)elapsed.TotalSeconds);
            TimeSpan remaining = TimeSpan.FromSeconds((totalSize - doneSize) / (speed == 0 ? 1 : speed));

            label1.Text = file;
            label2.Text = String.Format("Файл: {0} k", fileSize / 1024);
            label3.Text = String.Format("Всего: {0} M/{1} M", doneSize / 1024 / 1024, totalSize / 1024 / 1024);
            label4.Text = String.Format("Скорость: {0} k/s", speed / 1024);
            label5.Text = String.Format("Прошло: {0:dd\\.hh\\:mm\\:ss}", elapsed);
            label6.Text = String.Format("Осталось: {0:dd\\.hh\\:mm\\:ss}", remaining);

            progressBar1.Maximum = (int)(totalSize / 1024 / 1024);
            progressBar1.Value = (int)(doneSize / 1024 / 1024);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            new Thread(delegate()
            {
                string[] destList = (from file in Directory.GetFiles(@"P:\2008", "*", SearchOption.AllDirectories)
                                     select file.Replace(@"P:\", ""))
                                     .ToArray();
                List<string> srcList = Directory.GetFiles(@"D:\Александр\Pictures\Photos\2008", "*", SearchOption.AllDirectories).ToList();
                for (int i = 0; i < destList.Length; i++)
                {
                    for (int j = 0; j < srcList.Count; j++)
                    {
                        if (srcList[j].EndsWith(destList[i]))
                        {
                            srcList.RemoveAt(j);
                            break;
                        }
                    }
                }

                long totalSize = srcList.Sum(file => new FileInfo(file).Length);
                long doneSize = 0;
                DateTime startDt = DateTime.Now;

                for (int i = 0; i < srcList.Count; i++)
                {
                    string file = srcList[i];
                    string dest = file.Replace(@"D:\Александр\Pictures\Photos\", @"P:\");
                    string tempDest = Path.ChangeExtension(dest, "doc");

                    long fileSize = new FileInfo(file).Length;
                    UpdateView(dest, fileSize, doneSize, totalSize, DateTime.Now - startDt);
                    doneSize += fileSize;

                    Directory.CreateDirectory(Path.GetDirectoryName(dest));
                    try
                    {
                        File.Copy(file, tempDest, true);
                        File.Move(tempDest, dest);
                    }
                    catch
                    {
                        Thread.Sleep(10000);
                        File.Delete(tempDest);
                        File.Delete(dest);
                        i--;
                        doneSize -= fileSize;
                    }
                }
            }).Start();
            return;
        }
    }
}
