/*****************************************
 * Authors of this code:
 * 
 * http://www.whitebyte.info/
 * 
 * WhiteByte
 * 
 * Nick Russler
 * Ahmet Yüksektepe
 * Felix Rath
 * Trutz Behn
 * 
 * Redistributions of source code must retain the above copyright notice
 * No other Limitations
 ****************************************/

using System;
using System.Collections.Generic;
using System.Text;
using FTPLib;
using System.IO;

namespace FTPLib
{
    static class Extension
    {
        #region GetDirectorySize
        private static long GetDirectorySize(string p)
        {
            string[] a = Directory.GetFiles(p, "*.*");
            long b = 0;
            foreach (string name in a)
            {
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            return b;
        }

        private static long GetDirectorySize(string b, Boolean recurse)
        {
            if (!recurse)
                return GetDirectorySize(b);

            long result = 0;
            Stack<string> stack = new Stack<string>();
            stack.Push(b);

            while (stack.Count > 0)
            {
                string dir = stack.Pop();
                try
                {
                    result += GetDirectorySize(dir);
                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        stack.Push(dn);
                    }
                }
                catch { }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Try to Create a directory on the ftp server (When Exception is thrown returns false)
        /// </summary>
        /// <param name="dir">Directory to create</param>
        public static bool tryMakeDir(this FTP ftp, String path)
        {
            try
            {
                ftp.MakeDir(path);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public delegate void ProgressEvent(System.IO.FileInfo File, long BytesTotal, long FileSize, long TotalBytesDirectorySent, long TotalBytesDirectory, long StartTickCount);

        private static long oldBytesTotal = 0;
        private static long TotalBytesDirectory;
        private static long TotalBytesDirectorySend;
        private static long StartTickCount = 0;

        /// <summary>
        /// Uploads a file and notifies through the ProgressEvent
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="path">path to the file to upload</param>
        /// <param name="pe">A delegate to the Callback Method</param>
        public static void UploadFile(this FTP ftp, String path, ProgressEvent pe)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(path);

            StartTickCount = DateTime.Now.Ticks;

            ftp.OpenUpload(fi.FullName, fi.Name, false);

            while (ftp.DoUpload() > 0)
            {
                TotalBytesDirectorySend += (ftp.BytesTotal - oldBytesTotal);

                oldBytesTotal = ftp.BytesTotal;

                if (ftp.BytesTotal == ftp.FileSize)
                    oldBytesTotal = 0;

                if (pe != null)
                {
                    pe(fi, ftp.BytesTotal, ftp.FileSize, ftp.BytesTotal, ftp.FileSize, StartTickCount);
                }
            }
        }

        private static void UploadFolderRecursively(this FTP ftp, String StartDirectory, String DirectoryToUpload, ProgressEvent pe)
        {
            System.IO.DirectoryInfo current = new System.IO.DirectoryInfo(DirectoryToUpload);

            String currentPath = StartDirectory + "/" + current.Name;

            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            ftp.tryMakeDir(currentPath);

            ftp.ChangeDir(currentPath);

            try
            {
                files = current.GetFiles();
            }
            catch
            {
                //
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    ftp.OpenUpload(fi.FullName, fi.Name, false);

                    while (ftp.DoUpload() > 0)
                    {
                        TotalBytesDirectorySend += (ftp.BytesTotal - oldBytesTotal);

                        oldBytesTotal = ftp.BytesTotal;

                        if (ftp.BytesTotal == ftp.FileSize)
                            oldBytesTotal = 0;

                        if (pe != null)
                        {
                            pe(fi, ftp.BytesTotal, ftp.FileSize, TotalBytesDirectorySend, TotalBytesDirectory, StartTickCount);
                        }
                    }
                }

                subDirs = current.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    UploadFolderRecursively(ftp, currentPath, dirInfo.FullName, pe);
                }
            }
        }

        /// <summary>
        /// Uploads a folder Recursively and notifies through the ProgressEvent
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="DirectoryToUpload">path to the Directory to upload</param>
        /// <param name="pe">A delegate to the Callback Method</param>
        public static void UploadFolderRecursively(this FTP ftp, String DirectoryToUpload, ProgressEvent pe)
        {
            TotalBytesDirectory = GetDirectorySize(DirectoryToUpload, true);
            TotalBytesDirectorySend = 0;
            StartTickCount = DateTime.Now.Ticks;
            UploadFolderRecursively(ftp, ftp.GetWorkingDirectory(), DirectoryToUpload, pe);
        }
    }
}