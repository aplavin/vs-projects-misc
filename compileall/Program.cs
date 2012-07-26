using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace compileall
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = args.Count() > 0 ? args[0] : ".";
            foreach (string source in Directory.GetFiles(directory, "*.cpp", SearchOption.AllDirectories))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Compiling '{0}'...\t", source);

                string exec = Path.ChangeExtension(source, "exe");
                Process proc = Process.Start(
                    new ProcessStartInfo(
                        "g++",
                        String.Format("\"{0}\" -o \"{1}\" -O2 -s", source, exec))
                        {
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                        }
                );

                string errors = proc.StandardError.ReadToEnd();
                proc.WaitForExit();


                if (proc.ExitCode != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error");

                    File.AppendAllText(Path.ChangeExtension(source, "txt"), errors);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK");
                }
            }
        }
    }
}

