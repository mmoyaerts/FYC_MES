using Opc.Ua;
using System;
using System.Windows.Forms;
using td_fin;

using static System.Windows.Forms.Application;

namespace td_fin
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}