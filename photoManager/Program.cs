using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Goheer.EXIF;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Collections;
using System.Threading;

namespace photoManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            foreach (string file in Directory.GetFiles(@"E:\Александр\Pictures\Photos", "Thumbs.db", SearchOption.AllDirectories))
            {
                File.Delete(file);
            }

            int i = 0;
            Dictionary<long, List<string>> dict = new Dictionary<long, List<string>>();
            foreach (string file in Directory.GetFiles(@"E:\Александр\Pictures\Photos\2011", "*.jp*g", SearchOption.AllDirectories))
            {
                if (file.Contains("Other Images"))
                {
                    continue;
                }

                string dts;
                EXIFextractor ee;
                try
                {
                    ee = new EXIFextractor(file, "\n", "");

                    if (ee["DTOrig"] == null || ee["DTOrig"].ToString() == "0000:00:00 00:00:00\0")
                    {
                        if (ee["Date Time"] == null || ee["Date Time"].ToString() == "0000:00:00 00:00:00\0")
                        {
                            dts = new FileInfo(file).CreationTime.ToString();
                        }
                        else
                        {
                            dts = ee["Date Time"].ToString();
                        }
                    }
                    else
                    {
                        dts =  ee["DTOrig"].ToString();
                    }
                    if (dts != null)
                    {
                        if (dts.Count(ch => ch == ':') == 4)
                        {
                            dts = dts.Substring(0, dts.IndexOf(":")) + '-' + dts.Substring(dts.IndexOf(":") + 1);
                            dts = dts.Substring(0, dts.IndexOf(":")) + '-' + dts.Substring(dts.IndexOf(":") + 1);
                        }
                        DateTime dt = DateTime.Parse(dts);

                        Rename(file, dt.ToString("MMMM_d__HH_mm_ss.jp\\g"));
                    }

                    i++;
                    if (i % 100 == 0)
                    {
                        Console.WriteLine(i);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            Console.WriteLine("Completed");
            Console.ReadKey();
        }

        public static void Rename(string file, string name)
        {
            string s = Path.Combine(Path.GetDirectoryName(file), name);
            while (file != s && File.Exists(s))
            {
                s = s.Substring(0, s.LastIndexOf('.')) + "_" + s.Substring(s.LastIndexOf('.'));
            }
            File.Move(file, s);
        }

        public static string ArrayToString(IList array, string delimeter = "")
        {
            string outputString = "";

            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] is IList)
                {
                    //Recursively convert nested arrays to string
                    outputString += ArrayToString((IList)array[i], delimeter);
                }
                else
                {
                    outputString += array[i];
                }

                if (i != array.Count - 1)
                    outputString += delimeter;
            }

            return outputString;
        }
    }
}
