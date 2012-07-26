using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace FastSearch
{

    public class Item : IComparable<Item>
    {
        public string name;
        public FileSystemInfo info;

        public bool isDirectory()
        {
            return info.isDirectory();
        }

        public int CompareTo(Item other)
        {
            return name.CompareTo(other.name);
        }
    }

    public delegate bool Accepter(Item s);

    static class Backend
    {
        static List<Item> fileList;

        static Backend()
        {
            fileList = new List<Item>();
        }

        static void AddDir(DirectoryInfo directory)
        {
            try
            {
                foreach (var entry in directory.EnumerateFileSystemInfos())
                {
                    if (entry.isDirectory())
                    {
                        AddDir(entry as DirectoryInfo);
                    }
                    fileList.Add(new Item { name = entry.FullName, info = entry });
                }
            }
            catch { }
        }

        public static void FillList()
        {
            foreach (string disc in Directory.GetLogicalDrives())
            {
                AddDir(new DirectoryInfo(disc));
            }
            fileList.Sort();
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
