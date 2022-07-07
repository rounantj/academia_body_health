using Fingerprint_Matcher;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using SourceAFIS.Simple; // import namespace SourceAFIS.Simple
using static Sample.AFIS;
using System.Collections.Generic;


namespace Fingerprint_Matcher
{
    static class Program
    {

        
        [STAThread]
       
        static void Main()
        {
            List<MyPerson> database = new List<MyPerson>();
            Conexao DB = new Conexao();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string queryTableMain = "create table if not exists clientes(id integer not null auto_increment, num_identificador text, nome text, telefone text,sexo text, email text, cpf text, dataVencimento date, horario text, treinos text, tipoTreino text,   createdAt datetime, updatedAt datetime, primary key (id))";
            string queryTableTimes = "create table if not exists horarios(id integer not null auto_increment,descricao text, comeca text, termina text, horario text, createdAt datetime, updatedAt datetime, primary key (id))";
            string queryTableLOG = "create table if not exists clientesLOG( id integer not null auto_increment, num_identificador text, nome text, dataVencimento date, acao text, createdAt datetime, updatedAt datetime, primary key (id))";

            string pathWamp64 = "C:/wamp64/wampmanager.exe";
            string pathWamp86 = "C:/wamp/wampmanager.exe";

            BinaryFormatter formatter = new BinaryFormatter();

            Console.WriteLine("Reloading database...");
            using (FileStream stream = File.OpenRead("database.dat"))
                database = (List<MyPerson>)formatter.Deserialize(stream);


            DB.verifica("select version()");
            while (true)
            {
                if (DB.status)
                {
                 // MessageBox.Show("TRUE");
                    DB.queryVoid(queryTableMain);
                    DB.queryVoid(queryTableLOG);
                    DB.queryVoid(queryTableTimes);

                    

                    Application.Run(new Body(database));
                    break;
                }else
                {
                 //  MessageBox.Show("FALSE");
                    try
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(pathWamp64);
                            Thread.Sleep(TimeSpan.FromSeconds(15));
                            DB.queryVoid(queryTableMain);
                            Application.Run(new Body(database));
                            break;
                        }
                        catch
                        {
                            
                            System.Diagnostics.Process.Start(pathWamp86);
                            Thread.Sleep(TimeSpan.FromSeconds(15));
                            DB.queryVoid(queryTableMain);
                            Application.Run(new Body(database));
                            break;
                        }

                    }
                    catch { }
                    
                    
                    
                }
            }
            
           

            


    }
        


    }
}
