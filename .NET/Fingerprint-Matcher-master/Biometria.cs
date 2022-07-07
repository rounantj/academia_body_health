using Sample;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sample.AFIS;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simple
using static Sample.AFIS;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fingerprint_Matcher
{
    public partial class Biometria : Form
    {

        String numeroRegistro = "";
        Futronic novo = new Futronic();
        string mao = "";
        Conexao DB = new Conexao();
        List<MyPerson> database = new List<MyPerson>();
        Bitmap bitmap1, bitmap2, bitmap3, bitmap4;
        public Biometria()
        {
            InitializeComponent();
          DataTable lista =  DB.ExibirDados("select * from clientes");

            foreach (DataRow row in lista.Rows)
            {
                alunos.Items.Add(row["nome"].ToString() + " -" + row["id"].ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            novo.Init();
            if (novo.IsFinger())
            {
                Console.Beep();
                Bitmap pega = novo.ExportBitMap();
                esquerda1.Image = pega;
                bitmap1 = pega;
              


            }
            else
            {

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void alunos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] partes = alunos.Text.Substring(1).Split('-');

            numeroRegistro = partes[1];
            numRegistro.Text = partes[1];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void esquerda2_Click(object sender, EventArgs e)
        {
            novo.Init();
            if (novo.IsFinger())
            {
                Console.Beep();
                Bitmap pega = novo.ExportBitMap();
                bitmap2 = pega;
                esquerda2.Image = pega;



            }
            else
            {

            }
        }

        private void direita1_Click(object sender, EventArgs e)
        {
            novo.Init();
            if (novo.IsFinger())
            {
                Console.Beep();
                Bitmap pega = novo.ExportBitMap();
                direita1.Image = pega;
                bitmap3 = pega;



            }
            else
            {

            }
        }

        private void resetarBiometriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetBiometrics novo = new ResetBiometrics();
            novo.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mostra.Visible = true;

            AFIS afi = new AFIS();


            if (mao == "E")
            {
                if (esquerda1.Image != null && esquerda2.Image != null)
                {

                 //   afi.Cadastra(database, bitmap1, numeroRegistro.ToString());
                    afi.Cadastra(database, bitmap2, numeroRegistro.ToString());
                    //this.Close();

                }
                else
                {
                    MessageBox.Show("Capture as biometrias antes de salvar!");
                }




            }
            else
            {
                if (direita1.Image != null && direita2.Image != null)
                {

                   // afi.Cadastra(database, bitmap3, numeroRegistro.ToString());
                    afi.Cadastra(database, bitmap4, numeroRegistro.ToString());
//this.Close();

                }
                else
                {
                    MessageBox.Show("Capture as biometrias antes de salvar!");
                }

            }

            DB.queryVoid("update clientes  set num_identificador = id,  createdAt = now(),updatedAt =  now() where id = '" + numeroRegistro + "'");
            MessageBox.Show("Biometrias cadastradas com sucesso!");
            Console.WriteLine("Reloading database...");
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = File.OpenRead("database.dat"))
                database = (List<MyPerson>)formatter.Deserialize(stream);
            Body novu = new Body(database);
            novu.database = database;

            mostra.Visible = false;
        }

        private void direita2_Click(object sender, EventArgs e)
        {
            novo.Init();
            if (novo.IsFinger())
            {
                Console.Beep();
                Bitmap pega = novo.ExportBitMap();
                direita2.Image = pega;
                bitmap4 = pega;



            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mostra.Visible = true;
                    
                        AFIS afi = new AFIS();
                        
                     
                        if (mao == "E")
                        {
                                if (esquerda1.Image != null && esquerda2.Image != null) {

                                    afi.Cadastra(database, bitmap1, numeroRegistro.ToString());
                                  //  afi.Cadastra(database, bitmap2, numeroRegistro.ToString());
                                 

                                }
                                else
                                {
                                    MessageBox.Show("Capture as biometrias antes de salvar!");
                                }




                        }
                        else
                        {
                                if (direita1.Image != null && direita2.Image != null)
                                {

                                    afi.Cadastra(database, bitmap3, numeroRegistro.ToString());
                                  //  afi.Cadastra(database, bitmap4, numeroRegistro.ToString());
                                 

                                }else
                                {
                                    MessageBox.Show("Capture as biometrias antes de salvar!");
                                }
               
                        }

                DB.queryVoid("update clientes  set num_identificador = id,  createdAt = now(),updatedAt =  now() where id = '"+numeroRegistro+"'");
                MessageBox.Show("Biometrias cadastradas com sucesso!");
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = File.OpenRead("database.dat"))
                database = (List<MyPerson>)formatter.Deserialize(stream);
            Body novu = new Body(database);
            novu.database = database;

            mostra.Visible = false;



                    
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "MÃO ESQUERDA") { maoEsquerda.Visible = true; maoDireita.Visible = false; mao = "E"; }
            if (comboBox1.Text == "MÃO DIREITA") { maoEsquerda.Visible = false; maoDireita.Visible = true; mao = "D"; }

        }
    }
}
