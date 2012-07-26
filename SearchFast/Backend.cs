using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

namespace SearchFast
{
    [Serializable()]
    public class Item : IComparable<Item>
    {
        public string FullName { get; private set; }
        public string Name { get; private set; }
        public FileAttributes Attributes { get; private set; }
        public bool IsDirectory { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime LastAccessTime { get; private set; }
        public DateTime LastWriteTime { get; private set; }

        private long _length = -1;
        public long Length
        {
            get
            {
                return _length;
            }

            set
            {
                if (_length != -1)
                {
                    throw new Exception("Length is already set, cannot set it again");
                }
                else
                {
                    _length = value;
                }
            }
        }

        public Item(FileSystemInfo info)
        {
            FullName = info.FullName;
            Name = info.Name;
            Attributes = info.Attributes;
            IsDirectory = Attributes.HasFlag(FileAttributes.Directory);
            if (!IsDirectory)
            {
                Length = (info as FileInfo).Length;
            }
            CreationTime = info.CreationTime;
            LastAccessTime = info.LastAccessTime;
            LastWriteTime = info.LastWriteTime;
        }

        public int CompareTo(Item other)
        {
            if (IsDirectory != other.IsDirectory)
            {
                // directories come first
                return other.IsDirectory.CompareTo(IsDirectory);
            }
            return FullName.CompareTo(other.FullName);
        }
    }

    public delegate bool Accepter(Item s);

    static class Backend
    {
        static readonly string fileName = "filelist.dat";
        static List<Item> fileList;

        public static bool IsDirectory(this FileSystemInfo info)
        {
            return info.Attributes.HasFlag(FileAttributes.Directory);
        }

        static Backend()
        {
            fileList = new List<Item>();
        }

        static long AddDir(DirectoryInfo directory)
        {
            long size = 0;
            try
            {
                foreach (var entry in directory.EnumerateFileSystemInfos())
                {
                    Item item = new Item(entry);
                    if (item.IsDirectory)
                    {
                        long dsize = AddDir(entry as DirectoryInfo);
                        item.Length = dsize;
                        size += dsize;
                    }
                    else
                    {
                        size += item.Length;
                    }
                    fileList.Add(item);
                }
            }
            catch { }
            return size;
        }

        public static void Load()
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    fileList = (List<Item>)new BinaryFormatter().Deserialize(fs);
                }
            }
            catch
            {
                Update();
            }
        }

        private static void Save()
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, fileList);
            }
        }

        public static void Update()
        {
            foreach (string disc in Directory.GetLogicalDrives())
            {
                AddDir(new DirectoryInfo(disc));
            }
            fileList.Sort();
            //Save();
        }

        public static Item[] Get(Accepter accepter)
        {
            return (from item in fileList
                    where accepter(item)
                    select item)
                    .ToArray();
        }
    }
}
