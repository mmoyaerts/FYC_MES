using Opc.Ua;
using System;
using System.Windows.Forms;
using td_debut;

using static System.Windows.Forms.Application;

namespace td_debut
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