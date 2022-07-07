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
    public partial class Navigator : Form
    {
        public Navigator()
        {
            InitializeComponent();
            webBrowser1.Navigate("http://10.2.111.224:2000/hora_extra");
        }
    }
}
