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
    public partial class Feedback : Form
    {
      
        public Feedback()
        {
            InitializeComponent();

            label1.BackColor = Color.FromArgb(41, 53, 56);
        }

        private void Feedback_FormClosing(object sender, FormClosingEventArgs e)
        {
            Body novo = new Body();
            novo.timer1.Enabled = false;
        }

        private void Feedback_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Body novo = new Body();
                novo.timer1.Enabled = false;
                this.Close();
            }
                
                
                

            
        }

        private void digital_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
