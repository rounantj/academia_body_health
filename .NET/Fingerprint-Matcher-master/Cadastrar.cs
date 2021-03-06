using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using System.Drawing.Imaging;
using System.IO;
using Sample;
using static Sample.AFIS;

namespace Fingerprint_Matcher
{
    public partial class Cadastrar : Form
    {
        Futronic novo = new Futronic();
        string token = WindowsIdentity.GetCurrent().Token.ToString();
        Conexao DB = new Conexao();
        List<MyPerson> database = new List<MyPerson>();
        Bitmap bitmap1, bitmap2, myBit, myBit2;
        string currentImage = "";
        string xml1, xml2;
        string CurrentPATH = System.AppDomain.CurrentDomain.BaseDirectory.ToString();


  



        ImageCodecInfo myImageCodecInfo;
        System.Drawing.Imaging.Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;

        public static byte[] ConvertImageToByte(System.Drawing.Image image)
        {
            if (image == null)
                return null;

            byte[] data;

            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap bmp = new Bitmap(image);
                bmp.Save(stream, ImageFormat.Jpeg);
                data = stream.ToArray();
            }

            return data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
                if (nome.Text == "") { MessageBox.Show("Informe o 'Nome' do cliente para salvar!"); }
                else
                {
                    if (CPF.Text == "") { MessageBox.Show("Informe o 'CPF' do cliente para salvar!"); }
                    else
                    {
                        AFIS afi = new AFIS();
                        afi.Cadastra(database, bitmap1, token.ToString());
                     

                        if(DB.ClienteExiste(nome.Text) > 0)
                        {
                            
                            if (MessageBox.Show("Já existe um cliente com esse nome, deseja alterar os dados cadastrados?\nClique em 'OK' para sobrescrever os dados, ou 'CANCELAR' para conferir os dados.\nCaso por concidência os nomes sejam iguais dos dois clientes, adicione um sobrenome para distinguir!", "ATENÇÃO!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                DB.queryVoid("update clientes  set  nome = '" + nome.Text + "', telefone ='" + telefone.Text + "', sexo = '" + comboBox1.Text + "', email = '" + email.Text + "',cpf = '" + CPF.Text + "',horario = '" + horario.Text + "', createdAt = now(),updatedAt =  now() where nome = '" + nome.Text + "'");
                                this.Close();
                              
                            }
                        }
                        else
                        {
                            DB.queryVoid("insert into clientes (id, nome,telefone,sexo,email,cpf,horario,dataVencimento, createdAt, updatedAt) values ( null, '" + nome.Text + "','" + telefone.Text + "','" + comboBox1.Text + "','" + email.Text + "','" + CPF.Text + "','" + horario.Text + "', now(), now(),now())");
                            this.Close();
                       
                            if (MessageBox.Show("Cliente cadastrado com sucesso!", "SUCESSO!", MessageBoxButtons.OK) == DialogResult.OK)
                            {

                            }
                        }
                       
                      
                          
                    }
                }
           
        }

        public Cadastrar()
        {
            InitializeComponent();

          token = token + Convert.ToString(DB.proximaID() + 10);
            //TENTA COLETAR NOMES EM "arquivo.csv"
            
            try
            {
                string[] lineOfContents = File.ReadAllLines(@"arquivo.csv");
                foreach (var line in lineOfContents)
                {
                    string[] nomes = line.Split(',');
                    nome.Items.Add(nomes[0]);
                }
            }catch(Exception erro)
            {

            }
          
             

            

            DataTable horarios =  DB.ExibirDados("select * from horarios");
           

            for (int a = 0; a < horarios.Rows.Count; a++) {
                horario.Items.Add(horarios.Rows[a]["descricao"]);
            }

           

                
                
            



            comboBox1.SelectedIndex = 0;
           
            
        }

        private void Cadastrar_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
          
           
            
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
