using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FastSearch
{
    public static class Utils
    {
        public static bool isDirectory(this FileSystemInfo info)
        {
            return info.Attributes.HasFlag(FileAttributes.Directory);
        }
    }
}
