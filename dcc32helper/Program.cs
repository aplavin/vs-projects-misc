using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace dcc32helper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 2)
            {
                Process proc = Process.Start("dcc32.exe", "-CC -$D- " + args[0]);
                proc.WaitForExit();
                if (proc.ExitCode != 0)
                {
                    Environment.Exit(proc.ExitCode);
                }
                File.Delete(args[1]);
                File.Move(Path.ChangeExtension(args[0], "exe"), args[1]);
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }
}
