using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fingerprint_Matcher
{
    public class CatracaTopData
    {
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAcionamento1(byte Funcao, byte Tempo);

        public string acionaCatraca()
        {
            try
            {
                ConfigurarAcionamento1(8, 5);
                return "Catraca acionada";
            }
            catch(Exception ero)
            {
                return ero.ToString();
               
            }
        }
    }
}
