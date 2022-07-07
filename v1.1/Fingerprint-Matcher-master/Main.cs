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

namespace Fingerprint_Matcher
{
    public partial class Main : Form
    {

        Futronic novo = new Futronic();
        Bitmap bitmap1, bitmap2, myBit, myBit2;
        string currentImage = "";
        string xml1, xml2;

        PNFeatures featureA, featureB;



        ImageCodecInfo myImageCodecInfo;
        System.Drawing.Imaging.Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;
        public Main()
        {
            InitializeComponent();
        }

        public string score;
        public Bitmap qry;
        public Bitmap temp;

        private double match(Bitmap query, Bitmap template)
        {
            Change_Resolution2(query);
            Change_Resolution2(template);

            var fingerprintImg1 = query;
            var fingerprintImg2 = template;

            var featExtractor = new PNFeatureExtractor() { MtiaExtractor = new Ratha1995MinutiaeExtractor() };
            var features1 = featExtractor.ExtractFeatures(fingerprintImg1);
            var features2 = featExtractor.ExtractFeatures(fingerprintImg2);


            var matcher = new PN();
            double similarity = matcher.Match(features1, features2);
            score = similarity.ToString("0.000");
            Console.WriteLine("the matched score is {0}", score);
            if (similarity > 30)
            {

                Console.WriteLine("Its a Match !!\n" + similarity, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                Console.WriteLine("Unsuccessfull !!" + similarity, "Result", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return similarity;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            // 
            try
            {
                //   int minuto = Convert.ToInt32(DateTime.Now.Second.ToString("00")) / 5;
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("asdsad");
                novo.Init();
                if (novo.IsFinger())
                {
                    try
                    {
                        novo.SetDiodesStatus(true, true);
                        qry = novo.ExportBitMap();
                        qry.SetResolution(500, 500);
                       // pictureBox1.Image = qry;

                        myImageCodecInfo = GetEncoderInfo("image/bmp");
                        myEncoder = System.Drawing.Imaging.Encoder.Quality;

                        myEncoderParameters = new EncoderParameters(1);

                        // Save the bitmap as a JPEG file with quality level 5.
                        myEncoderParameter = new EncoderParameter(myEncoder, 75L);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        qry.Save("C:/Users/ronanr/Pictures/FINGERS_DB/_3.bmp", myImageCodecInfo, myEncoderParameters);
                        pictureBox1.ImageLocation="C:/Users/ronanr/Pictures/FINGERS_DB/_3.bmp";
                        // myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS/temp_3.bmp", myImageCodecInfo, myEncoderParameters);






                        /*
                        string[] arquivos = Directory.GetFiles("C:/Users/ronanr/Pictures/FINGERS_DB/");
                        double initialValue = 0; string fileName = "";
                        foreach (string arq in arquivos)
                        {
                            Bitmap bitLocal = new Bitmap(arq);
                            bitLocal.SetResolution(500, 500); qry.SetResolution(500, 500);
                            double valueCurrent = match(qry,bitLocal);
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
                            Console.WriteLine(arq);
                            Console.WriteLine(arq + "\n-> RESULT -> " + valueCurrent);
                        }
                        pictureBox2.Image = new Bitmap(fileName);
                        label1.Text = "The result is:\n" + fileName.Replace("C:/Users/ronanr/Pictures/FINGERS_DB/", "") + "\nPrecisão: " + initialValue.ToString("0") + "%";
                        */
                    }
                    catch (Exception erru)
                    {
                        Console.WriteLine("Oops..\n" + erru);
                        

                    }

                    Console.WriteLine("Ooo DEDO");
                }
                else
                {
                    novo.SetDiodesStatus(false, false);
                }
            }
            catch
            {

            }
            }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private Bitmap Change_Resolution(string file)
        {
            using (Bitmap bitmap = (Bitmap)Image.FromFile(file))
            {
                using (Bitmap newBitmap = new Bitmap(bitmap))
                {
                    newBitmap.SetResolution(500, 500);
                    return newBitmap;
                }
            }
        }
        private Bitmap Change_Resolution2(Bitmap file)
        {


            file.SetResolution(500, 500);
            return file;


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
        public void procura()
        {

            string[] arquivos = Directory.GetFiles("C:/Users/ronanr/Pictures/FINGERS_DB/");



            Console.WriteLine("Arquivos:");
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
                Console.WriteLine(arq);
                Console.WriteLine(arq + "\n-> RESULT -> " + valueCurrent);
            }
            pictureBox2.Image = new Bitmap(fileName);
            label1.Text = "The result is:\n" + fileName.Replace("C:/Users/ronanr/Pictures/FINGERS_DB/", "") + "\nPrecisão: " + initialValue.ToString("0") + "%";
        }
        public void captura()
        {
            pictureBox1.ImageLocation = "C:/Users/ronanr/Pictures/FINGERS/default.jfif";
            pictureBox2.ImageLocation = "C:/Users/ronanr/Pictures/FINGERS/default.jfif";
            // novo.Init();
            Bitmap myBitmap = novo.ExportBitMap();
            myBit2 = myBitmap;
            if (novo.Connected)
            {
                novo.SetDiodesStatus(true, true);
                Console.WriteLine("Conected...");
                myImageCodecInfo = GetEncoderInfo("image/bmp");
                myEncoder = System.Drawing.Imaging.Encoder.Quality;

                myEncoderParameters = new EncoderParameters(1);

                // Save the bitmap as a JPEG file with quality level 5.
                myEncoderParameter = new EncoderParameter(myEncoder, 5L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                //  myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS_DB/_3.bmp", myImageCodecInfo, myEncoderParameters);
                // myBitmap.Save("C:/Users/ronanr/Pictures/FINGERS/temp_3.bmp", myImageCodecInfo, myEncoderParameters);


                novo.ftrScanOpenDevice1();

                qry = myBitmap;
                bitmap2 = myBitmap;
                // bmpToXML(qry, name.Text);
                // xml1 = "C:/Users/ronanr/Pictures/FINGERS_DB/" + name.Text + ".xml";
            }




            if (pictureBox1 != null)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
            novo.SetDiodesStatus(false, false);



            pictureBox1.Image = myBitmap;
        }
    }
}
