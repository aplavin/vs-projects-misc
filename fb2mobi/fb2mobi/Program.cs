using System;
using System.IO;

namespace fb2mobi
{
    static class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Fb2Mobi.Convert(args[0]);
        }
    }
}
