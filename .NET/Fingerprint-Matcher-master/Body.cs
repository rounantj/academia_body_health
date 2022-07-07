using System;
using System.Drawing;
using System.Windows.Forms;

using PatternRecognition.FingerprintRecognition.Core;
using PatternRecognition.FingerprintRecognition.FeatureExtractors;
using PatternRecognition.FingerprintRecognition.Matchers;
using PatternRecognition.FingerprintRecognition.FeatureRepresentation;

using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;
using Sample;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simple
using static Sample.AFIS;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


using System.Net.Sockets;

using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Fingerprint_Matcher
{
    public partial class Body : Form
    {
        int conta = 0;
        Futronic novo = new Futronic();
        Bitmap bitmap1, bitmap2, myBit, myBit2;
        string currentImage = "";
        string xml1, xml2;
        AFIS afi = new AFIS();
        Feedback novu = new Feedback();
        bool retornu = false;

        PNFeatures featureA, featureB;
        string CurrentPATH = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        Conexao DB = new Conexao();
      public  List<MyPerson> database = new List<MyPerson>();
        ImageCodecInfo myImageCodecInfo;
        System.Drawing.Imaging.Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;
        bool devoVerificar = true;
        // Initialize SourceAFIS
       
        public Body(List<MyPerson>  data)
        {
            InitializeComponent();

            database = data;
          

                   
            /*
               StreamReader objInput = new StreamReader("database.dat", System.Text.Encoding.Default);
               string contents = objInput.ReadToEnd().Trim();
               string[] split = System.Text.RegularExpressions.Regex.Split(contents, "\\s+", RegexOptions.None);
               foreach (string s in split)
               {
                   Console.WriteLine(s);
               }
               */



            feedback.BackColor = Color.FromArgb(41, 53, 56);
            label1.BackColor = Color.FromArgb(41, 53, 56);
            statusStrip1.BackColor = Color.FromArgb(41, 53, 56);

            DB_status.Text = "Aplicativo Em Stand By...";
            DB_status.ForeColor = System.Drawing.Color.Blue;

           
            //  pictureBox1.ImageLocation = CurrentPATH+"default.jfif";
            //  pictureBox2.ImageLocation = "C:/Users/ronanr/Pictures/FINGERS/default.jfif";



        }
        public static string hash(byte[] file)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                return BitConverter.ToString(sha1.ComputeHash(file)).Replace("-", "");
            }
        }
        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {

            BitmapData bmpdata = null;

            try
            {
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }

        }


        public string score;
        public Bitmap qry;
        public Bitmap temp;

        static async Task Main()
        {
            Futronic novo = new Futronic();
            if (novo.IsFinger())
            {
              //  Console.WriteLine("Pos o dedo");
            }
        }

        private Bitmap Change_Resolution(string file)
        {
            using (Bitmap bitmap = (Bitmap)Image.FromFile(file))
            {
                using (Bitmap newBitmap = new Bitmap(bitmap))
                {
                    newBitmap.SetResolution(500,500);
                    return newBitmap;
                        }
            }
        }
        private Bitmap Change_Resolution2(Bitmap file)
        {
           
              
                    file.SetResolution(5, 5);
                    return file;
                
            
        }

        private double match(Bitmap query, Bitmap template)
        {
           // Change_Resolution2(query);
           // Change_Resolution2(template);
            
          //  var fingerprintImg1 = query;
          //  var fingerprintImg2 = template;
            
            var featExtractor = new PNFeatureExtractor() { MtiaExtractor = new Ratha1995MinutiaeExtractor() };
            var features1 = featExtractor.ExtractFeatures(query);
            var features2 = featExtractor.ExtractFeatures(template);
           

           var matcher = new PN();
           //  = new PN();

            double similarity = matcher.Match(features1, features2);
            score = similarity.ToString("0.000");
          //  Console.WriteLine("the matched score is {0}", score);
            if (similarity > 30)
            {
               
              // Console.WriteLine("Its a Match !!\n"+similarity, "Result", MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
            }
            else
            {
              //  Console.WriteLine("Unsuccessfull !!" + similarity, "Result", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return similarity;
        }
        

        private void Form1_Load(object sender, EventArgs e)
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
        public void captura()
        {
            pictureBox1.ImageLocation = "C:/Users/ronanr/Pictures/FINGERS/default.jfif";
           // pictureBox2.ImageLocation = "C:/Users/ronanr/Pictures/FINGERS/default.jfif";
           // novo.Init();
            Bitmap myBitmap = novo.ExportBitMap();
            myBit2 = myBitmap;
            if (novo.Connected)
            {
                novo.SetDiodesStatus(true, true);
               // Console.WriteLine("Conected...");
                myImageCodecInfo = GetEncoderInfo("image/bmp");
                myEncoder = System.Drawing.Imaging.Encoder.Quality;

                myEncoderParameters = new EncoderParameters(1);

                // Save the bitmap as a JPEG file with quality level 5.
                myEncoderParameter = new EncoderParameter(myEncoder, 5L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS_DB/_3.bmp", myImageCodecInfo, myEncoderParameters);
                myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS/temp_3.bmp", myImageCodecInfo, myEncoderParameters);


                novo.ftrScanOpenDevice1();

                qry = myBitmap;
                bitmap2 = myBitmap;
                // bmpToXML(qry, name.Text);
                // xml1 = "C:/Users/ronanr/Pictures/FINGERS_DB/" + name.Text + ".xml";
            }




           
            novo.SetDiodesStatus(false, false);



            pictureBox1.Image = myBitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            novo.Init();
            Bitmap myBitmap = novo.ExportBitMap();
            myBit2 = myBitmap;
            if (novo.Connected)
            {
                novo.SetDiodesStatus(true, true);
              //  Console.WriteLine("Conected...");
                myImageCodecInfo = GetEncoderInfo("image/bmp");
                myEncoder = System.Drawing.Imaging.Encoder.Quality;

                myEncoderParameters = new EncoderParameters(1);

                // Save the bitmap as a JPEG file with quality level 5.
                myEncoderParameter = new EncoderParameter(myEncoder, 5L);
                myEncoderParameters.Param[0] = myEncoderParameter;
              //  myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS_DB/_3.bmp", myImageCodecInfo, myEncoderParameters);
                myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS/temp_3.bmp", myImageCodecInfo, myEncoderParameters);


                novo.ftrScanOpenDevice1();

                qry = myBitmap;
                bitmap2 = myBitmap;
                // bmpToXML(qry, name.Text);
                // xml1 = "C:/Users/ronanr/Pictures/FINGERS_DB/" + name.Text + ".xml";
            }


            pictureBox1.Image = qry;


           
            novo.SetDiodesStatus(false, false);

          
            
                pictureBox1.Image = qry;
          
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine(timer1.Enabled + " _>timer");

            int seconds = Convert.ToInt32(contador.Text);

            int now = DateTime.Now.Second;
           // Console.WriteLine(now+" .... "+seconds);
            if (now != seconds)
            {
               // Console.WriteLine("....");
                contador.Text = now.ToString();
                novo.Init();
                if (novo.IsFinger())
                {
                    Console.Beep();
                    Bitmap pega = novo.ExportBitMap();
                    pictureBox1.Image = pega;
                    novu.digital.Image = pega;
                    novu.digital.Visible = true;
                    testa(pega);

                }
                else
                {

                }
            }else
            {

            }
            
        }

        private async void reboot()
        {


            //await pausaSemReboot(1000);
            // pictureBox2.Visible = false;

           // Console.WriteLine(retornu + " _>retorno");

            pictureBox1.Visible = false;
            novu.digital.Visible = false;

            catracaPNG.Visible = false;
            novu.pictureBox4.Visible = false;

            novu.label1.BackColor = Color.FromArgb(41, 53, 56);
            label1.BackColor = Color.FromArgb(41, 53, 56);
            novu.label1.Visible = true;

            label1.Text = "";
            novu.label1.Text = "Utilize o leitor biométrico...";
            button4.Visible = false;

            button1.Visible = true;

            timer1.Enabled = true;

            if (retornu)
            {
                feedback.ImageLocation = CurrentPATH + @"\leituraPendente.gif";
               // feedback.ImageLocation = CurrentPATH + @"\leituraPendente.gif";
               // novu.pictureBox1.ImageLocation = CurrentPATH + @"\leituraPendente.gif";
                novu.pictureBox1.ImageLocation = CurrentPATH + @"\leituraPendente.gif";
                retornu = false;
            }
            
           

           



        }

        private void cadastrarBiometriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void administraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", "http://10.2.111.224:2000/hora_extra");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DB_status.Text = "Aplicativo Pausado...";
            DB_status.ForeColor = System.Drawing.Color.Yellow;
            timer1.Enabled = false;
          
            reboot();
            button4.Visible = true;
            button1.Visible = false;
            feedback.ImageLocation = null;
            retornu = true;


        }

        private void feedback_Click(object sender, EventArgs e)
        {
            
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        public void procura()
        {

            string[] arquivos = Directory.GetFiles("C:/Users/ronanr/Pictures/FINGERS_DB/");



         //   Console.WriteLine("Arquivos:");
            double initialValue = 0;
            string fileName = "";
            foreach (string arq in arquivos)
            {
                Bitmap bitLocal = new Bitmap(arq);
                double valueCurrent = match(qry, bitLocal);
                if (initialValue < valueCurrent)
                {
                    if (valueCurrent < 100)
                    {
                        initialValue = valueCurrent;
                    }

                    fileName = arq;
                    if (initialValue > 20)
                    {
                        break;
                    }
                }
             //   Console.WriteLine(arq);
              //  Console.WriteLine(arq + "\n-> RESULT -> " + valueCurrent);
            }
            //pictureBox2.Image = new Bitmap(fileName);
           
            label1.Text = "The result is:\n" + fileName.Replace("C:/Users/ronanr/Pictures/FINGERS_DB/","") + "\nPrecisão: " + initialValue.ToString("0") + "%";
        }

        private void beepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.Beep();
            novo.Init();
            if (novo.IsFinger())
            {
                myImageCodecInfo = GetEncoderInfo("image/bmp");
                myEncoder = System.Drawing.Imaging.Encoder.Quality;
               
                myEncoderParameters = new EncoderParameters(1);
                Bitmap myBitmap = novo.ExportBitMap();
                // Save the bitmap as a JPEG file with quality level 5.
                myEncoderParameter = new EncoderParameter(myEncoder, 5L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                //  myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS_DB/_3.bmp", myImageCodecInfo, myEncoderParameters);
                myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS/temp_3.bmp", myImageCodecInfo, myEncoderParameters);
                try
                {
                   // afi.Cadastra("C:/Users/ronanr/Pictures/FINGERS/temp_3.bmp", "Ronan"+(conta++));
                    MessageBox.Show("Success");
                }
                catch(Exception err)
                {
                    MessageBox.Show("Erro\n" + err);
                }
               
            }
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                temp= qry;
                //pictureBox2.Image = temp;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string[] arquivos = Directory.GetFiles("C:/Users/ronanr/Pictures/FINGERS_DB/");



          //  Console.WriteLine("Arquivos:");
            double initialValue = 0;
            string fileName = "";
            foreach (string arq in arquivos)
            {

                Bitmap bitLocal = new Bitmap(arq);
                double valueCurrent = match(qry, bitLocal);
                if (initialValue < valueCurrent)
                {
                    if (valueCurrent < 100)
                    {
                        initialValue = valueCurrent;
                    }

                    fileName = arq;
                    if (initialValue > 20)
                    {
                        break;
                    }
                }
             //   Console.WriteLine(arq);
               // Console.WriteLine(arq + "\n-> RESULT -> " + valueCurrent);
            }
         //   pictureBox2.ImageLocation = fileName;
            feedback.ImageLocation = CurrentPATH + @"\leituraOK.gif";
            retornu = true;
            MessageBox.Show("The result is:\n" + fileName + "\nWith points: " + initialValue);

           
        }

        private void verifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                novo.Init();
                if (novo.IsFinger())
                {
                    myImageCodecInfo = GetEncoderInfo("image/bmp");
                    myEncoder = System.Drawing.Imaging.Encoder.Quality;

                    myEncoderParameters = new EncoderParameters(1);
                    Bitmap myBitmap = novo.ExportBitMap();
                    // Save the bitmap as a JPEG file with quality level 5.
                    myEncoderParameter = new EncoderParameter(myEncoder, 5L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    //  myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS_DB/_3.bmp", myImageCodecInfo, myEncoderParameters);
                    myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS/temp_6.bmp", myImageCodecInfo, myEncoderParameters);
                    try
                    {
                     //   MessageBox.Show("Ai -> " + afi.Verifica("C:/Users/ronanr/Pictures/FINGERS/temp_6.bmp", "Visitor #12345"));
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Erro\n" + err);
                    }

                }
               
            }catch(Exception err)
            {
                MessageBox.Show("erro\n" + err);
            }
        }

        private void criarNovaDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = File.Open("database.dat", FileMode.Create))
                formatter.Serialize(stream, database);
          //  Console.WriteLine("Ciada com sucesso!");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Cadastrar novo = new Cadastrar();
            novo.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            About novo = new About();
            Screen[] sMonitores;
            sMonitores = Screen.AllScreens;
            novo.ShowDialog();
            novo.Left = sMonitores[0].Bounds.Left;
            novo.Top = sMonitores[0].Bounds.Top;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            if (Screen.AllScreens.Length > 1) {
                timer1.Enabled = true;
                Screen[] screens = Screen.AllScreens;
                novu.Location = screens[1].WorkingArea.Location;
                novu.WindowState = FormWindowState.Maximized;
                retornu = false;
                reboot();
                novu.label1.BackColor = Color.FromArgb(41, 53, 56);
                novu.label1.Visible = true;
                novu.ShowDialog();
            }

            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            /*
            Bitmap foto = new Bitmap(@"USERS_FINGERS/"+testi.Text+".bmp");

            string texto = afi.Verifica(database, foto, "Visitante");
            MessageBox.Show(texto);
            */
           


        }
        public void LIBERACATRACA() {

            //porta de comunicação do socket
            const int PORTA = 9000;
            //tamanho máximo da mensagem recebida do cliente
            const int TAMANHO_BUFFER = 10000;
            //socket do cliente        private TcpClient cliente;
            //mensagem que o cliente manda para o servidor
            string mensagem = "LIBERAR";
            //mensagem que o servidor manda de volta ao cliente
            string respostaServidor;


            TcpClient cliente = new TcpClient();

            cliente.Connect("localhost", PORTA);
            NetworkStream servidorStream = cliente.GetStream();
            /* adiciona um caractere especial para melhorar a
              * impressão da mensagem no servidor console
             * e converte a mensagem em um array de bytes
             */
            byte[] saida = Encoding.ASCII.GetBytes(mensagem);
            //envia a mensagem ao servidor
            servidorStream.Write(saida, 0, saida.Length);
            servidorStream.Flush();
            /*

            byte[] entrada = new byte[TAMANHO_BUFFER];
            //recebe uma mensagem do servidor
            servidorStream.Read(entrada, 0, (int)cliente.ReceiveBufferSize);
            //converte a mensagem do servidor em uma string
            respostaServidor = Encoding.ASCII.GetString(entrada);
            */
        }


        private void catracaPNG_Click(object sender, EventArgs e)
        {
            try { LIBERACATRACA(); }catch(Exception err)
            {
                MessageBox.Show("Erro -> "+err);
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
      
            Process.Start("http://" + "localhost:3000/gestao");
        }

        private void Body_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Biometria novo = new Biometria();
            novo.ShowDialog();
        }

        private static System.Timers.Timer syncTimer;
        public async void testa(Bitmap imageim)
        {
            try
            {
                DB.verifica("select version()");
                if (DB.status)
                {
                    DB_status.Text = "...Aplicativo Online";
                    DB_status.ForeColor = System.Drawing.Color.GreenYellow;
                }
                else
                {
                    DB_status.Text = "Banco de Dados Desconectado...";
                    DB_status.ForeColor = System.Drawing.Color.Red;
                    if (devoVerificar)
                    {
                       Application.Restart();
                    }
                }
               
                    try
                    {
                    
                        novo.SetDiodesStatus(true, true);
                        qry = imageim;
                        qry.SetResolution(500, 500);
                        pictureBox1.Image = qry;
                        novu.digital.Image = qry;

                        novu.label1.BackColor = Color.FromArgb(41, 53, 56);
                        label1.BackColor = Color.FromArgb(41, 53, 56);
                        novu.label1.Text = "Analisando biometria...";
                        label1.Text = "Analisando biometria...";

                    pictureBox1.Visible = true;
                        novu.digital.Visible = true;
                
                        double initialValue = 0; int num_identificacao = 0;
                    /*
                       myImageCodecInfo = GetEncoderInfo("image/bmp");
                       myEncoder = System.Drawing.Imaging.Encoder.Quality;

                       myEncoderParameters = new EncoderParameters(1);

                       // Save the bitmap as a JPEG file with quality level 5.
                       myEncoderParameter = new EncoderParameter(myEncoder, 5L);
                       myEncoderParameters.Param[0] = myEncoderParameter;
                       qry.Save(CurrentPATH + "temp.bmp", myImageCodecInfo, myEncoderParameters);



                       foreach (DataRow dataRow in DB.ExibirDados("select *, sha1(img) from imagens order by updatedAt desc").Rows)
                       {
                           //Adiciona na lista Especificando a clouna 

                           Bitmap bmp;
                           using (var ms = new MemoryStream((byte[])dataRow["img"]))
                           {
                               bmp = new Bitmap(ms);
                           }
                           double valueCurrent = match(qry, bmp);
                           initialValue = 0; num_identificacao = Convert.ToInt32(dataRow["num_identificacao"]);
                           if (initialValue < valueCurrent)
                           {
                               if (valueCurrent < 100)
                               {
                                   initialValue = valueCurrent;
                               }

                               if (initialValue > 25)
                               {
                                   currentSelected = bmp;
                                   break;
                               }
                           }

                       }
                         */
                    string texto = afi.Verifica(database,qry, "Visitante");
                    string[] partes = texto.Substring(1).Split('|');
                    initialValue = Convert.ToDouble(partes[0]); num_identificacao = Convert.ToInt32(partes[1]);

                    /*if (initialValue < 10)
                        {
                           
                            label1.Text = "Não reconhecido...\nProcure um atendente e faça seu cadastro!";                    
                            pictureBox1.Visible = false;
                            pictureBox2.Visible = false;
                            await PausaComTaskDelay();
                        }
                        else
                        {
                            DB.ExibirDados("update imagens set updatedAt = now() where num_identificacao = " + num_identificacao);
                            pictureBox2.Image = currentSelected;
                            feedback.ImageLocation = CurrentPATH + @"\leitura2.gif";
                            catracaPNG.Visible = true;
                            pictureBox1.Visible = true;
                            pictureBox2.Visible = true;
                            label1.Text = DB.queryString("select nome from clientes where num_identificador = '" + num_identificacao + "'") + "\nSeja bem vindo!\nGire a catraca para entrar...";
                            await PausaComTaskDelay();
                         }
                         */
                  //  Console.WriteLine("Resultado pesquisa.... " + partes[1]);
                         if(partes[1] != "null")
                    {
                        // DB.ExibirDados("update imagens set updatedAt = now() where num_identificacao = " + num_identificacao);
                        //  pictureBox2.Image = currentSelected;
                        


                        /*
                        EasyInnerSDK.frmMain catraquei = new EasyInnerSDK.frmMain();
                        catraquei.ShowDialog();

                        EasyInnerSDK.frmMain novo21 = new EasyInnerSDK.frmMain();
                        novo21.Show();
                          */
                        // System.Diagnostics.Process.Start("C:\\HEALTH\\Fingerprint-Matcher-master\\EASY INNER\\LabEasyInner\\Fontes\\bin\\Release\\EasyInnerSDK.exe");
                        
                        if (DB.ClienteEmDia(num_identificacao) > 0)
                        {
                            feedback.ImageLocation = CurrentPATH + @"\leitura2.gif";
                            novu.pictureBox1.ImageLocation = CurrentPATH + @"\leitura2.gif";
                            retornu = true;
                            catracaPNG.Visible = true;
                            novu.pictureBox4.Visible = true;
                            pictureBox1.Visible = true;
                            novu.pictureBox1.Visible = true;
                            //  pictureBox2.Visible = true;

                            label1.BackColor = Color.FromArgb(0, 0, 0);
                            novu.label1.BackColor = Color.FromArgb(0, 0, 0);
                            int diasParaVencer = DB.diasParaVencer(num_identificacao);
                            string labelUM = "";

                            if(diasParaVencer == 0)
                            {
                                labelUM = "Olá " + DB.queryString("select nome from clientes where num_identificador = '" + num_identificacao + "'") + "\nSua mensalidade vence hoje!\n\n\nGire a catraca para entrar...\n";
                            }
                            else
                            {
                                if (diasParaVencer <= 7)
                                {
                                    labelUM = "Olá " + DB.queryString("select nome from clientes where num_identificador = '" + num_identificacao + "'") + "\nSua mensalidade vence em " + diasParaVencer + " dias!\n\n\nGire a catraca para entrar...\n";
                                }
                                else
                                {
                                    labelUM = "Olá " + DB.queryString("select nome from clientes where num_identificador = '" + num_identificacao + "'") + "\nSeja bem vindo!\n\n\nGire a catraca para entrar...\n";
                                }
                            }
                            
                            
                            label1.Text = labelUM;
                            novu.label1.Text = labelUM;

                            label1.ForeColor = Color.GreenYellow;
                            novu.label1.ForeColor = Color.GreenYellow;

                            timer1.Enabled = false;
                             try {LIBERACATRACA();}catch (Exception err) {
                               // MessageBox.Show("Catraca desconectada...");
                            }
                            await PausaComTaskDelay();
                        }
                        else
                        {
                            label1.ForeColor = Color.Red;
                            novu.label1.ForeColor = Color.Red;
                            feedback.ImageLocation = CurrentPATH + @"\alertaSino.gif";
                            novu.pictureBox1.ImageLocation = CurrentPATH + @"\alertaSino.gif";
                            retornu = true;


                            pictureBox1.Visible = true; 
                            novu.pictureBox1.Visible = true;
                            //  pictureBox2.Visible = true;

                            label1.BackColor = Color.FromArgb(255, 255, 255);
                            novu.label1.BackColor = Color.FromArgb(255, 255, 255);


                            string labelUM = "Olá " + DB.queryString("select nome from clientes where num_identificador = '" + num_identificacao + "'") + "\nAcesso não permitido no momento!\nProcure a recepção ou a direção da Body Health...\n";
                            label1.Text = labelUM;
                            novu.label1.Text = labelUM;

                            timer1.Enabled = false;
                         
                            await PausaComTaskDelay();
                        }
                        
                        
                       

                    }
                    else
                    {
                        novu.label1.BackColor = Color.FromArgb(41, 53, 56);
                       label1.BackColor = Color.FromArgb(41, 53, 56);
                        novu.label1.Text = "Ajuste melhor o seu dedo...";
                        label1.Text = "Ajuste melhor o seu dedo...";
                       // await PausaComTaskDelay();
                    }
                  





                }
                    catch (Exception erru)
                    {
                  
                       // Console.WriteLine("Oops..\n" + erru);
                    novu.label1.BackColor = Color.FromArgb(41, 53, 56);
                    label1.Visible = true;
                    label1.BackColor = Color.FromArgb(41, 53, 56);
                    novu.label1.Text = "Ainda não te reconhecemos...\nJa solicitou o seu cadastro biométrico?\n\nSe ja possuir cadastro tente novamente!";
                    label1.Text = "Ainda não te reconhecemos...\nPosicione novamente o dedo no leitor!";
                    await PausaComTaskDelay();

                }

                // Console.WriteLine("Ooo DEDO");

            }
            catch (Exception err)
            {
                label1.Visible = true;
                label1.BackColor = Color.FromArgb(41, 53, 56);
                label1.Text = "Ainda não te reconhecemos...\nPosicione novamente o dedo no leitor!";
               // await PausaComTaskDelay();
            }
        }
        async Task PausaComTaskDelay()
        {
            
            await Task.Delay(3000);
          
            reboot();
        }
        async Task pausaSemReboot(int time)
        {

            await Task.Delay(time);

          //  reboot();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            button1.Visible = true;
            timer1.Enabled = true;
   
            /*
            while (true)
            {
                Console.WriteLine("....");
                novo.Init();
                if (novo.IsFinger())
                {
                    Console.Beep();
                    Bitmap pega = novo.ExportBitMap();
                    pictureBox1.Image = pega;
                    testa(pega);
                    break;
                }else
                {

                }
            }
            */
           
            /*
            timer1.Enabled = true;
            button4.Visible = false;
            button1.Visible = true;
            feedback.ImageLocation = CurrentPATH+ @"\leituraPendente.gif";
            */
        }             
    }
}
