using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Media.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simple
using System.Drawing;

using System.Windows;
using System.Drawing.Imaging;

namespace Sample
{
    public class AFIS
    {
       
        [Serializable]
       public class MyFingerprint : Fingerprint
        {
            public string Filename;
        }

        // Inherit from Person in order to add Name field
        [Serializable]
      public  class MyPerson : Person
        {
            public string Name;
        }

        // Initialize path to images
        static readonly string ImagePath = Path.Combine(Path.Combine("..", ".."), "images");

        // Shared AfisEngine instance (cannot be shared between different threads though)
      public static AfisEngine Afis;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

       public BitmapImage faz(Bitmap image)
        {
            using (var memory = new MemoryStream())
            {
                image.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
        
        public  MyPerson Enroll(Bitmap image, string name)
        {
            Console.WriteLine("Enrolling {0}...", name);

            // Initialize empty fingerprint object and set properties
            MyFingerprint fp = new MyFingerprint();
            fp.Filename = name;
            // Load image from the file
            Console.WriteLine(" Loading image from {0}...", name);
            //BitmapImage image = new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute));
            fp.AsBitmapSource = faz(image);
            // Above update of fp.AsBitmapSource initialized also raw image in fp.Image
            // Check raw image dimensions, Y axis is first, X axis is second
            Console.WriteLine(" Image size = {0} x {1} (width x height)", fp.Image.GetLength(1), fp.Image.GetLength(0));

            // Initialize empty person object and set its properties
            MyPerson person = new MyPerson();
            person.Name = name;
            // Add fingerprint to the person
            person.Fingerprints.Add(fp);

            // Execute extraction in order to initialize fp.Template
            Console.WriteLine(" Extracting template...");
            Afis.Extract(person);
            // Check template size
            Console.WriteLine(" Template size = {0} bytes", fp.Template.Length);

            return person;
        }

        public  void Cadastra(List<MyPerson> database,Bitmap image, string name)
        {
            // Initialize SourceAFIS
            Afis = new AfisEngine();
            BinaryFormatter formatter = new BinaryFormatter();
            // Enroll some people
            try
            {
                Console.WriteLine("Reloading database...");
                using (FileStream stream = File.OpenRead("database.dat"))
                    database = (List<MyPerson>)formatter.Deserialize(stream);

                database.Add(Enroll(image, name));
                Console.WriteLine("Saving database...");
                using (Stream stream = File.Open("database.dat", FileMode.Create))
                    formatter.Serialize(stream, database);
                Console.WriteLine("Cadastrado com sucesso!");
            }
            catch(Exception erro)
            {
                Console.WriteLine("Erro ao cadastrar:\n" + erro);
            }
           
           

            // Save the database to disk and load it back, just to try out the serialization
           
            
           

           


        }

        public string Verifica(List<MyPerson> database  ,Bitmap image, string nameTemp)
        {
            Afis = new AfisEngine();
            BinaryFormatter formatter = new BinaryFormatter();
                   

            MyPerson probe = Enroll(image, nameTemp);

            

            // Look up the probe using Threshold = 10
            Afis.Threshold = 10;
            Console.WriteLine("Identifying {0} in database of {1} persons...", probe.Name, database.Count);
           
            // Null result means that there is no candidate with similarity score above threshold
            
            // Print out any non-null result
           
            float score = 0; string nomeAchado = "";
            foreach (MyPerson pessoa in database)
            {
                Console.WriteLine(pessoa.Name);

                MyPerson match = Afis.Identify(probe, database).FirstOrDefault() as MyPerson;
                nomeAchado = match.Name;
                Console.WriteLine("Probe {0} matches registered person {1}", probe.Name, match.Name);
                if (match == null)
                {
                    Console.WriteLine("No matching person found.");
                    return"0|null";
                }else
                {
                    score = Afis.Verify(probe, match);
                    if (score > 50)
                    {
                        
                       
                        Console.WriteLine("Similarity score between {0} and {1} = {2:F3}", probe.Name, nomeAchado, score);
                        Console.WriteLine("@pause");
                        return score + "|" + nomeAchado;
                    }
                }
                
                
               
            }
            Console.WriteLine("Não encontrado...");
            Console.WriteLine("@pause");
            return "null|null";
            // Compute similarity score


        }






    }
}
