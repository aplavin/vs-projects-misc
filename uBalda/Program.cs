using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Silvermoon.WinControls;
using Silvermoon.Controls;

namespace uBalda
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Keyboard.lang = Keyboard.Lang.RU;
            SMApplication.Run<SettingsPage>("uBalda");
        }
    }
}