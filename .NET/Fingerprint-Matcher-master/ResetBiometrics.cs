using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sample.AFIS;

namespace Fingerprint_Matcher
{
    public partial class ResetBiometrics : Form
    {
        List<MyPerson> database = new List<MyPerson>();
        public ResetBiometrics()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(pass.Text == "mdt1234@")
            {
                reset.Visible = true; reset.Enabled = true;
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = File.Open("database.dat", FileMode.Create))
                formatter.Serialize(stream, database);
           MessageBox.Show("Reset efetuado com sucesso!");
        }
    }
}
