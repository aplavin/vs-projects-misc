
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using FTPLib;

namespace ftpupload
{
    class Program
    {
        static DateTime startDt;
        static string lastFile = "";
        static DateTime lastDt = DateTime.Now;

        static void ProgressEvent(FileInfo File, long BytesTotal, long FileSize, long TotalBytesDirectorySent, long TotalBytesDirectory, long StartTickCount)
        {
            if (File.FullName != lastFile || DateTime.Now.CompareTo(lastDt + TimeSpan.FromSeconds(2)) == 1)
            {
                lastFile = File.FullName;
                lastDt = DateTime.Now;

                TimeSpan elapsed = DateTime.Now - startDt;
                elapsed.Subtract(TimeSpan.FromMilliseconds(elapsed.Milliseconds));
                long speed = TotalBytesDirectorySent / (elapsed.Seconds == 0 ? 1 : elapsed.Seconds);
                TimeSpan remaining = TimeSpan.FromSeconds((TotalBytesDirectory - TotalBytesDirectorySent) / speed);

                Console.WriteLine("{0}: {1}k/{2}k; {3}k/{4}k; {5}k/s; {6} - {7}", File.FullName,
                    BytesTotal / 1024, FileSize / 1024,
                    TotalBytesDirectorySent / 1024, TotalBytesDirectory / 1024,
                    speed / 1024,
                    elapsed, remaining);
            }
        }

        static void Main(string[] args)
        {
            FTP ftp = new FTP("alexander-plavin.ftp.narod.ru", "alexander-plavin", "ZznQHBkgmRzOg4V4n8Ya");
            ftp.ChangeDir("photos");
            startDt = DateTime.Now;
            ftp.UploadFolderRecursively(@"C:\wput", ProgressEvent);
            Console.ReadLine();
        }
    }
}
