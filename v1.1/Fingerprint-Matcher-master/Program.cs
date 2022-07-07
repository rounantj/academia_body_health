using Fingerprint_Matcher;
using System;
using System.Threading;
using System.Windows.Forms;


namespace Fingerprint_Matcher
{
    static class Program
    {
        
        [STAThread]
       
        static void Main()
        {
            Conexao DB = new Conexao();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string queryTableMain = "create table if not exists clientes(id integer not null auto_increment, num_identificador text, nome text, telefone text,sexo text, email text, cpf text, dataVencimento date, horario text, treinos text, tipoTreino text,   createdAt datetime, updatedAt datetime, primary key (id))";
            string queryTableTimes = "create table if not exists horarios(id integer not null auto_increment,descricao text, comeca text, termina text, horario text, createdAt datetime, updatedAt datetime, primary key (id))";
            string queryTableLOG = "create table if not exists clientesLOG( id integer not null auto_increment, num_identificador text, nome text, dataVencimento date, acao text, createdAt datetime, updatedAt datetime, primary key (id))";

            string pathWamp64 = "C:/wamp64/wampmanager.exe";
            string pathWamp86 = "C:/wamp/wampmanager.exe";

          

            DB.verifica("select version()");
            while (true)
            {
                if (DB.status)
                {
                 // MessageBox.Show("TRUE");
                    DB.queryVoid(queryTableMain);
                    DB.queryVoid(queryTableLOG);
                    DB.queryVoid(queryTableTimes);

                    

                    Application.Run(new Body());
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
                            Application.Run(new Body());
                            break;
                        }
                        catch
                        {

                            System.Diagnostics.Process.Start(pathWamp86);
                            Thread.Sleep(TimeSpan.FromSeconds(15));
                            DB.queryVoid(queryTableMain);
                            Application.Run(new Body());
                            break;
                        }

                    }
                    catch { }
                    
                    
                    
                }
            }
            
           

            


    }
        


    }
}
