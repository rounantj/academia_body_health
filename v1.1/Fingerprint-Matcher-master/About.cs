using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fingerprint_Matcher
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            aboutUS.Text = "Este produto (Software) foi desenhado sob medida\npara atender ao Studio Body Health em Jacupemba - ES.\n\n\nRequisitos:\n1- Wampserver (x86 ou x64) instalado.\n" +
                "2- Base de dados nomeada como 'fingers' criada e estável.\n\n\n\nPrecisa de suporte ou quer um orçamento?\nFaça contato: (27) 9-9601-1204 Whatsapp ( Ronan Rodrigues )\nou email rounantj@hotmail.com";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
