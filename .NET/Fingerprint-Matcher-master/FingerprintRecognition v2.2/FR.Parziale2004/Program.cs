using PRFramework.FingerprintRecognition.Parziale2004;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Parziale
{
    static class Program
    {

        [STAThread]

        static void Main()
        {
         
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Start());
                  


        }
    }
}
